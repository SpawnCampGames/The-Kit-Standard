using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int Health
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value,0,maxHealth);
            Debug.Log($"Health: {currentHealth}");
            if(currentHealth == 0) Debug.Log("PlayerIsDead");
        }
    }

    void Start()
    {
        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    [ContextMenu("Take 10 Damage")]
    private void DebugTakeDamage()
    {
        TakeDamage(10);
    }

    [ContextMenu("Heal 10 HP")]
    private void DebugHeal()
    {
        Heal(10);
    }
}
/* BAD
public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    void Start()
    {
        health = 100;
    }

    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("PlayerIsDead");
        }
    }
}*/
