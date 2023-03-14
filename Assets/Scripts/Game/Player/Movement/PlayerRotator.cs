using UnityEngine;
using Zenject;

namespace Snowlers.Game.Player.Movement
{
    public class PlayerRotator : MonoBehaviour
    {
        private IPlayerMover m_playerMover;

        [Inject]
        private void Construct(IPlayerMover playerMover)
        {
            m_playerMover = playerMover;
        }
        
        private void Update()
        {
            Vector3 moveDirection = m_playerMover.Velocity.normalized;
            if (moveDirection == Vector3.zero) 
                return;
            
            float angle = Mathf.Atan2(moveDirection.x, -moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

