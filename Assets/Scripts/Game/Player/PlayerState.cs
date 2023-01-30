using System;

namespace Snowlers.Game.Player
{
    public class PlayerState : IPlayerState
    {
        private EPlayerState m_state;
        
        public event Action<EPlayerState> OnStateChanged;

        public EPlayerState State
        {
            get => m_state;
            set
            {
                if (m_state == value)
                    return;

                EPlayerState prevState = m_state;
                m_state = value;
                OnStateChanged?.Invoke(prevState);
            }
        }
    }
}