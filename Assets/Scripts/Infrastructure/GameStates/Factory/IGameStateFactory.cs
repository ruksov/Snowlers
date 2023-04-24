namespace Snowlers.Infrastructure.GameStates.Factory
{
  public interface IGameStateFactory
  {
    IExitableGameState State<TState>() where TState : IExitableGameState;
  }
}