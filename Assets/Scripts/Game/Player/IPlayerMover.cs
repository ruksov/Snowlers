using System;
using UnityEngine;

namespace Snowlers.Game.Player
{
    public enum EMoveSide
    {
        Right,
        Left
    }
    
    public interface IPlayerMover
    {
        public Vector3 Velocity { get; }
        public EMoveSide MoveSide { get; }
        public Transform PlayerTransfrom { get; }

        public void SetActive(bool isActive);
        public bool IsActive();
        public void SetPlayer(Transform player);
        
        public event Action<float> OnShiftOrigin;
    }
}