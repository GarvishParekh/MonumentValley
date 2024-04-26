using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private SoundData soundData;
    [SerializeField] private AudioSource audioSourceMid;
    [SerializeField] private AudioSource audioSourceLow;

    private void Awake()
    {
        instance = this;    
    }

    public void PlayHitSound()
    {
        audioSourceMid.PlayOneShot(soundData.hitSound);
    }

    public void PlayLevelCompleteSound()
    {
        audioSourceMid.PlayOneShot(soundData.levelCompleteSound);
    }

    public void PlayJumpSound()
    {
        audioSourceMid.PlayOneShot(soundData.jumpSound);
    }

    public void PlayStaffGrowSound()
    {
        audioSourceLow.PlayOneShot(soundData.staffGrowSound);
    }
}
