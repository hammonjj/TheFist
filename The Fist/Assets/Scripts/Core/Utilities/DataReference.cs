using UnityEngine;

namespace Bitbox.Core.Utilities
{
  public class DataReference<T> : MonoBehaviourBase
  {
    [SerializeField] private T _data;

    public T Data => _data;
  }

}