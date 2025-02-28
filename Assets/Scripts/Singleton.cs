using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are multiple instance of " + GetType().ToString());
        }
        else
        {
            Instance = this as T;
        }

        DontDestroyOnLoad(this);
    }

    public virtual void OnDestroy()
    {
        if (Instance == gameObject)
        {
            Instance = null;
        }
    }

    protected static bool IsAvailable
    {
        get
        {
            return Instance != null;
        }
    }
}
