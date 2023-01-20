using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    // Events
    public UnityEvent changeDirectionEvent;
    public UnityEvent sharpChangeDirectionEvent;

    // Members
    private GameInput m_gameInput;

    private void OnEnable()
    {
        if(m_gameInput == null)
        {
            m_gameInput = new();
            m_gameInput.Gameplay.SetCallbacks(this);
        }

        m_gameInput.Enable();
    }

    private void OnDisable()
    {
        m_gameInput.Disable();
    }

    public void OnChangeDirection(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started &&
            changeDirectionEvent != null)
        {
            changeDirectionEvent.Invoke();
        }
        else if(context.phase == InputActionPhase.Performed)
        {
            sharpChangeDirectionEvent.Invoke();
        }
    }
}
