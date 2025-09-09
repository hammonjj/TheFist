using Bitbox.Core.Eventing;
using Bitbox.Core.Types.Enums;

namespace Bitbox.Core.Events.Global
{
  public struct PauseGameEvent
  {
    public bool IsPaused;
  }

  public struct ReloadCurrentSceneEvent
  {
  }

  public struct QuitGameEvent
  {
  }

  public struct PlayerDieDeathEvent
  {
  }

  public struct SceneMessageBusUpdatedEvent
  {
    public MessageBus MessageBus;
  }

  public struct LoadMacroSceneEvent
  {
    public MacroSceneType SceneType;
  }
}