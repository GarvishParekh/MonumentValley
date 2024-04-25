using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "Sound Data")]
public class SoundData : ScriptableObject
{
    public AudioClip backgroundMusic;
    public AudioClip hitSound;
    public AudioClip levelCompleteSound;
    public AudioClip jumpSound;
}
