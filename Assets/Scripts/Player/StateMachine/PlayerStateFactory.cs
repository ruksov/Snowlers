public class PlayerStateFactory
{
    private PlayerStateMachine m_stateMachine;

    public PlayerStateFactory(PlayerStateMachine _stateMachine)
    {
        m_stateMachine = _stateMachine;
    }

    public PlayerBaseState Idle() { return new PlayerIdleState(m_stateMachine); }
    public PlayerBaseState Move() { return new PlayerMoveState(m_stateMachine); }
    public PlayerBaseState Turn() { return new PlayerTurnState(m_stateMachine); }
    public PlayerBaseState SharpTurn() { return new PlayerSharpTurnState(m_stateMachine); }
}
