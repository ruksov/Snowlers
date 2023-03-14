using System;
using Snowlers.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Snowlers.Game.Player.Movement
{
    public class PlayerMover : IPlayerMover, ITickable, IDisposable
    {
        [Serializable]
        public class Settings
        {
            public float velocityY = 5.0f;
            public float minVelocityX = 8.0f;
            public float maxVelocityX = 16.0f;
            public float accelerationX = 60.0f;
            public float shiftOriginThreshold = 100.0f;
        }
        
        private readonly IInputService m_inputService;
        private readonly Settings m_settings;

        private bool m_isActive;
        private Vector3 m_velocity = Vector3.zero;
        private EMoveSide m_moveSide;
        private bool m_turnPressed;
        private Transform m_playerTransform;
        
        public Vector3 Velocity => m_velocity;
        public EMoveSide MoveSide => m_moveSide;
        public Transform PlayerTransform => m_playerTransform;
        public event Action<float> OnShiftOrigin;


        private bool IsRightSide => MoveSide == EMoveSide.Right;
        private float MinVelocityX => m_settings.minVelocityX * (IsRightSide ? 1 : -1);
        private float MaxVelocityX => m_settings.maxVelocityX * (IsRightSide ? 1 : -1);
        private float AccelerationX => m_settings.accelerationX * (IsRightSide ? 1 : -1);

        public PlayerMover(IInputService inputService, Settings settings)
        {
            m_inputService = inputService;
            m_settings = settings;
        }

        public void SetActive(bool isActive)
        {
            m_isActive = isActive;

            if (m_isActive)
            {
                m_inputService.OnStartTurn += OnStartTurn;
                m_inputService.OnStopTurn += OnStopTurn;
                
                m_velocity.y = m_settings.velocityY;
                m_moveSide = Random.Range(0, 2) == 0 ? EMoveSide.Right : EMoveSide.Left;
            }
            else
            {
                m_inputService.OnStartTurn -= OnStartTurn;
                m_inputService.OnStopTurn -= OnStopTurn;
                
                m_velocity = Vector3.zero;
            }
        }

        public bool IsActive()
        {
            return m_isActive;
        }

        public void SetPlayer(Transform player)
        {
            m_playerTransform = player;
        }

        public void Dispose()
        {
            SetActive(false);
        }

        public void Tick()
        {
            if (!m_isActive)
                return;

            if (VelocityXLessThen(MinVelocityX) ||
                (m_turnPressed && VelocityXLessThen(MaxVelocityX)))
            {
                m_velocity.x += AccelerationX * Time.deltaTime;
            }

            m_playerTransform.position += m_velocity * Time.deltaTime;
            
            if (m_playerTransform.position.y < -m_settings.shiftOriginThreshold)
            {
                OnShiftOrigin?.Invoke(-m_playerTransform.position.y);
            }
        }
        
        private bool VelocityXLessThen(float value)
        {
            return IsRightSide ? m_velocity.x < value : m_velocity.x > value;
        }

        private void OnStartTurn()
        {
            m_turnPressed = true;
            m_moveSide = IsRightSide ? EMoveSide.Left : EMoveSide.Right;
        }

        private void OnStopTurn()
        {
            m_turnPressed = false;
        }
    }
}