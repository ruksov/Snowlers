using System;
using UnityEngine.InputSystem;

namespace Snowlers.Input
{
    public class DefaultInputService : IInputService, GameInput.IGameplayActions, GameInput.IMenuActions
    {
        public event Action OnTap;
        public event Action OnStartTurn;
        public event Action OnStopTurn;

        private readonly GameInput m_gameInput;

        public DefaultInputService()
        {
            m_gameInput = new();
            m_gameInput.Menu.SetCallbacks(this);
            m_gameInput.Gameplay.SetCallbacks(this);
        }
        
        public void Enable()
        {
            m_gameInput.Enable();
        }

        public void Disable()
        {
            m_gameInput.Disable();
        }

        public void Enable(EActions actions)
        {
            switch (actions)
            {
                case EActions.Menu:
                    m_gameInput.Menu.Enable();
                    break;
                
                case EActions.Gameplay:
                    m_gameInput.Gameplay.Enable();
                    break;
            }
        }
        
        public void Disable(EActions actions)
        {
            switch (actions)
            {
                case EActions.Menu:
                    m_gameInput.Menu.Disable();
                    break;
                
                case EActions.Gameplay:
                    m_gameInput.Gameplay.Disable();
                    break;
            }
        }

        void GameInput.IGameplayActions.OnStartTurn(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnStartTurn?.Invoke();
        }

        void GameInput.IGameplayActions.OnStopTurn(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnStopTurn?.Invoke();
        }
        
        void GameInput.IMenuActions.OnTap(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnTap?.Invoke();
        }
    }
}



