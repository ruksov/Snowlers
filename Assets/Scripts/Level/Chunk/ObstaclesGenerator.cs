using System.Collections.Generic;
using Snowlers.Common.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Snowlers.Level.Chunk
{
    public class ObstaclesGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("m_chunk")] [SerializeField] private SceneChunk sceneChunk;
        
        private const string m_kObstaclesContainerName = "Obstacles";

        private GameObject m_obstaclesContainer;
        private readonly List<Vector2Int> m_occupiedPositions = new();

        private ChunkData ChunkData => sceneChunk.Data;

        private void OnEnable()
        {
            CreateContainerObject();
            GenerateObstacles();
        }

        private void OnDisable()
        {
            DestroyContainerObject();
        }
        
        private void GenerateObstacles()
        {
            int obstaclesCount = Mathf.FloorToInt((ChunkData.size.x * ChunkData.size.y) * ChunkData.obstaclesCountRatio);
            
            for (int i = 0; i < obstaclesCount; ++i)
            {
                Vector2Int freePosition = GenerateRandomFreePosition();
                m_occupiedPositions.Add(freePosition);

                Instantiate(ChunkData.obstaclePrefab, m_obstaclesContainer.transform)
                    .With(_obstacle => _obstacle.transform.localPosition = new Vector3(freePosition.x, freePosition.y));
            }
        }

        private void CreateContainerObject()
        {
            m_obstaclesContainer = new GameObject(m_kObstaclesContainerName)
                .With(_go => _go.transform.SetParent(transform, false))
                .With(_go => _go.transform.localPosition += Vector3.left * ChunkData.size.x * 0.5f);
        }
        
        private void DestroyContainerObject()
        {
            Destroy(m_obstaclesContainer);
        }

        private Vector2Int GenerateRandomFreePosition()
        {
            Vector2Int freePosition = Vector2Int.zero;
            
            do
            {
                freePosition.x = Random.Range(0, ChunkData.size.x);
                freePosition.y = -Random.Range(0, ChunkData.size.y);
            } 
            while (m_occupiedPositions.Contains(freePosition));

            return freePosition;
        }
    }
}
