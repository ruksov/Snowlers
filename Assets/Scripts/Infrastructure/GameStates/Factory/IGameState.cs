namespace Snowlers.Infrastructure.GameStates.Factory
{
  public interface IExitableGameState
  {
    void Exit();
  }

  public interface IGameState : IExitableGameState
  {
    void Enter();
  }

  public interface IPayloadGameState<TPayload> : IExitableGameState
  {
    void Enter(TPayload payload);
  }
}