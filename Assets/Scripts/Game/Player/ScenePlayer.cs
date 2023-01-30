using Snowlers.Input;
using TMPro;
using UnityEngine;
using Zenject;

namespace Snowlers.Game.Player
{
    public class ScenePlayer : MonoBehaviour
    {
        private IPlayerState m_playerState;
        private IPlayerMoveService m_playerMoveService;
        private IInputService m_inputService;

        [SerializeField] private TextMeshProUGUI m_diedLabel;

        [Inject]
        private void Construct(IPlayerState playerState, IPlayerMoveService playerMoveService, IInputService inputService)
        {
            m_playerState = playerState;
            m_playerState.State = EPlayerState.Idle;
            
            m_playerMoveService = playerMoveService;
            
            m_inputService = inputService;
            m_inputService.Enable();
            m_inputService.Disable(EActions.Gameplay);
            m_inputService.Enable(EActions.Menu);
            m_inputService.OnTap += OnTap;
        }

        private void OnDestroy()
        {
            m_inputService.OnTap -= OnTap;
        }
        
        private void Update()
        {
            transform.position += m_playerMoveService.Velocity * Time.deltaTime;
        }
        
        private void OnTap()
        {
            m_inputService.Disable(EActions.Menu);
            m_inputService.Enable(EActions.Gameplay);
            
            m_playerState.State = EPlayerState.Move;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            m_diedLabel.gameObject.SetActive(true);
            m_inputService.Disable();
            m_playerState.State = EPlayerState.Dead;
        }
    }
}