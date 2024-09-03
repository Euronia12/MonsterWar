using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    [Tooltip("씬 이동 시 : true 비파괴/ false 파괴")]
    [SerializeField] protected bool isDontDestroyOnLoad = true;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    //매니저에서 Awake호출 시 Base로 호출 할것!!
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (isDontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance != null)
            Destroy(gameObject);
    }
}
