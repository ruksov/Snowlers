using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerContext m_playerContext = default;

    private PlayerStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine = new PlayerStateMachine(m_playerContext);
    }

    private void Update()
    {
        m_stateMachine.Update();
        Move();
    }

    private void Move()
    {
        transform.position += m_playerContext.moveVector * Time.deltaTime;
    }
}
