using System.Diagnostics.CodeAnalysis;
using Snowlers.Game.Player;
using UnityEngine;
using Zenject;

namespace Snowlers.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        private IPlayerMoveService m_playerMoveService;

        [Inject]
        private void Construct(IPlayerMoveService playerMoveService)
        {
            m_playerMoveService = playerMoveService;
        }
        
        private void Update()
        {
            Vector3 moveDirection = m_playerMoveService.Velocity.normalized;
            if (moveDirection == Vector3.zero) 
                return;
            
            float angle = Mathf.Atan2(moveDirection.x, -moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

