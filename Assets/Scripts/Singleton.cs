using UnityEngine;

public abstract class Singleton<T> : Singleton where T : Singleton<T>
{
    private static T _instance;
    private static readonly object _lock = new();

    [SerializeField]
    private bool _persistent = true;

    public static T Instance
    {
        get
        {
            if (Quitting)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance != null)
                    return _instance;
                
                var instances = FindObjectsByType<T>(FindObjectsSortMode.None);
                var count = instances.Length;

                if (count == 1)
                    return _instance = instances[0];

                if (count > 0)
                {
                    for (var i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);

                    return _instance = instances[0];
                }

                return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
            }
        }
    }

    private void Awake()
    {
        if (_persistent)
            DontDestroyOnLoad(gameObject);

        OnAwake();
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool Quitting { get; private set; }
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
}