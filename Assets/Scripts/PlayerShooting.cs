using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private AudioClip _loadSound;

    private Ray ray;
    private RaycastHit hitInfo;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
            return;
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        _audioSource.PlayOneShot(_fireSound);

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 6 | 1 << 7))
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Ai>().Dead();
                SoundManager.Instance.PlayEnemyDied();
                GameManager.instance.AddScore();
            }
            else
            {
               SoundManager.Instance.PlayBarrierShot();
            }
        }
        _audioSource.PlayOneShot(_loadSound);
    }
}
