using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine _stateMachine)
        : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        PlayerContext.turnChannel.OnEventRaised += OnTurn;

        PlayerContext.moveVector = Vector3.zero;
        PlayerContext.side = Random.Range(0, 2) == 0 ? ESide.Left : ESide.Right;
    }

    public override void Exit()
    {
        PlayerContext.turnChannel.OnEventRaised -= OnTurn;
    }

    private void OnTurn()
    {
        PlayerContext.moveVector.y = PlayerContext.speedY;
        m_stateMachine.ChangeState(StateFactory.Turn());
    }
}
