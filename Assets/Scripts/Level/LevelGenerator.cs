using System.Collections.Generic;
using Snowlers.Common.Extensions;
using Snowlers.Game.Player;
using Snowlers.Level.Chunk;
using UnityEngine;

namespace Snowlers.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private ScenePlayer m_player;
        [SerializeField] private float m_playerOffset;
        [SerializeField] private SceneChunk m_chunkPrefab;
        [SerializeField] private uint m_chunksCount = 3;

        private const string m_kChunksContainerName = "Chunks";
        
        private GameObject m_chunksContainer;
        private float m_nextSpawnY;

        private readonly Queue<SceneChunk> m_chunks = new();

        private void OnEnable()
        {
            CreateChunksContainer();
            GenerateChunks();

            m_player.OnShiftOrigin += OnShiftOrigin;
        }

        private void OnShiftOrigin(float distanceToShift)
        {
            m_nextSpawnY -= distanceToShift;
            foreach (SceneChunk chunk in m_chunks)
            {
                chunk.transform.position += Vector3.up * distanceToShift;
            }
        }

        private void OnDisable()
        {
            DestroyChunksContainer();
        }

        private void Update()
        {
            if (m_chunks.Count == 0)
                return;
            
            SceneChunk firstChunk = m_chunks.Peek();
            float thresholdPlayerY = firstChunk.transform.position.y - firstChunk.Data.size.y - m_playerOffset;
            
            if (m_player.transform.position.y < thresholdPlayerY)
            {
                Destroy(firstChunk.gameObject);
                m_chunks.Dequeue();
                
                m_chunks.Enqueue(CreateChunk());
            }
        }

        private void CreateChunksContainer()
        {
            m_chunksContainer = new GameObject(m_kChunksContainerName)
                .With(_go => _go.transform.SetParent(transform, false));;
        }

        private void DestroyChunksContainer()
        {
            m_chunks.Clear();
            Destroy(m_chunksContainer);
        }

        private void GenerateChunks()
        {
            m_nextSpawnY = 0.0f;
            
            for (uint i = 0; i < m_chunksCount; ++i)
            {
                m_chunks.Enqueue(CreateChunk());
            }
        }

        private SceneChunk CreateChunk()
        {
            return Instantiate(m_chunkPrefab, m_chunksContainer.transform)
                .With(chunk => chunk.transform.localPosition = Vector3.down * m_nextSpawnY)
                .With(chunk => m_nextSpawnY += chunk.Data.size.y);
        }
    }
}