public abstract class PlayerBaseState
{
    protected PlayerStateMachine m_stateMachine;

    protected PlayerContext PlayerContext => m_stateMachine.PlayerContext;
    protected PlayerStateFactory StateFactory => m_stateMachine.StateFactory;

    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
