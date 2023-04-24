using System;
using System.Collections;
using Snowlers.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snowlers.Infrastructure.Services.SceneLoader
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner m_coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) => 
      m_coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null) =>
      m_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    
    private static IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}