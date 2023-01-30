using System;
using Snowlers.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Snowlers.Game.Player
{
    public class PlayerMoveService : IPlayerMoveService, ITickable, IDisposable
    {
        private readonly IInputService m_inputService;
        private readonly IPlayerState m_playerState;
        private readonly PlayerSettings m_playerSettings;

        private Vector3 m_velocity;
        private bool m_turnPressed;
        
        public Vector3 Velocity => m_velocity;
        public EMoveSide MoveSide { get; private set; }
        
        private bool IsRightSide => MoveSide == EMoveSide.Right;
        private float MinVelocityX => m_playerSettings.minVelocityX * (IsRightSide ? 1 : -1);
        private float MaxVelocityX => m_playerSettings.maxVelocityX * (IsRightSide ? 1 : -1);
        private float AccelerationX => m_playerSettings.accelerationX * (IsRightSide ? 1 : -1);

        public PlayerMoveService(IInputService inputService, IPlayerState playerState, PlayerSettings playerSettings)
        {
            m_inputService = inputService;

            m_playerState = playerState;
            m_playerState.OnStateChanged += OnPlayerStateChange;

            m_playerSettings = playerSettings;
        }

        public void Dispose()
        {
            m_playerState.OnStateChanged -= OnPlayerStateChange;

            if (m_playerState.State == EPlayerState.Move)
            {
                m_inputService.OnStartTurn -= OnStartTurn;
                m_inputService.OnStopTurn -= OnStopTurn;
            }
        }

        public void Tick()
        {
            if (m_playerState.State != EPlayerState.Move)
                return;

            if(m_turnPressed && VelocityXLessThen(MaxVelocityX))
            {
                m_velocity.x += AccelerationX * Time.deltaTime;

                if (!VelocityXLessThen(MaxVelocityX))
                    m_velocity.x = MaxVelocityX;
            }
            else if (!m_turnPressed && VelocityXLessThen(MinVelocityX))
            {
                m_velocity.x += AccelerationX * Time.deltaTime;

                if (!VelocityXLessThen(MinVelocityX))
                    m_velocity.x = MinVelocityX;
            }
        }
        
        private bool VelocityXLessThen(float value)
        {
            return IsRightSide ? m_velocity.x < value : m_velocity.x > value;
        }

        private void OnStartTurn()
        {
            m_turnPressed = true;
            MoveSide = IsRightSide ? EMoveSide.Left : EMoveSide.Right;
        }

        private void OnStopTurn()
        {
            m_turnPressed = false;
        }

        private void OnPlayerStateChange(EPlayerState prevPlayerState)
        {
            switch (m_playerState.State)
            {
                case EPlayerState.Dead:
                case EPlayerState.Idle:
                    m_velocity = Vector3.zero;

                    if (prevPlayerState == EPlayerState.Move)
                    {
                        m_inputService.OnStartTurn -= OnStartTurn;
                        m_inputService.OnStopTurn -= OnStopTurn;
                    }
                    break;
                
                case EPlayerState.Move:
                    MoveSide = (EMoveSide)Random.Range(0, 2);
            
                    m_velocity.y = -m_playerSettings.velocityY;
                    m_velocity.x = IsRightSide ? m_playerSettings.minVelocityX : -m_playerSettings.minVelocityX;
            
                    m_inputService.OnStartTurn += OnStartTurn;
                    m_inputService.OnStopTurn += OnStopTurn;
                    break;
            }
        }
    }
}