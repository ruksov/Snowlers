using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerMover : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public float speedY = 5.0f;

        public float maxSpeedX = 4.0f;
        public float turnTime = 0.75f;

        public float sharpMaxSpeedX = 8.0f;
        public float sharpTurnTime = 0.5f;
    }

    public Settings m_settings;

    public Vector3 MoveVector => m_moveVector;
    public Vector3 MoveDirection => m_moveVector.normalized;

    private IInputService m_inputService;

    private bool m_isRightSide;
    private bool m_isSharpTurn;
    private float m_speedX;
    private float m_turnTime;
    private float m_startTurnTime;
    private float m_startTurnSpeedX;
    private bool m_isStopped;
    private Vector3 m_moveVector;

    private bool IsInTurnState => m_startTurnTime > 0.0f;

    private float MaxSppedX => m_isRightSide ? m_settings.maxSpeedX : -m_settings.maxSpeedX;
    private float SharpMaxSppedX => m_isRightSide ? m_settings.sharpMaxSpeedX : -m_settings.sharpMaxSpeedX;
    private float CurrentMaxSpeedX => m_isSharpTurn ? SharpMaxSppedX : MaxSppedX;

    [Inject]
    private void Construct(IInputService _inputService)
    {
        m_inputService = _inputService;

        m_inputService.OnTurn += OnTurn;
        m_inputService.OnSharpTurn += OnSharpTurn;

        m_isRightSide = Random.Range(0, 2) == 0;
        m_isSharpTurn = false;

        m_speedX = MaxSppedX;
        m_turnTime = m_settings.turnTime;
        m_startTurnTime = 0.0f;
        m_startTurnSpeedX = 0.0f;
        m_isStopped = false;
    }

    private void OnDestroy()
    {
        m_inputService.OnTurn -= OnTurn;
        m_inputService.OnSharpTurn -= OnSharpTurn;
    }

    public void StopMove()
    {
        m_isStopped = true;
    }

    private void Update()
    {
        if (m_isStopped)
            return;

        if (IsInTurnState)
        {
            ProcessTurn();
        }

        UpdateMoveVector();

        transform.position += m_moveVector;
    }

    void UpdateMoveVector()
    {
        m_moveVector.x = m_speedX;
        m_moveVector.y = m_settings.speedY;
        m_moveVector *= Time.deltaTime;
    }

    private void ProcessTurn()
    {
        float ratio = (Time.time - m_startTurnTime) / m_turnTime;
        if (ratio > 1.0f)
        {
            m_speedX = CurrentMaxSpeedX;
            m_startTurnTime = 0.0f;
            return;
        }

        m_speedX = Mathf.Lerp(m_startTurnSpeedX, CurrentMaxSpeedX, ratio);
    }

    private void OnTurn()
    {
        m_isSharpTurn = false;
        m_startTurnSpeedX = m_speedX;
        m_startTurnTime = Time.time;

        m_isRightSide = !m_isRightSide;
        m_turnTime = m_settings.turnTime;
    }

    private void OnSharpTurn()
    {
        m_isSharpTurn = true;
        m_startTurnSpeedX = m_speedX;
        m_startTurnTime = Time.time;

        m_turnTime = m_settings.sharpTurnTime;
    }
}
