﻿using System;
using System.IO;
using System.Timers;
using Autofac;
using Fclp;
using Pomsole.Core;
using Pomsole.Core.Audio;
using Pomsole.Core.Config;
using Pomsole.Core.Models;

namespace Pomsole
{
    class Program
    {
        static IAudioPlayer AudioPlayer;
        static ISessionManager SessionManager;
        static ConfigManager ConfigManager;
        static Timer SessionTimer;
        static SessionConfig SessionConfig;

        static void Main(string[] args)
        {
            var container = BuildContainer();
            ConfigManager = container.Resolve<ConfigManager>();
            InitConfig();
            var parser = GetArgsParser();
            var parsedArgs = parser.Parse(args);
            if (!parsedArgs.HasErrors)
            {
                SessionConfig = parser.Object;
                AudioPlayer = container.Resolve<IAudioPlayer>();
                SessionManager = container.Resolve<ISessionManager>();
                SessionManager.BeginSession(SessionConfig);
                SessionTimer = new Timer(250);
                SessionTimer.Elapsed += Timer_Elapsed;
                SessionTimer.Start();
                while (true)
                    Console.ReadLine();
            }
            else
            {
                Console.WriteLine(parsedArgs.ErrorText);
                Environment.Exit(1);
            }
        }

        private static void InitConfig()
        {
            ConfigManager.Init(Path.Combine(Environment.CurrentDirectory, "Pomsole.config.json"));
            if (ConfigManager.Config.IsEmpty())
            {
                ConfigManager.Config.AlertFilePath = @"C:\files\sounds\ShipBell.wav";
                // TODO: Ask for config values
                ConfigManager.Save();
            }
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SessionManager.Update();
            var status = SessionManager.GetStatus();
            // Clear the current line then reset cursor position
            Console.Write(string.Format("\r{0}", "".PadLeft(Console.CursorLeft, ' ')));
            Console.SetCursorPosition(0, Console.CursorTop);
            if (status.PlayAlert && !SessionConfig.Quiet)
                AudioPlayer.PlayAudio();
            if (status.State == SessionState.Completed)
            {
                Console.WriteLine($"Session completed!");
                Console.WriteLine($"Length: {status.Session.SessionLength}");
                if (!string.IsNullOrWhiteSpace(status.Session.Task))
                    Console.WriteLine($"Task: {status.Session.Task}");
                // TODO: Persist completed session
                Environment.Exit(0);
            }
            else
            {
                var output = $"{status.State.ToString()}: {status.TimeRemaining.ToString("m\\:ss")}";
                Console.Write(output);
                Console.Title = output;
            }
        }

        static FluentCommandLineParser<SessionConfig> GetArgsParser()
        {
            var p = new FluentCommandLineParser<SessionConfig>();

            p.Setup(arg => arg.SessionLength)
                .As('s', "session")
                .WithDescription("Session length in minutes")
                .Required();

            p.Setup(arg => arg.BreakLength)
                .As('b', "break")
                .WithDescription("Break length in minutes")
                .Required();

            p.Setup(arg => arg.Task)
                .As('t', "task")
                .WithDescription("Name of task");

            p.Setup(arg => arg.Category)
                .As('c', "category")
                .WithDescription("Name of task category");

            p.Setup(arg => arg.Quiet)
                .As('q', "quiet")
                .WithDescription("Quiet mode (no sound)")
                .SetDefault(false);

            p.SetupHelp("?", "help")
             .Callback(text => Console.WriteLine(text));

            return p;
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AudioPlayer>().As<IAudioPlayer>();
            builder.RegisterModule(new CoreModule());
            return builder.Build();
        }
    }
}
