using Snowlers.Common.Extensions;
using UnityEngine;

namespace Snowlers.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Chunk.Chunk m_chunkPrefab;
        [SerializeField] private uint m_chunksCount = 5;

        private const string m_kChunksContainerName = "Chunks";
        
        private GameObject m_chunksContainer;
        private float m_nextSpawnY;

        private void OnEnable()
        {
            CreateChunksContainer();
            GenerateChunks();
        }

        private void OnDisable()
        {
            DestroyChunksContainer();
        }

        private void CreateChunksContainer()
        {
            m_chunksContainer = new GameObject(m_kChunksContainerName)
                .With(_go => _go.transform.SetParent(transform, false));;
        }

        private void DestroyChunksContainer()
        {
            Destroy(m_chunksContainer);
        }

        private void GenerateChunks()
        {
            m_nextSpawnY = 0.0f;
            
            for (uint i = 0; i < m_chunksCount; ++i)
            {
                Instantiate(m_chunkPrefab, m_chunksContainer.transform)
                    .With(_chunk => _chunk.transform.localPosition = Vector3.down * m_nextSpawnY)
                    .With(_chunk => m_nextSpawnY += _chunk.Data.size.y);
            }
        }
    }
}