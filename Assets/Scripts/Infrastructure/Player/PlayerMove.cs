using Snowlers.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Snowlers.Infrastructure.Player
{
  public class PlayerMove : MonoBehaviour
  {
    public float MoveSpeedY;
    public float MoveSpeedX;
    
    private IInputService m_input;
    private bool m_moveToRight;
    private Vector3 m_moveVector;

    [Inject]
    public void Construct(IInputService input)
    {
      m_input = input;
      m_input.OnTap += ChangeSide;
    }

    private void Awake()
    {
      m_moveToRight = GenerateStartMoveSide();
      m_moveVector = CreateMoveVector();
    }

    private void Update()
    {
      transform.position += m_moveVector * Time.deltaTime;
    }

    private void ChangeSide()
    {
      m_moveToRight = !m_moveToRight;
      m_moveVector = CreateMoveVector();
    }

    private Vector3 CreateMoveVector()
    {
      return new Vector3()
      {
        x = m_moveToRight ? MoveSpeedX : -MoveSpeedX,
        y = -MoveSpeedY,
        z = 0
      };
    }

    private static bool GenerateStartMoveSide() => 
      Random.Range(0, 2) == 0;
  }
}