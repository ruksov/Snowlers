using UnityEngine;

namespace Snowlers.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class ObstaclesTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerMover m_mover;

        private void OnTriggerEnter2D(Collider2D other)
        {
            m_mover.StopMove();
        }
    }
}
