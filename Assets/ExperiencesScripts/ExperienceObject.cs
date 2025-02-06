using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceObject : MonoBehaviour
{
    public int experienceValue = 20; // Deneyim puaný deðeri

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player'a deneyim puaný ekle
            PlayerExperience playerExperience = other.GetComponent<PlayerExperience>();
            if (playerExperience != null)
            {
                playerExperience.AddExperience(experienceValue);
            }

            // Deneyim objesini sahneden yok et
            Destroy(gameObject);
        }
    }
}
