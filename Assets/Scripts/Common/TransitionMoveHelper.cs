using UnityEngine;

namespace Snowlers.Common
{
    public class TransitionMoveHelper
    {
        private float m_transitionTime;
        private float m_goalSpeedX;
        private float m_startSpeedX;

        private float m_timeElapsed;

        public float CurrentSpeedX { get; private set; }

        public bool IsDone { get; private set; }

        public void Start(float transitionTime, float goalSpeedX, float startSpeedX)
        {
            m_transitionTime = transitionTime;
            m_goalSpeedX = goalSpeedX;
            m_startSpeedX = startSpeedX;

            IsDone = false;
            m_timeElapsed = 0.0f;
        }

        public void Stop()
        {
            IsDone = true;
        }

        public void Update(float deltaTime)
        {
            if (IsDone)
                return;

            m_timeElapsed += deltaTime;
            float timeRatio = m_timeElapsed / m_transitionTime;

            if (timeRatio > 1.0f)
            {
                IsDone = true;
                CurrentSpeedX = m_goalSpeedX;
                return;
            }

            CurrentSpeedX = Mathf.Lerp(m_startSpeedX, m_goalSpeedX, timeRatio);
        }
    }
}