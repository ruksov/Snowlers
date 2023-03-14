using System;
using Snowlers.Game.Player.Factory;
using Snowlers.Game.Player.Movement;
using Zenject;

namespace Snowlers.Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
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
            Container
                .Bind<StartPoint>()
                .FromComponentInHierarchy()
                .AsCached();

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
