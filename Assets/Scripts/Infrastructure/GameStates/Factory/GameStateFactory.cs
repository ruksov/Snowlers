using Zenject;

namespace Snowlers.Infrastructure.GameStates.Factory
{
  public class GameStateFactory : IGameStateFactory
  {
    private readonly DiContainer m_container;

    public GameStateFactory(DiContainer container) => 
      m_container = container;

    public IExitableGameState State<TState>() where TState : IExitableGameState => 
      m_container.Resolve<TState>();
  }
}