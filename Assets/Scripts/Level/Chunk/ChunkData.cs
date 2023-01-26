using UnityEngine;

namespace Snowlers.Level.Chunk
{
    [CreateAssetMenu(menuName = "Game/ChunkData", fileName = "ChunkData", order = 0)]
    public class ChunkData : ScriptableObject
    {
        public Vector2Int size;
        public GameObject obstaclePrefab;
        public float obstaclesCountRatio;
    }
}