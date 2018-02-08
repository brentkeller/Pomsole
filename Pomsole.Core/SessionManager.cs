using Pomsole.Core.Models;

namespace Pomsole.Core
{
    public interface ISessionManager
    {
        void BeginSession(SessionConfig config);
        SessionStatus GetStatus();
        void Update();
    }

    public class SessionManager : ISessionManager
    {
        protected Session Session;
        protected SessionState CurrentState;
        protected bool PlayAlert = false;

        public void BeginSession(SessionConfig config)
        {
            Session = new Session
            {
                SessionLength = config.SessionLength,
                BreakLength = config.BreakLength,
                Category = config.Category,
                Task = config.Task,
                SessionStart = SystemTime.Now,
                SessionEnd = SystemTime.Now.AddMinutes(config.SessionLength)
            };
            CurrentState = SessionState.Session;
        }

        public SessionStatus GetStatus()
        {
            var status = new SessionStatus {
                PlayAlert = PlayAlert,
                Session = Session,
                State = CurrentState
            };
            var endTime = Session.SessionEnd;
            if (CurrentState == SessionState.Break)
                endTime = Session.BreakEnd.Value;
            status.TimeRemaining = endTime - SystemTime.Now;
            return status;
        }

        public void Update()
        {
            PlayAlert = false;
            var now = SystemTime.Now;
            if (now > Session.SessionEnd)
            {
                if (Session.BreakStart == null)
                {
                    Session.BreakStart = SystemTime.Now;
                    Session.BreakEnd = SystemTime.Now.AddMinutes(Session.BreakLength);
                    SetState(SessionState.Break);
                }
                else if (now > Session.BreakEnd)
                {
                    SetState(SessionState.Completed);
                }
            }
        }

        private void SetState(SessionState state)
        {
            CurrentState = state;
            PlayAlert = true;
        }
    }
}
