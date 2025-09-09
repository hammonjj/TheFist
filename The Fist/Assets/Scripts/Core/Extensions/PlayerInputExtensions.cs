using UnityEngine.InputSystem;

namespace Bitbox.Core.Extensions
{
  public static class PlayerInputExtensions
  {
    /// <summary>
    /// Returns true if the given action‐map is currently the active map on this PlayerInput.
    /// </summary>
    public static bool IsMapEnabled(this PlayerInput playerInput, string mapName)
    {
      // avoid null‐refs:
      if (playerInput == null || playerInput.currentActionMap == null)
      {
        return false;
      }

      return playerInput.currentActionMap.name == mapName;
    }

    /// <summary>
    /// Enables the given action‐map and disables any other on the same PlayerInput.
    /// (Equivalent to playerInput.SwitchCurrentActionMap(mapName), but by name.)
    /// </summary>
    public static void SwitchToMap(this PlayerInput playerInput, string mapName)
    {
      if (playerInput == null) { return; }

      var map = playerInput.actions.FindActionMap(mapName);
      if (map != null)
      {
        map.Enable();
      }
    }
  }
}