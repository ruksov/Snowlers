using System;
using UnityEngine.InputSystem;

namespace Snowlers.Infrastructure.Input
{
    public class DefaultInputService : IInputService, GameInput.IGameActions
    {
        public event Action OnTap;

        private readonly GameInput m_gameInput;

        public DefaultInputService()
        {
            m_gameInput = new();
            m_gameInput.Game.SetCallbacks(this);
        }
        
        public void Enable()
        {
            m_gameInput.Enable();
        }

        public void Disable()
        {
            m_gameInput.Disable();
        }

        void GameInput.IGameActions.OnTap(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnTap?.Invoke();
        }
    }
}



