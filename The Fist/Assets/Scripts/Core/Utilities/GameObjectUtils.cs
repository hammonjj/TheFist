using System;
using UnityEngine;

namespace Bitbox.Core.Utilities
{
  /// <summary>
  /// Utility methods for working with GameObjects and Transforms.
  /// </summary>
  public static class GameObjectUtils
  {
    public static GameObject FindChildWithTag(Transform parent, string tag, bool includeInactive = false)
    {
      if (parent == null || string.IsNullOrEmpty(tag))
      {
        return null;
      }

      var children = parent.GetComponentsInChildren<Transform>(includeInactive);
      foreach (Transform child in children)
      {
        if (child.CompareTag(tag))
        {
          return child.gameObject;
        }
      }

      return null;
    }

    public static Transform FindChildTransformWithTag(Transform parent, string tag, bool includeInactive = false)
    {
      if (parent == null || string.IsNullOrEmpty(tag))
      {
        return null;
      }

      foreach (Transform child in parent.GetComponentsInChildren<Transform>(includeInactive))
      {
        if (child.CompareTag(tag))
        {
          return child;
        }
      }

      return null;
    }

    public static int FindClosestTransformIndex(Transform source, Transform[] objects)
    {
      if (source == null || objects == null || objects.Length == 0)
      {
        return -1;
      }

      float closestDistance = float.MaxValue;
      int closestIndex = -1;

      for (int i = 0; i < objects.Length; i++)
      {
        if (objects[i] == null)
        {
          continue;
        }

        float distance = Vector3.Distance(source.position, objects[i].position);
        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestIndex = i;
        }
      }

      return closestIndex;
    }

    public static T FindComponentInParents<T>(GameObject startingPoint)
    {
      Transform current = startingPoint.transform.parent;

      while (current != null)
      {
        T comp = current.GetComponent<T>();
        if (comp != null)
        {
          return comp;
        }

        current = current.parent;
      }

      return default;
    }

    public static GameObject? FindGameObjectInChildrenByName(Transform parent, string name, bool includeInactive = false)
    {
      if (parent == null || string.IsNullOrEmpty(name))
      {
        return null;
      }

      foreach (Transform child in parent)
      {
        if (!includeInactive && !child.gameObject.activeSelf)
        {
          continue;
        }

        if (child.name.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
          return child.gameObject;
        }

        var result = FindGameObjectInChildrenByName(child, name, includeInactive);
        if (result != null)
        {
          return result;
        }
      }

      return null;
    }

  }
}