using UnityEngine;

namespace Snowlers.Game.Player
{
    public enum EMoveSide
    {
        Right,
        Left
    }
    
    public interface IPlayerMoveService
    {
        public Vector3 Velocity { get; }
        public EMoveSide MoveSide { get; }
    }
}