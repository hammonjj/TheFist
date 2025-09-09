using System.Collections.Generic;
using Bitbox.Core.Utilities;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Bitbox.Core
{
  public class PlayerInputManager : MonoBehaviourBase
  {
    private UnityEngine.InputSystem.PlayerInputManager _playerInputManager;

    public IReadOnlyList<PlayerInput> PlayerInputs => _playerInputs.AsReadOnly();
    private List<PlayerInput> _playerInputs = new List<PlayerInput>();

    protected override void OnAwakened()
    {
      _playerInputManager = GetComponent<UnityEngine.InputSystem.PlayerInputManager>();
      _playerInputManager.onPlayerJoined += OnPlayerJoined;
    }
        
        private void OnPlayerJoined(PlayerInput input)
        {
            Assert.IsFalse(
                _playerInputs.Contains(input),
                $"PlayerInput for player {input.playerIndex + 1} already exists.");

            _playerInputs.Add(input);
            input.gameObject.name = $"PlayerInput_{input.playerIndex}";
            input.gameObject.transform.SetParent(gameObject.transform);

            LogInfo($"Player {input.playerIndex} joined with scheme: {input.currentControlScheme}");
        }
  }
}