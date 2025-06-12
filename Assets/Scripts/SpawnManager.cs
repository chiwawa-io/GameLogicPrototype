using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Multiple instances of SpawnManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private GameObject _aiPrefab;
    private int _maxAIPrefabs = 20;

    private List<GameObject> _aiPrefabPool;


    void Start()
    {
        _aiPrefabPool = new List<GameObject>();

        InitPool();

        StartCoroutine(SpawnAi());

        if (_aiPrefab == null) Debug.LogError("AI Prefab is not assigned in the SpawnManager.");
        if (_startPoint == null) Debug.LogError("Start Point is not assigned in the SpawnManager.");
    }

    void Update()
    {
        
    }

    void InitPool()
    {
        GameObject tmp;

        for (int i = 0; i < _maxAIPrefabs; i++)
        {
            tmp = Instantiate(_aiPrefab);
            tmp.SetActive(false);
            _aiPrefabPool.Add(tmp);
        }
    }

    public GameObject GetAIPrefab()
    {
        foreach (var aiPrefab in _aiPrefabPool)
        {
            if (!aiPrefab.activeInHierarchy)
            {
                aiPrefab.SetActive(true);
                return aiPrefab;
            }
        }
        Debug.LogWarning("No inactive AI Prefabs available in the pool. Consider increasing the pool size.");
        return null;
    }

    public void SpawnAiPrefab()
    {
        GameObject aiPrefab = GetAIPrefab();
        if (aiPrefab != null)
        {
            aiPrefab.transform.position = _startPoint.position;
        }
        else
        {
            Debug.LogError("Failed to spawn AI Prefab: No available prefab in the pool.");
        }
    }

    public Transform GetEndPoint()
    {
        return _endPoint.transform;
    }

    IEnumerator SpawnAi()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Adjust the spawn interval as needed
            SpawnAiPrefab();
        }
    }

}
