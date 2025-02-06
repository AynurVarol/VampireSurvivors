using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    /* public int maxHealth = 100;
     public int healthReductionRate; //farkl� d��manlar i�in farkl� sa�l�k azalma oranlar�
     private int currentHealth;

     public int experienceValue; // d��man�n verdi�i deneyim puan�

     private PlayerExperience playerExperience;

     void Start()
     {
         currentHealth = maxHealth;

         //oyuncunun deneyim scriptini bulmak i�in;

         GameObject player = GameObject.FindWithTag("Player");
         if(player != null)
         {
             playerExperience = player.GetComponent<PlayerExperience>();
         }
     }

     public void TakeDamage(int damage)
     {
         currentHealth -= damage * healthReductionRate; // Sa�l�k azalma oran�na g�re hasar ver
         if (currentHealth <= 0)
         {

             Die();
         }
     }

     void Die()
     {

         if (playerExperience != null)
         {
             playerExperience.AddExperience(experienceValue); // Oyuncuya deneyim puan� ekle
         }


         // Yaln�zca sahnedeki nesneyi yok edin, asset'i de�il
         if (gameObject.scene.IsValid()) // Obje sahnede ise
         {
             Destroy(gameObject); // Objeyi sahneden kald�r
         }
         /*else
         {
             Debug.LogWarning("Yok edilmek istenen obje sahnede de�il!");
         }*/
    public int maxHealth = 300;
    private int currentHealth;

    public GameObject experiencePrefab; // Deneyim objesi prefab'�
    public int experienceValue; // D��man�n verdi�i deneyim puan�

    public delegate void OnPlayerDeath(); // �l�m i�in event tan�m�
    public static event OnPlayerDeath PlayerDied; // Event olarak tan�mland�

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " " + damage + " hasar ald�, kalan sa�l�k: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " �lecek.");
            Die();
        }
    }

   public void Die()
    {

        Debug.Log(gameObject.name + " �ld�!");
        // Sadece oyuncu objesi �ld���nde PlayerDied eventini tetikleyin
        if (gameObject.CompareTag("Player"))
        {
            PlayerDied?.Invoke();
        }

        Destroy(gameObject); // Oyuncu objesini sahneden yok eder

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
       /* else
        {
            Debug.LogWarning("Yok edilmek istenen obje sahnede de�il!");
        }*/

    }


}
