using System;
using UnityEngine;

namespace Snowlers.Game.Player.Movement
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
        public Transform PlayerTransform { get; }

        public void SetActive(bool isActive);
        public bool IsActive();
        public void SetPlayer(Transform player);
        
        public event Action<float> OnShiftOrigin;
    }
}