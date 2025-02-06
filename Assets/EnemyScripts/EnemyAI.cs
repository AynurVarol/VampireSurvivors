using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   
    public GameObject playerObject;

    public float speed = 3.0f; // Düþmanýn hareket hýzý
    public float attackRange = 0.5f; // Düþmanýn saldýrý menzili

    public int damage = 10; // Verilen hasar miktarý
    private Rigidbody rb;

    void OnEnable() //sonradan
    {
        // Oyuncu spawn edildiðinde referansý al
        PlayerSpawn.OnPlayerSpawned += SetPlayerObject;
    }

    void OnDisable() // sonradan
    {
        // Event'ten çýk
        PlayerSpawn.OnPlayerSpawned -= SetPlayerObject;

    }

    void Start()
    {
       
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini alýr
        
        // Player'ýn sahnede olup olmadýðýný kontrol et
        Debug.Log("player sahnede");
        if (playerObject == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            // Player referansý atanmadýysa, player'ý tag ile bul
            //playerObject = GameObject.FindWithTag("Player");
            //sonradan 
            //PlayerSpawn.OnPlayerSpawned += SetPlayerObject;

            
           Debug.Log("playerý buldu");
          
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
        Debug.Log("Player referansý alýndý: " + player.name);
    }


    void FixedUpdate()
    {
      

        if (playerObject == null) return;

        Vector3 direction = (playerObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);

        if(distance > attackRange)
        {
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            Debug.Log($"{gameObject.name} oyuncuya doðru hareket ediyor.");
        }
        else
        {
            rb.velocity = Vector3.zero; // Menzile ulaþtýðýnda durur
            Debug.Log("Enemy saldýrý pozisyonunda.");
            Attack(); // Oyuncuya saldýrýr
        }




    }

    

    void Attack()
    {
        if (playerObject != null)
        {
            Health playerHealth = playerObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Saldýrý gerçekleþir, oyuncunun canýný azaltýr
            }
            else
            {
                Debug.LogWarning("Player objesinde Health bileþeni bulunamadý.");
            }
        }
        else
        {
            Debug.LogWarning("Player referansý atanmadý.");



        }
    }
}

