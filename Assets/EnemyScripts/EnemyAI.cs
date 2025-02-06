using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   
    public GameObject playerObject;

    public float speed = 3.0f; // D��man�n hareket h�z�
    public float attackRange = 0.5f; // D��man�n sald�r� menzili

    public int damage = 10; // Verilen hasar miktar�
    private Rigidbody rb;

    void OnEnable() //sonradan
    {
        // Oyuncu spawn edildi�inde referans� al
        PlayerSpawn.OnPlayerSpawned += SetPlayerObject;
    }

    void OnDisable() // sonradan
    {
        // Event'ten ��k
        PlayerSpawn.OnPlayerSpawned -= SetPlayerObject;

    }

    void Start()
    {
       
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al�r
        
        // Player'�n sahnede olup olmad���n� kontrol et
        Debug.Log("player sahnede");
        if (playerObject == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            // Player referans� atanmad�ysa, player'� tag ile bul
            //playerObject = GameObject.FindWithTag("Player");
            //sonradan 
            //PlayerSpawn.OnPlayerSpawned += SetPlayerObject;

            
           Debug.Log("player� buldu");
          
            if (player != null)
            {
                SetPlayerObject(player);
            }
            else
            {
                PlayerSpawn.OnPlayerSpawned += SetPlayerObject;
            }

        }
    }
  

    void SetPlayerObject(GameObject player) //sonadan eklendi
    {
        playerObject = player;
        Debug.Log("Player referans� al�nd�: " + player.name);
    }


    void FixedUpdate()
    {
      

        if (playerObject == null) return;

        Vector3 direction = (playerObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);

        if(distance > attackRange)
        {
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            Debug.Log($"{gameObject.name} oyuncuya do�ru hareket ediyor.");
        }
        else
        {
            rb.velocity = Vector3.zero; // Menzile ula�t���nda durur
            Debug.Log("Enemy sald�r� pozisyonunda.");
            Attack(); // Oyuncuya sald�r�r
        }




    }

    

    void Attack()
    {
        if (playerObject != null)
        {
            Health playerHealth = playerObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Sald�r� ger�ekle�ir, oyuncunun can�n� azalt�r
            }
            else
            {
                Debug.LogWarning("Player objesinde Health bile�eni bulunamad�.");
            }
        }
        else
        {
            Debug.LogWarning("Player referans� atanmad�.");



        }
    }
}

