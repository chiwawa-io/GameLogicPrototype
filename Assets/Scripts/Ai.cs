using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    private enum AiState
    {
        Running,
        Hiding,
        Finished,
        Dead
    }

    private Transform endPoint;

    private NavMeshAgent agent;

    private Animator animator;

    [SerializeField]
    private AiState aiState;

    private void OnEnable()
    {
        Initialize();
    }

    void Initialize ()
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

        animator.ResetTrigger("Death");
        aiState = AiState.Running;
    } 

    public void Dead ()
    {
        StartCoroutine(DeathRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            this.gameObject.SetActive(false);
            aiState = AiState.Finished;
        }
        if (other.CompareTag("HidingSpot"))
        {
            if (aiState == AiState.Dead) return;
            agent.isStopped = true;
            animator.SetBool("Hiding", true);
            StartCoroutine(HidingRoutine());
            aiState = AiState.Hiding;
        }
        if (other.CompareTag("HidingTrigger"))
        {
            var position = other.GetComponent<HidingTriggerScript>().Get().position;
            if (aiState == AiState.Dead) return;
            agent.SetDestination(position);
        }
        
    }

    IEnumerator HidingRoutine()
    {
        yield return new WaitForSeconds(2f);
        agent.isStopped = false;
        animator.SetBool("Hiding", false);
        agent.SetDestination(endPoint.position);
        animator.SetFloat("Speed", agent.speed);
        aiState = AiState.Running;
    }

    IEnumerator DeathRoutine() {
        aiState = AiState.Dead;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2f);
        agent.isStopped = true;
        this.gameObject.SetActive(false);
    }
}
