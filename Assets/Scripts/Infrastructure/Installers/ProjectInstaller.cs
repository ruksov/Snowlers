using Snowlers.Infrastructure.GameStates.Factory;
using Snowlers.Infrastructure.GameStates.Machine;
using Snowlers.Infrastructure.GameStates.States;
using Snowlers.Infrastructure.Services.Input;
using Snowlers.Infrastructure.Services.SceneLoader;
using Zenject;

namespace Snowlers.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindServices();

            BindGameStates();
            BindGameStateMachineWithFactory();
        }

        private void BindServices()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this);
            Container.Bind<IInputService>().To<DefaultInputService>().AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
        }

        private void BindGameStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        private void BindGameStateMachineWithFactory()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle().NonLazy();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}
