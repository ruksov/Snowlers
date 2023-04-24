using Snowlers.Infrastructure.GameStates.Factory;
using Snowlers.Infrastructure.GameStates.Machine;
using Snowlers.Infrastructure.Services.SceneLoader;

namespace Snowlers.Infrastructure.GameStates.States
{
  public class LoadLevelState : IPayloadGameState<string>
  {
    private readonly ISceneLoader m_sceneLoader;
    private readonly IGameStateMachine m_stateMachine;

    public LoadLevelState(ISceneLoader sceneLoader, IGameStateMachine stateMachine)
    {
      m_sceneLoader = sceneLoader;
      m_stateMachine = stateMachine;
    }

    public void Enter(string sceneName) => 
      m_sceneLoader.Load(sceneName, OnLoaded);

    public void Exit()
    {
    }

    private void OnLoaded() => 
      m_stateMachine.Enter<GameLoopState>();
  }
}