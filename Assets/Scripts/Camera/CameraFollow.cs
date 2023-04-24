using UnityEngine;

namespace Snowlers.Camera
{
  public class CameraFollow : MonoBehaviour
  {
    public Vector3 Offset;
    public Transform Following;

    private void LateUpdate()
    {
      if (Following == null)
        return;

      transform.position = NewPosition();
    }

    private Vector3 NewPosition()
    {
      Vector3 newPosition = transform.position;
      newPosition.y = Following.position.y;
      newPosition += Offset;
      return newPosition;
    }
  }
}