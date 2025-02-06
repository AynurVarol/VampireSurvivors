using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    /* public int maxHealth = 100;
     public int healthReductionRate; //farklý düþmanlar için farklý saðlýk azalma oranlarý
     private int currentHealth;

     public int experienceValue; // düþmanýn verdiði deneyim puaný

     private PlayerExperience playerExperience;

     void Start()
     {
         currentHealth = maxHealth;

         //oyuncunun deneyim scriptini bulmak için;

         GameObject player = GameObject.FindWithTag("Player");
         if(player != null)
         {
             playerExperience = player.GetComponent<PlayerExperience>();
         }
     }

     public void TakeDamage(int damage)
     {
         currentHealth -= damage * healthReductionRate; // Saðlýk azalma oranýna göre hasar ver
         if (currentHealth <= 0)
         {

             Die();
         }
     }

     void Die()
     {

         if (playerExperience != null)
         {
             playerExperience.AddExperience(experienceValue); // Oyuncuya deneyim puaný ekle
         }


         // Yalnýzca sahnedeki nesneyi yok edin, asset'i deðil
         if (gameObject.scene.IsValid()) // Obje sahnede ise
         {
             Destroy(gameObject); // Objeyi sahneden kaldýr
         }
         /*else
         {
             Debug.LogWarning("Yok edilmek istenen obje sahnede deðil!");
         }*/
    public int maxHealth = 300;
    private int currentHealth;

    public GameObject experiencePrefab; // Deneyim objesi prefab'ý
    public int experienceValue; // Düþmanýn verdiði deneyim puaný

    public delegate void OnPlayerDeath(); // Ölüm için event tanýmý
    public static event OnPlayerDeath PlayerDied; // Event olarak tanýmlandý

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " " + damage + " hasar aldý, kalan saðlýk: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " ölecek.");
            Die();
        }
    }

   public void Die()
    {

        Debug.Log(gameObject.name + " öldü!");
        // Sadece oyuncu objesi öldüðünde PlayerDied eventini tetikleyin
        if (gameObject.CompareTag("Player"))
        {
            PlayerDied?.Invoke();
        }

        Destroy(gameObject); // Oyuncu objesini sahneden yok eder

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
       /* else
        {
            Debug.LogWarning("Yok edilmek istenen obje sahnede deðil!");
        }*/

    }


}
