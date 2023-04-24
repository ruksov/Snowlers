using Snowlers.Infrastructure.GameStates.Factory;

namespace Snowlers.Infrastructure.GameStates.Machine
{
  public interface IGameStateMachine
  {
    void Enter<TState>() where TState : IExitableGameState;
    void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadGameState<TPayload>;
  }
}