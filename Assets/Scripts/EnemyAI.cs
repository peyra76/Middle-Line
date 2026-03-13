using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Настройки атаки")]
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public float rotationSpeed = 5.0f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    [Header("Stun Settings")]
    public bool isStunned = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>(); 

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (isStunned) return;

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            FaceTarget();

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            float normalizedSpeed = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("Speed", normalizedSpeed);

            if (agent.hasPath && agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathPartial)
            {
                Debug.LogWarning("Враг видит игрока, но не может построить к нему полный путь!");
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; 

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void ApplyStagger(float duration)
    {
        StartCoroutine(StaggerRoutine(duration));
    }

    private System.Collections.IEnumerator StaggerRoutine(float duration)
    {
        isStunned = true;

        if (agent != null) agent.isStopped = true;

        if (animator != null) animator.ResetTrigger("Attack");

        yield return new WaitForSeconds(duration);

        isStunned = false;
        if (agent != null) agent.isStopped = false;
    }

}