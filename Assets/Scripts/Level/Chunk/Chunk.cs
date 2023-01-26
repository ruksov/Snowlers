using UnityEngine;

namespace Snowlers.Level.Chunk
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private ChunkData m_data = default;
        public ChunkData Data => m_data;
    }
}