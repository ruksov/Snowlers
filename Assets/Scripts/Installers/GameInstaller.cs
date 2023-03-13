using System;
using Snowlers.Game.Player;
using UnityEngine;
using Zenject;

namespace Snowlers.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform m_playerStart;
        [SerializeField] private Game.Player.PlayerSettings m_playerSettings;
        
        public override void InstallBindings()
        {
            InstallPlayer();
        }

        private void InstallPlayer()
        {
            Container
                .Bind<IPlayerState>()
                .To<PlayerState>()
                .AsSingle();

            Container
                .Bind<PlayerSettings>()
                .FromInstance(m_playerSettings)
                .AsSingle();

            Container
                .Bind(typeof(IPlayerMover), typeof(ITickable), typeof(IDisposable))
                .To<PlayerMover>()
                .AsSingle();
        }
    }
}
