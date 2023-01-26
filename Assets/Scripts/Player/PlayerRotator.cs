using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Snowlers.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private PlayerMover m_mover;

        private void Update()
        {
            Vector3 moveDirection = m_mover.MoveDirection;
            if (moveDirection == Vector3.zero) 
                return;
            
            float angle = Mathf.Atan2(moveDirection.x, -moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

