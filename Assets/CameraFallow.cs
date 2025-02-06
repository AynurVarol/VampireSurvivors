using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef oyuncu 
    public float smoothSpeed; //hareket hýzýnýn yumuþaklýðý
    public Vector3 offset; // Kameranýn hedefe göre konum farký

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Hedef pozisyonunu ve ofseti alýr
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Kameranýn konumunu yumuþak bir þekilde geçiþ yapar
        transform.position = smoothedPosition; // Kamerayý yeni pozisyona taþýr
    }
}
