using System;
using Snowlers.Input;
using TMPro;
using UnityEngine;
using Zenject;

namespace Snowlers.Game.Player
{
    public class ScenePlayer : MonoBehaviour
    {
        private IPlayerMover m_playerMover;
        private IInputService m_inputService;

        [SerializeField] private TextMeshProUGUI m_diedLabel;
        [SerializeField] private float m_shiftOriginThreshold;
        [SerializeField] private TrailRenderer m_trailRenderer;
        [SerializeField] private SpriteRenderer m_bodySprite;

        [Inject]
        private void Construct(IPlayerMover playerMover, IInputService inputService)
        {
            m_playerMover = playerMover;
            m_playerMover.SetPlayer(transform);
            
            m_inputService = inputService;
            m_inputService.Enable();
            m_inputService.Disable(EActions.Gameplay);
            m_inputService.Enable(EActions.Menu);
            m_inputService.OnTap += OnTap;
        }

        public void SetSkin(Sprite skin)
        {
            m_bodySprite.sprite = skin;
        }

        private void OnEnable()
        {
            m_playerMover.OnShiftOrigin += OnShiftOriginCallback;
        }

        private void OnDisable()
        {
            m_playerMover.OnShiftOrigin -= OnShiftOriginCallback;
        }

        private void OnDestroy()
        {
            m_inputService.OnTap -= OnTap;
        }

        private void OnTap()
        {
            m_inputService.Disable(EActions.Menu);
            m_inputService.Enable(EActions.Gameplay);
            m_playerMover.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            m_diedLabel.gameObject.SetActive(true);
            m_inputService.Disable();
            m_playerMover.SetActive(false);
        }

        private void OnShiftOriginCallback(float distanceToShift)
        {
            Vector3 shiftVector = Vector3.up * distanceToShift;
            
            Vector3[] positions = new Vector3[m_trailRenderer.positionCount];
            m_trailRenderer.GetPositions(positions);

            for (var i = 0; i < positions.Length; i++)
            {
                positions[i] += shiftVector;
            }

            m_trailRenderer.SetPositions(positions);
            
            transform.position += shiftVector;
        }
    }
}