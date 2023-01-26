using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snowlers.Level
{
    public class ObstaclesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject m_obstaclePrefab;
        [SerializeField] private float m_obstaclesCountRatio;
        [SerializeField] private Vector2Int m_chunkSize;
        
        private const string m_kContainerObjectName = "Obstacles";
        private GameObject m_containerObject;

        private readonly List<Vector2Int> m_occupiedPositions = new();
        
        private void OnEnable()
        {
            CreateContainerObject();
            GenerateObstacles();
        }

        private void OnDisable()
        {
            DestroyContainerObject();
        }
        
        public void GenerateObstacles()
        {
            int obstaclesCount = Mathf.FloorToInt((m_chunkSize.x * m_chunkSize.y) * m_obstaclesCountRatio);
            for (int i = 0; i < obstaclesCount; ++i)
            {
                Vector2Int freePosition = GenerateRandomFreePosition();
                m_occupiedPositions.Add(freePosition);
                
                GameObject obstacle = Instantiate(m_obstaclePrefab, m_containerObject.transform);
                obstacle.transform.localPosition = new Vector3(freePosition.x, freePosition.y);
            }
        }

        private void CreateContainerObject()
        {
            m_containerObject = new GameObject(m_kContainerObjectName);
            m_containerObject.transform.SetParent(transform, false);
            m_containerObject.transform.localPosition += Vector3.left * m_chunkSize.x * 0.5f;
        }
        
        private void DestroyContainerObject()
        {
            Destroy(m_containerObject);
        }

        private Vector2Int GenerateRandomFreePosition()
        {
            Vector2Int freePosition = Vector2Int.zero;
            
            do
            {
                freePosition.x = Random.Range(0, m_chunkSize.x);
                freePosition.y = -Random.Range(0, m_chunkSize.y);
            } 
            while (m_occupiedPositions.Contains(freePosition));

            return freePosition;
        }
    }
}
