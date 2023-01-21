public class PlayerSharpTurnState : PlayerTurnState
{
    public PlayerSharpTurnState(PlayerStateMachine _stateMachine)
        : base(_stateMachine)
    {
        m_changeSide = false;
    }

    protected override float GetMaxSpeedX()
    {
        return PlayerContext.sharpMaxSpeedX;
    }

    protected override float GetTurnTime()
    {
        return PlayerContext.sharpTurnTime;
    }
}
