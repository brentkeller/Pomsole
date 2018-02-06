using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pomsole.Core.IO;
using Pomsole.Core.Models;

namespace Pomsole.Core.Data
{
    public interface IDataManager
    {
        T Read<T>(string file);
        void WriteAll(IEnumerable<Session> sessions, string file);
        void Write(Session session, string file);
    }

    public class DataManager : IDataManager
    {
        private readonly IFileManager FileManager;

        public DataManager(IFileManager fileManager)
        {
            FileManager = fileManager;
        }

        public T Read<T>(string file)
        {
            return FileManager.Read<T>(file);
        }

        public void WriteAll(IEnumerable<Session> sessions, string file)
        {
            FileManager.Write(sessions, file);
        }

        public void Write(Session session, string file)
        {
            var sessions = new List<Session>();
            if (File.Exists(file))
                sessions = Read<IEnumerable<Session>>(file).ToList();
            sessions.Add(session);
            WriteAll(sessions, file);
        }

    }
}
