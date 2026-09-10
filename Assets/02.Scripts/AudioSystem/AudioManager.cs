using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource _mainAudioSource;


    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
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