using UnityEngine;

public enum ESide
{
    Left,
    Right
}

[CreateAssetMenu]
public class PlayerContext : ScriptableObject
{
    // Config
    public float speedY = 5.0f;
    public float maxSpeedX = 5.0f;
    public float turnTime = 1.0f;
    public float sharpMaxSpeedX = 10.0f;
    public float sharpTurnTime = 0.5f;

    // Player related event channels
    public VoidEventChannelSO turnChannel;
    public VoidEventChannelSO sharpTurnChannel;

    // Runtime
    public Vector3 moveVector = Vector3.zero;
    public ESide side = ESide.Right;
}
