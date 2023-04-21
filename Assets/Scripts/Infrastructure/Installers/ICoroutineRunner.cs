using System.Collections;
using UnityEngine;

namespace Snowlers.Infrastructure.Installers
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}