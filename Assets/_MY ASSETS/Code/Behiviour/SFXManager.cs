using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private SoundData soundData;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        instance = this;    
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(soundData.hitSound);
    }

    public void PlayLevelCompleteSound()
    {
        audioSource.PlayOneShot(soundData.levelCompleteSound);
    }
}
