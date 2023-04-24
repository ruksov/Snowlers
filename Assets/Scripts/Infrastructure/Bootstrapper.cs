using Snowlers.Infrastructure.GameStates.Machine;
using Snowlers.Infrastructure.GameStates.States;
using UnityEngine;
using Zenject;

namespace Snowlers.Infrastructure
{
  public class Bootstrapper : MonoBehaviour
  {
    public string StartSceneName;
    
    private IGameStateMachine m_stateMachine;

    [Inject]
    public void Construct(IGameStateMachine stateMachine) => 
      m_stateMachine = stateMachine;

    private void Start() => 
      m_stateMachine.Enter<LoadLevelState, string>(StartSceneName);
  }
}