using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 300;
    private int currentHealth;
    public delegate void OnPlayerDeath(); // Ölüm için event tanýmý
    public static event OnPlayerDeath PlayerDied; // Event olarak tanýmlandý
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player " + damage + " hasar aldý, kalan saðlýk: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " ölecek.");
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("PLayer öldü!");
        // Sadece oyuncu objesi öldüðünde PlayerDied eventini tetikleyin
        if (gameObject.CompareTag("Player"))
        {
            PlayerDied?.Invoke();
        }

        Destroy(gameObject); // Oyuncu objesini sahneden yok eder
    }
}
