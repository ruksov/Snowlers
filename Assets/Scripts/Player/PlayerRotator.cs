using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerRotator : MonoBehaviour
{
    private PlayerMover m_mover;

    private void Awake()
    {
        m_mover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        Vector3 moveDirection = m_mover.MoveDirection;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.x, -moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

