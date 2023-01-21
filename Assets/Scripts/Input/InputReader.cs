using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    // Events
    [SerializeField] private VoidEventChannelSO m_turnChannel = default;
    [SerializeField] private VoidEventChannelSO m_sharpTurn = default;

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

    public void OnTurn(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started &&
            m_turnChannel != null)
        {
            m_turnChannel.RaiseEvent();
        }
        else if(context.phase == InputActionPhase.Performed &&
            m_sharpTurn != null)
        {
            m_sharpTurn.RaiseEvent();
        }
    }
}
