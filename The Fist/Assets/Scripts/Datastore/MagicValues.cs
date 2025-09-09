using UnityEngine;

namespace Bitbox.Core.Datastore
{
  /// <summary>
  ///   A static class to hold magic values and PlayerPrefs accessors.
  /// </summary>
  public static class MagicValues
  {
    // Magic Values
    public static string GameVersion { get; } = Application.version;

    public static float EnemySpawnDelay = 2.0f;
    public static float TimeDelayBeforeEnablingPlayer = 2.0f;

    // PlayerPrefs
    private const string InstallIdKey = "install_id";

    public static string InstallId
    {
      get
      {
#if UNITY_EDITOR
      return "3393a9fc-bd7c-49c6-83d6-5d3d7457e4a2";
#else
        if (!PlayerPrefs.HasKey(InstallIdKey))
        {
          SetString(InstallIdKey, UuidGenerator.GenerateUUID());
        }

        return GetString(InstallIdKey);
#endif
      }
    }

    private static void SetString(string key, string value)
    {
      PlayerPrefs.SetString(key, value);
      PlayerPrefs.Save();
    }

    private static string GetString(string key, string defaultValue = "")
    {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : defaultValue;
    }

    private static void SetInt(string key, int value)
    {
      PlayerPrefs.SetInt(key, value);
      PlayerPrefs.Save();
    }

    private static int GetInt(string key, int defaultValue = 0)
    {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : defaultValue;
    }

    private static void SetFloat(string key, float value)
    {
      PlayerPrefs.SetFloat(key, value);
      PlayerPrefs.Save();
    }

    private static float GetFloat(string key, float defaultValue = 0f)
    {
      return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
    }

    private static void DeleteKey(string key)
    {
      if (PlayerPrefs.HasKey(key))
      {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
      }
    }

    public static void ClearAll()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}