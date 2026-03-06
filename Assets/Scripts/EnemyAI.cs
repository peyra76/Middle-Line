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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
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
}