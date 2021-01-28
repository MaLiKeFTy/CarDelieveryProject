using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static event Action OnStart;
    public static event Action OnEnd;

    static GameState instance;
    void Awake() => DeploySingleton();

    /// <summary>
    /// Application start callback
    /// </summary>
    void Start() => OnStart?.Invoke();

    /// <summary>
    /// Application end callback
    /// </summary>
    void OnApplicationQuit() => OnEnd?.Invoke();

    void DeploySingleton()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
