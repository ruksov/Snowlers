using UnityEngine;

public class PlayerTurnState : PlayerBaseState
{
    private float m_startTurnTime = 0.0f;
    private float m_startTurnSpeedX = 0.0f;
    private float m_goalSpeedX = 0.0f;

    private float TurnDuration => Time.time - m_startTurnTime;
    private bool IsRightSide => PlayerContext.side == ESide.Right;

    // Config behaviour
    protected bool m_changeSide = true;

    public PlayerTurnState(PlayerStateMachine _stateMachine)
        : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        PlayerContext.turnChannel.OnEventRaised += OnTurn;
        PlayerContext.sharpTurnChannel.OnEventRaised += OnSharpTurn;

        m_startTurnTime = Time.time;
        m_startTurnSpeedX = PlayerContext.moveVector.x;

        if (m_changeSide)
            PlayerContext.side = IsRightSide ? ESide.Left : ESide.Right;

        m_goalSpeedX = IsRightSide ? GetMaxSpeedX() : -GetMaxSpeedX();
    }

    public override void Exit()
    {
        PlayerContext.turnChannel.OnEventRaised -= OnTurn;
        PlayerContext.sharpTurnChannel.OnEventRaised -= OnSharpTurn;
    }

    public override void Update()
    {
        float ratio = TurnDuration / GetTurnTime();

        if(ratio > 1.0f)
        {
            PlayerContext.moveVector.x = m_goalSpeedX;
            m_stateMachine.ChangeState(StateFactory.Move());
            return;
        }

        PlayerContext.moveVector.x = Mathf.Lerp(m_startTurnSpeedX, m_goalSpeedX, ratio);
    }

    protected virtual float GetMaxSpeedX()
    {
        return PlayerContext.maxSpeedX;
    }

    protected virtual float GetTurnTime()
    {
        return PlayerContext.turnTime;
    }

    private void OnTurn()
    {
        m_stateMachine.ChangeState(StateFactory.Turn());
    }

    private void OnSharpTurn()
    {
        m_stateMachine.ChangeState(StateFactory.SharpTurn());
    }
}
