using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    private Transform endPoint;

    private NavMeshAgent agent;

    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        endPoint = SpawnManager.Instance.GetEndPoint();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this GameObject.");
            return;
        }

        agent.SetDestination(endPoint.position);
        animator.SetFloat("Speed", agent.speed);
    }

    
    void Update()
    {
        //if (agent.remainingDistance < 1f) this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
