using UnityEngine.VFX;

namespace Bitbox.Core.Extensions
{
  public static class VisualEffectExtensions
  {
    public static void SafePlay(this VisualEffect vfx)
    {
      if (vfx != null)
      {
        vfx.Play();
      }
    }

    public static void SafeStop(this VisualEffect vfx)
    {
      if (vfx != null)
      {
        vfx.Stop();
      }
    }

    public static void SafeSetBool(this VisualEffect vfx, string name, bool value)
    {
      if (vfx != null && vfx.HasBool(name))
      {
        vfx.SetBool(name, value);
      }
    }

    public static void SafeSendEvent(this VisualEffect vfx, string eventName)
    {
      if (vfx != null)
      {
        vfx.SendEvent(eventName);
      }
    }

    public static void SafeSetFloat(this VisualEffect vfx, string name, float value)
    {
      if (vfx != null && vfx.HasFloat(name))
      {
        vfx.SetFloat(name, value);
      }
    }
  }
}