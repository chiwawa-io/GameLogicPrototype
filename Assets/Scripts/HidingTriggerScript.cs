using UnityEngine;

public class HidingTriggerScript : MonoBehaviour
{
    [SerializeField]
    private Transform _hidinSpot;

    public Transform Get()
    {
        return _hidinSpot;
    }
}
