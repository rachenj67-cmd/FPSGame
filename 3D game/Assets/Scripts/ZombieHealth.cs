using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; 

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    private Animator anim;
    private NavMeshAgent agent;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; 

        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

     
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

       
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        if (col != null) col.enabled = false;

       
        Destroy(gameObject, 5f);
    }
}

