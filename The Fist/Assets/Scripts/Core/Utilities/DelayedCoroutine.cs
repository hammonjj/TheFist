using System;
using System.Collections;
using UnityEngine;

// Usage:
// DelayedCoroutine.Run(
//     () => _sceneMessageBus.Publish(new BeginEncounterEvent()),
//     MagicValues.TimeDelayBeforeEnablingPlayer
// );
namespace Bitbox.Core.Utilities
{
  public static class DelayedCoroutine
  {
    private class DelayedActionRunner : MonoBehaviour { }

    private static DelayedActionRunner _runner;

    private static void EnsureRunnerExists()
    {
      if (_runner != null) { return; }

      var go = new GameObject("[DelayedActionRunner]");
      UnityEngine.Object.DontDestroyOnLoad(go);
      _runner = go.AddComponent<DelayedActionRunner>();
    }

    public static void Run(Action action, float delaySeconds, bool useUnscaledTime = true)
    {
      EnsureRunnerExists();
      _runner.StartCoroutine(RunRoutine(action, delaySeconds, useUnscaledTime));
    }

    private static IEnumerator RunRoutine(Action action, float delaySeconds, bool useRealtime)
    {
      if (useRealtime)
      {
        yield return new WaitForSecondsRealtime(delaySeconds);
      }
      else
      {
        yield return new WaitForSeconds(delaySeconds);
      }

      action?.Invoke();
    }
  }
}