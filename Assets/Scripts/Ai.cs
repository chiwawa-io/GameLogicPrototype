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

    void Start()
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

        switch (other.gameObject.name)
        {
            case "T1":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T2":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T3":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T4":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T5":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T6":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T7":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T8":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T9":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T10":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T11":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T12":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T13":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T14":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T15":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T16":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            case "T17":
                agent.SetDestination(other.GetComponent<HidingTriggerScript>().Get().position);
                break;
            default:
                break;

        }
    }

    IEnumerator HidingRoutine()
    {
        yield return new WaitForSeconds(5f);
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
        Destroy(gameObject);
    }
}
