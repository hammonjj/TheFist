namespace Bitbox.Eventing.Player
{
  public struct PlayerDeathEvent { }
  public struct PlayerRespawnedEvent { }
  public struct PlayerHitEvent { }

  public struct PlayerMovementEvent
{
  public float HorizontalMovement;
  public float VerticalMovement;
}
}