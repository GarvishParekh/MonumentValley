using UnityEngine;

public class StarIdentity : MonoBehaviour
{
    public StarStatus starStatus;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject star;
    
    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        star = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        ManageStarStatus();
    }

    private void ManageStarStatus()
    {
        switch (starStatus)
        {
            case StarStatus.Default:
                StarAnimation();
                WaveAnimation();
                break;

            case StarStatus.Collected:
                break;
        }
    }

    private void StarAnimation()
    {
        star.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }


    Vector3 starPosition;
    float sineWave = 0;

    [SerializeField] private float degree = 0;
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveHeight = 1;
    [SerializeField] private float heightOffset = 0;
    private void WaveAnimation()
    {
        degree += Time.deltaTime * waveSpeed;
        sineWave = (Mathf.Sin(degree) * waveHeight) + heightOffset;

        starPosition.y = sineWave;  
        star.transform.localPosition = starPosition;
    }
}
