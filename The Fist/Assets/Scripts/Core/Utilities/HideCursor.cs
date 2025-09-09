using Bitbox.Core.Utilities;
using UnityEngine;

namespace BitBox.Core.Utilities
{
    public class HideCursor : MonoBehaviourBase
    {
        protected override void OnAwakened()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnDestroy()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}