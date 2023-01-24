using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DefaultInputService : IInputService, GameInput.IGameplayActions
{
    public event Action OnTap;
    public event Action OnTurn;
    public event Action OnSharpTurn;

    private GameInput m_gameInput;

    public DefaultInputService()
    {
        m_gameInput = new();
        m_gameInput.Gameplay.SetCallbacks(this);
        Enable();
    }

    public void Enable()
    {
        m_gameInput.Enable();
    }

    public void Disable()
    {
        m_gameInput.Disable();
    }

    void GameInput.IGameplayActions.OnTap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OnTap?.Invoke();
    }

    void GameInput.IGameplayActions.OnTurn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OnTurn?.Invoke();
    }

    void GameInput.IGameplayActions.OnSharpTurn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            OnSharpTurn?.Invoke();
    }
}



