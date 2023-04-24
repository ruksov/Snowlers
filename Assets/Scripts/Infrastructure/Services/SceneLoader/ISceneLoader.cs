using System;

namespace Snowlers.Infrastructure.Services.SceneLoader
{
  public interface ISceneLoader
  {
    void Load(string sceneName, Action onLoaded = null);
  }
}