using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;
    public delegate void OnPlayerDeath(); // �l�m i�in event tan�m�
    public static event OnPlayerDeath PlayerDied; // Event olarak tan�mland�
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player " + damage + " hasar ald�, kalan sa�l�k: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " �lecek.");
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("PLayer �ld�!");
        // Sadece oyuncu objesi �ld���nde PlayerDied eventini tetikleyin
        if (gameObject.CompareTag("Player"))
        {
            PlayerDied?.Invoke();
        }

        Destroy(gameObject); // Oyuncu objesini sahneden yok eder
    }
}
