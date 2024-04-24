using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
