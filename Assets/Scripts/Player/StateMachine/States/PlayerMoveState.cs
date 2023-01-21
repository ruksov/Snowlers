public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine _stateMachine)
        : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        PlayerContext.turnChannel.OnEventRaised += OnTurn;
    }

    public override void Exit()
    {
        PlayerContext.turnChannel.OnEventRaised -= OnTurn;
    }

    private void OnTurn()
    {
        m_stateMachine.ChangeState(StateFactory.Turn());
    }
}
