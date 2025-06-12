using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }
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

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _barierShot;
    [SerializeField] private AudioClip _enemyDied;
    [SerializeField] private AudioClip _enemyEscaped;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBarrierShot()
    {
        _audioSource.PlayOneShot(_barierShot);
    }
    public void PlayEnemyDied()
    {
        _audioSource.PlayOneShot(_enemyDied);
    }
    public void PlayEnemyEscaped()
    {
        _audioSource.PlayOneShot(_enemyEscaped);
    }
}
