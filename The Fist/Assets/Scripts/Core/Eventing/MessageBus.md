using System.Runtime.CompilerServices;
using Core;
using Core.Events.Celestrial;
using Unity.Assertions;
using UnityEngine;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    None
}

public abstract class MonoBehaviourBase : MonoBehaviour, Library.Library.Logging.ILogger
{
    [Header("Debug Settings")]
    [Tooltip("Enable to draw gizmos for this object.")]
    public bool ShowGizmos = false;

    [Tooltip("Minimum log level to output to the console.")]
    public LogLevel logLevel = LogLevel.Info;

    protected string _cachedObjectName;
    protected MessageBus _sceneMessageBus;
    protected MessageBus _globalMessageBus;
    private Library.Library.Logging.Logger _loggerInstance;

    private void Awake()
    {
        _cachedObjectName = gameObject.name;

        _sceneMessageBus = GameObject.FindWithTag(Objects.SceneMessageBus)?.GetComponent<MessageBus>();

        _loggerInstance = new Library.Library.Logging.Logger(
          gameObject.name,
          GetInstanceID(),
          () => logLevel
        );

        _globalMessageBus = GameObject.FindWithTag(Tags.GameController)?.GetComponent<MessageBus>();
        Assert.IsNotNull(_globalMessageBus, "Game Manager Message Bus not found!");

        OnAwakened();
    }

    private void OnEnable()
    {
        _globalMessageBus.Subscribe<SceneMessageBusUpdatedEvent>(OnSceneMessageBusUpdated);
        OnEnabled();
    }

    private void OnDisable()
    {
        _globalMessageBus.Unsubscribe<SceneMessageBusUpdatedEvent>(OnSceneMessageBusUpdated);
        OnDisabled();
    }

    private void Start()
    {
        OnStarted();
    }

    private void Update()
    {
        OnUpdated();
    }

    private void FixedUpdate()
    {
        OnFixedUpdated();
    }

    private void LateUpdate()
    {
        OnLateUpdated();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered(other);
    }

    protected virtual void OnAwakened() { }
    protected virtual void OnEnabled() { }
    protected virtual void OnStarted() { }
    protected virtual void OnUpdated() { }
    protected virtual void OnFixedUpdated() { }
    protected virtual void OnLateUpdated() { }
    protected virtual void OnDisabled() { }
    protected virtual void OnDestroyed() { }
    protected virtual void OnDrawnGizmos() { }
    protected virtual void OnTriggerEntered(Collider other) { }

    private void OnSceneMessageBusUpdated(SceneMessageBusUpdatedEvent @event)
    {
        _sceneMessageBus = @event.MessageBus;
    }

    protected void LogDebug(string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    ) => _loggerInstance.Debug(message, filePath, lineNumber);

    protected void LogInfo(string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    ) => _loggerInstance.Info(message, filePath, lineNumber);

    protected void LogWarning(string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    ) => _loggerInstance.Warning(message, filePath, lineNumber);

    protected void LogError(string message,
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0
    ) => _loggerInstance.Error(message, filePath, lineNumber);

    void Library.Library.Logging.ILogger.Debug(string m, string f, int l) => _loggerInstance.Debug(m, f, l);
    void Library.Library.Logging.ILogger.Info(string m, string f, int l) => _loggerInstance.Info(m, f, l);
    void Library.Library.Logging.ILogger.Warning(string m, string f, int l) => _loggerInstance.Warning(m, f, l);
    void Library.Library.Logging.ILogger.Error(string m, string f, int l) => _loggerInstance.Error(m, f, l);

    protected T FindComponentInParents<T>() where T : Component
    {
        Transform current = transform.parent;

        while (current != null)
        {
            T comp = current.GetComponent<T>();
            if (comp != null)
            {
                return comp;
            }

            current = current.parent;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        if (ShowGizmos)
        {
            DrawGizmosSafe();
            OnDrawnGizmos();
        }
    }


    protected virtual void DrawGizmosSafe() { }
}
