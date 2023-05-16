using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();
    private static bool isApplicateQuit = false;

    public static T Instance
    {
        get
        {
            if (isApplicateQuit)
            {
                return _instance;
            }

            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = Object.FindObjectOfType<T>();

#if UNITY_EDITOR
                        if (Object.FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError("[SingletonMono] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        }
#endif

                        if (_instance == null)
                        {
                            GameObject gameObject = new GameObject(typeof(T).Name);
                            _instance = gameObject.AddComponent<T>();

                            if (Application.isPlaying)
                            {
                                Object.DontDestroyOnLoad(gameObject);
                            }
                        }
                    }
                }
            }
            return _instance;
        }
    }

    public virtual void OnApplicationQuit()
    {
        isApplicateQuit = true;
    }
}
