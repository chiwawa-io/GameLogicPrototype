using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitInfo;
    void Start()
    {
        LayerMask layerMask = LayerMask.GetMask("Enemy"); // Adjust the layer mask as needed
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
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 6))
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
            // You can add more logic here, like applying damage to the hit object
        }
        else
        {
            Debug.Log("Missed");
        }
    }
}
