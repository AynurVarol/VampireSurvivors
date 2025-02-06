using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int experienceValue; // D��man�n verdi�i deneyim puan�

    private int currentHealth;

    public GameObject experiencePrefab; // Deneyim objesi prefab'�
   


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy" + damage + " hasar ald�, kalan sa�l�k: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log(" Enmey �lecek.");
            Die();
        }
    }

    public void Die()
    {
        // Deneyim objesini olu�tur
        if (experiencePrefab != null && tag == "Enemy")
        {
            Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        }

        // D��man objesini sahneden yok et (sadece sahnedeki GameObject'e uygula)
        if (gameObject.scene.isLoaded)
        {
            Destroy(gameObject); // Yaln�zca sahnede y�klenmi� bir objeyi yok etmeye �al��t���n�zdan emin olun
        }
    }
}
