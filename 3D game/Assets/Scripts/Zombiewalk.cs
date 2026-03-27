using UnityEngine;
using UnityEngine.AI;

public class Zombiewalk : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;

    [Header("Range Settings")]
    public float chaseRange = 10f;
    public float attackRange = 2f;

    [Header("Attack Settings")]
    public float attackDelay = 1.5f;
    public float attackCooldown = 2f;

    private float timer;
    private float lastAttackTime;
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

      
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        timer = attackDelay;
    }

    void Update()
    {
     
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
        {
            return; 
        }

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

     
        if (distance <= attackRange)
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Attack();
                timer = attackCooldown;
            }
        }
        // 2. ระยะไล่ตาม
        else if (distance <= chaseRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
            timer = attackDelay;
        }
 
        else
        {
            agent.isStopped = true;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                Debug.Log("Zombie bit the player!");
            }
        }
    }
}