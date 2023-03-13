using Snowlers.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Snowlers.Game.Player
{
    [CreateAssetMenu(menuName = "Game/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public float velocityY = 5.0f;

        public float minVelocityX = 8.0f;
        public float maxVelocityX = 16.0f;
        public float accelerationX = 60.0f;

        [FormerlySerializedAs("originShiftThreshold")] public float shiftOriginThreshold = 100.0f;

        public Transform spawnPoint;
        public ObjectFollower camera;
        public Sprite skin;
        public ScenePlayer playerPrefab;
    }
}