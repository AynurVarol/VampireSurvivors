using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef oyuncu 
    public float smoothSpeed; //hareket h�z�n�n yumu�akl���
    public Vector3 offset; // Kameran�n hedefe g�re konum fark�

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Hedef pozisyonunu ve ofseti al�r
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Kameran�n konumunu yumu�ak bir �ekilde ge�i� yapar
        transform.position = smoothedPosition; // Kameray� yeni pozisyona ta��r
    }
}
