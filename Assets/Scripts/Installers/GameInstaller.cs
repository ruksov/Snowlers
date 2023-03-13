using System;
using Snowlers.Common;
using Snowlers.Game.Player;
using Snowlers.Game.Player.Factory;
using UnityEngine;
using Zenject;

namespace Snowlers.Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Transform m_playerStart;
        [SerializeField] private ObjectFollower m_camera;
        [SerializeField] private PlayerSettings m_playerSettings;
        
        public override void InstallBindings()
        {
            InstallInstaller_Hack();
            InstallPlayer();
        }

        private void InstallInstaller_Hack()
        {
            Container
                .BindInterfacesAndSelfTo<GameInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void InstallPlayer()
        {
            m_playerSettings.spawnPoint = m_playerStart;
            m_playerSettings.camera = m_camera;
            
            Container
                .Bind<PlayerSettings>()
                .FromInstance(m_playerSettings)
                .AsSingle();

            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind(typeof(IPlayerMover), typeof(ITickable), typeof(IDisposable))
                .To<PlayerMover>()
                .AsSingle();
        }

        public void Initialize()
        {
            var playerFactory = Container.Resolve<PlayerFactory>();
            playerFactory.Create();
        }
    }
}
