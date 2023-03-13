using System;
using Snowlers.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Snowlers.Game.Player
{
    public class PlayerMover : IPlayerMover, ITickable, IDisposable
    {
        private readonly IInputService m_inputService;
        private readonly PlayerSettings m_playerSettings;

        private bool m_isActive;
        private Vector3 m_velocity = Vector3.zero;
        private EMoveSide m_moveSide;
        private bool m_turnPressed;
        private Transform m_playerTransform;
        
        public Vector3 Velocity => m_velocity;
        public EMoveSide MoveSide => m_moveSide;
        
        private bool IsRightSide => MoveSide == EMoveSide.Right;
        private float MinVelocityX => m_playerSettings.minVelocityX * (IsRightSide ? 1 : -1);
        private float MaxVelocityX => m_playerSettings.maxVelocityX * (IsRightSide ? 1 : -1);
        private float AccelerationX => m_playerSettings.accelerationX * (IsRightSide ? 1 : -1);

        public PlayerMover(IInputService inputService, PlayerSettings playerSettings)
        {
            m_inputService = inputService;
            m_playerSettings = playerSettings;
        }

        public void SetActive(bool isActive)
        {
            m_isActive = isActive;

            if (m_isActive)
            {
                m_inputService.OnStartTurn += OnStartTurn;
                m_inputService.OnStopTurn += OnStopTurn;
                
                m_velocity.y = m_playerSettings.velocityY;
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
                
                if (Mathf.Abs(m_velocity.x) > Mathf.Abs(m_playerSettings.maxVelocityX))
                    m_velocity.x = MaxVelocityX;
            }

            m_playerTransform.position += m_velocity * Time.deltaTime;
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