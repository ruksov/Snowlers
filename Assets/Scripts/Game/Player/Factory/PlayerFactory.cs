using UnityEngine;
using Zenject;

namespace Snowlers.Game.Player.Factory
{
    public class PlayerFactory
    {
        private readonly PlayerSettings m_playerSettings; 
        private readonly DiContainer m_diContainer;

        public PlayerFactory(PlayerSettings playerSettings, DiContainer diContainer)
        {
            m_playerSettings = playerSettings;
            m_diContainer = diContainer;
        }

        public void Create()
        {
            GameObject playerObject = m_diContainer
                .InstantiatePrefab(
                    m_playerSettings.playerPrefab, 
                    m_playerSettings.spawnPoint.position, 
                    Quaternion.identity, 
                    null);
            
            ScenePlayer player = playerObject.GetComponent<ScenePlayer>();
            player.SetSkin(m_playerSettings.skin);
            
            m_playerSettings.camera.SetObject(player.transform);
        }
    }
}