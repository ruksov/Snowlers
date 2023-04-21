using Snowlers.Infrastructure.GameStates.Factory;
using UnityEngine;

namespace Snowlers.Infrastructure.GameStates.States
{
  public class GameLoopState : IGameState
  {
    public void Enter()
    {
      Debug.Log("Enter to Game loop state");
    }

    public void Exit()
    {
    }
  }
}