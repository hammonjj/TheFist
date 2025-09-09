using UnityEngine;

namespace Bitbox.Core.Extensions
{
  public static class ParticleSystemExtensions
  {
    public static void SafePlay(this ParticleSystem ps, bool withChildren = true)
    {
      if (ps != null)
      {
        ps.Play(withChildren);
      }
    }

    public static void SafeStop(this ParticleSystem ps, bool withChildren = true, ParticleSystemStopBehavior stopBehavior = ParticleSystemStopBehavior.StopEmitting)
    {
      if (ps != null)
      {
        ps.Stop(withChildren, stopBehavior);
      }
    }
  }
}