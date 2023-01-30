using System;

namespace Snowlers.Game.Player
{
    public enum EPlayerState
    {
        Idle,
        Move,
        Dead
    }
    
    public interface IPlayerState
    {
        // Pass prev state
        public event Action<EPlayerState> OnStateChanged;
        
        public EPlayerState State { get; set; }
    }
}