public class PlayerStateMachine
{
	private PlayerBaseState m_state;
	private PlayerContext m_playerContext;
	private PlayerStateFactory m_stateFactory;

	public PlayerContext PlayerContext => m_playerContext;
	public PlayerStateFactory StateFactory => m_stateFactory;

	public PlayerStateMachine(PlayerContext _playerContext)
	{
		m_playerContext = _playerContext;
		m_stateFactory = new(this);

		m_state = m_stateFactory.Idle();
		m_state.Enter();
	}

	public void Update()
	{
		m_state.Update();
	}

	public void ChangeState(PlayerBaseState _state)
	{
		m_state.Exit();
		m_state = _state;
		m_state.Enter();
	}
}

