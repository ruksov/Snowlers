using System;
using Snowlers.Game.Player;
using Snowlers.Game.Player.Factory;
using Snowlers.Game.Player.Movement;
using UnityEngine;
using Zenject;

namespace Snowlers.Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameSettings settings;
 
        [Serializable]
        public class GameSettings
        {
            public PlayerFactory.Settings playerFactorySettings;
            public ScenePlayer.Settings scenePlayerSettings;
            public PlayerMover.Settings playerMoverSettings;
        }
 
        public override void InstallBindings()
        {
            Container.BindInstance(settings.playerFactorySettings);
            Container.BindInstance(settings.scenePlayerSettings);
            Container.BindInstance(settings.playerMoverSettings);
        }
    }
}