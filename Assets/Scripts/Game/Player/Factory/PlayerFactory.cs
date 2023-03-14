using System;
using UnityEngine;
using Zenject;

namespace Snowlers.Game.Player.Factory
{
    public class PlayerFactory
    {
        [Serializable]
        public class Settings
        {
            public ScenePlayer playerPrefab;
        }
        
        private readonly StartPoint m_startPoint;
        private readonly Settings m_settings;
        private readonly DiContainer m_diContainer;

        public PlayerFactory(StartPoint startPoint, Settings settings, DiContainer diContainer)
        {
            m_startPoint = startPoint;
            m_settings = settings;
            m_diContainer = diContainer;
        }

        public void Create()
        {
            ScenePlayer scenePlayer = m_diContainer
                .InstantiatePrefabForComponent<ScenePlayer>(
                    m_settings.playerPrefab, 
                    m_startPoint.transform.position, 
                    Quaternion.identity, 
                    null);
            
            m_startPoint.CameraFollower.SetObject(scenePlayer.transform);
        }
    }
}