using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3Variable m_moveVector;

    public void OnChangeDirection()
    {
        Vector3 newMoveVector = m_moveVector.Value;
        newMoveVector.x = -newMoveVector.x;
        m_moveVector.SetValue(newMoveVector);
    }
}
