using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareExperienceObject : MonoBehaviour
{
    public int experienceValue = 100;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //playera deneyim puanýný ekle
            PlayerExperience playerExperience = other.GetComponent<PlayerExperience>();
            if(playerExperience != null)
            {
                playerExperience.AddExperience(experienceValue);
            }
            //deneyim objesini sahneden yok et
            Destroy(gameObject);
        }
    }
}
