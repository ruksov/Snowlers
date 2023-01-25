using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerMover))]
public class ObstaclesTrigger : MonoBehaviour
{
    private PlayerMover m_mover;

    private void Awake()
    {
        m_mover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_mover.StopMove();
    }
}
