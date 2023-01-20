using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3Reference m_moveVector = default;

    private void Update()
    {
        transform.position += m_moveVector.Value * Time.deltaTime;
    }
}
