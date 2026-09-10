using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource _mainAudioSource;


    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _mainAudioSource = GetComponent<AudioSource>();
    }

    public void ChangeBackgroundMusic(AudioClip clip)
    {
        _mainAudioSource.clip = clip;
    }

    public void ActivateEffectAudioClip(AudioClip clip)
    {
        _mainAudioSource.pitch = Random.Range(0.8f, 1.2f);
        _mainAudioSource.PlayOneShot(clip);
    }
}