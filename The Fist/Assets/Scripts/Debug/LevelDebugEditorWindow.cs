using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using Bitbox.Core.Eventing;
using Bitbox.Core.Types;
using Bitbox.Eventing.Debug;
using Bitbox.Core.Types.Enums;
using Bitbox.Core.Events.Global;
using UnityEngine.Assertions;

public class LevelDebugEditorWindow : EditorWindow
{
    private string[] _playModeSceneNames = new string[] {
        "MainMenu", "Sandbox"};
    private string[] _debugSceneNames = new string[] {
        "MainMenu", "Sandbox", "Player", "Systems" };
    private string[] _commandNames = new string[] {
        "Enemies Passive", "Player Invincible", "Kill All Enemies" };

    private int _selectedCommandIndex = 0;

    private Vector2 _scroll;
    private int _selectedSceneIndex = 0;

    [MenuItem("Tools/Level Debug")]
    public static void ShowWindow()
    {
        GetWindow<LevelDebugEditorWindow>("Level Debug");
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Debug Editor", EditorStyles.boldLabel);

        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        if (GUILayout.Button("Play Root Scene"))
        {
            LoadBootstrapAndPlay();
        }

        GUILayout.Space(10);

        GenerateSceneLoadingDebugUi();

        GUILayout.Space(10);

        GenerateSceneLoadingPlayModeUi();

        GUILayout.Space(10);

        LoadInGameCommands();

        EditorGUILayout.EndScrollView();
    }

    private void GenerateSceneLoadingPlayModeUi()
    {
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        GUILayout.Label("Load Scene (Play Mode)", EditorStyles.label);

        EditorGUILayout.BeginHorizontal();
        _selectedSceneIndex = EditorGUILayout.Popup(_selectedSceneIndex, _playModeSceneNames);
        if (GUILayout.Button("Load Scene"))
        {
            LoadSelectedSceneWhilePlaying(_playModeSceneNames[_selectedSceneIndex]);
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }

    private void GenerateSceneLoadingDebugUi()
    {
        GUILayout.Label("Load Scene (Editor)", EditorStyles.label);

        EditorGUILayout.BeginHorizontal();
        _selectedSceneIndex = EditorGUILayout.Popup(_selectedSceneIndex, _debugSceneNames);

        if (!GUILayout.Button("Load Scene"))
        {
            EditorGUILayout.EndHorizontal();
            return;
        }

        switch (_debugSceneNames[_selectedSceneIndex])
        {
            case "MainMenu":
                EditorSceneManager.OpenScene("Assets/Scenes/" + Scenes.MainMenu + ".unity", OpenSceneMode.Single);
                break;
            case "Sandbox":
                EditorSceneManager.OpenScene("Assets/Scenes/" + Scenes.Sandbox + ".unity", OpenSceneMode.Single);
                break;
            case "Player":
                EditorSceneManager.OpenScene("Assets/Scenes/" + Scenes.Player + ".unity", OpenSceneMode.Single);
                break;
            case "Systems":
                EditorSceneManager.OpenScene("Assets/Scenes/" + Scenes.Systems + ".unity", OpenSceneMode.Single);
                break;
            default:
                Debug.LogWarning("Unknown scene name: " + _playModeSceneNames[_selectedSceneIndex]);
                break;
        }

        EditorGUILayout.EndHorizontal();
    }

    private void LoadBootstrapAndPlay()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return;
        }

        EditorSceneManager.OpenScene("Assets/Scenes/" + Scenes.Bootstrap + ".unity", OpenSceneMode.Single);

        EditorApplication.delayCall += () =>
        {
            if (!EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = true;
            }
        };
    }

    private void LoadInGameCommands()
    {
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        GUILayout.Label("Game Events", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        _selectedCommandIndex = EditorGUILayout.Popup(_selectedCommandIndex, _commandNames);
        if (GUILayout.Button("Fire Command"))
        {
            switch (_commandNames[_selectedCommandIndex])
            {
                case "Enemies Passive":
                    MakeAllEnemiesPassive();
                    break;
                case "Player Invincible":
                    MakePlayerInvincible();
                    break;
                case "Kill All Enemies":
                    KillAllEnemies();
                    break;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUI.EndDisabledGroup();
    }

    private void MakeAllEnemiesPassive()
    {
        GetGlobalMessageBus()?.Publish(new EnemiesPassiveEvent());
    }

    private void LoadSelectedSceneWhilePlaying(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                GetGlobalMessageBus()?.Publish(new LoadMacroSceneEvent()
                {
                    SceneType = MacroSceneType.MainMenu
                });
                break;
            case "Sandbox":
                GetGlobalMessageBus()?.Publish(new LoadMacroSceneEvent()
                {
                    SceneType = MacroSceneType.Sandbox
                });
                break;
            default:
                Debug.LogWarning("Unknown scene name: " + sceneName);
                break;
        }
    }

    private void MakePlayerInvincible()
    {
        GetSceneMessageBus()?.Publish(new PlayerInvincibleEvent());
        Debug.Log("Player is now invincible.");
    }

    private void KillAllEnemies()
    {
        GetSceneMessageBus()?.Publish(new KillAllEnemiesEvent());
    }

    private MessageBus GetSceneMessageBus()
    {
        var messageBus = GameObject.FindWithTag(Tags.SceneMessageBus)?.GetComponent<MessageBus>();
        Assert.IsNotNull(messageBus, "Scene Message Bus not found.");
        return messageBus;
    }

    private MessageBus GetGlobalMessageBus()
    {
        var messageBus = GameObject.FindWithTag(Tags.GameController)?.GetComponent<MessageBus>();
        Assert.IsNotNull(messageBus, "Global Message Bus not found.");
        return messageBus;
    }
}
