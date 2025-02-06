using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int experienceValue; // Düþmanýn verdiði deneyim puaný

    private int currentHealth;

    public GameObject experiencePrefab; // Deneyim objesi prefab'ý
   


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy" + damage + " hasar aldý, kalan saðlýk: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log(" Enmey ölecek.");
            Die();
        }
    }

    public void Die()
    {
        // Deneyim objesini oluþtur
        if (experiencePrefab != null && tag == "Enemy")
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        }

        // Düþman objesini sahneden yok et (sadece sahnedeki GameObject'e uygula)
        if (gameObject.scene.isLoaded)
        {
            Destroy(gameObject); // Yalnýzca sahnede yüklenmiþ bir objeyi yok etmeye çalýþtýðýnýzdan emin olun
        }
    }
}
