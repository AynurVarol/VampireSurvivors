using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public float speed = 5f; // Karakterin hareket hýzý
    private Rigidbody rb; // Karakterin fiziksel hareketini kontrol eden bileþen
    private Vector3 moveVelocity; // Hareket yönü ve hýzý
    private float minZ = -7.62f, maxZ = 7.62f;
    private float minX = -11.22f, maxX = 11.22f;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini alýr
       
    }


    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); // 3D'de hareket (X ve Z ekseni)
        moveVelocity = moveInput.normalized * speed; // Giriþleri normalize eder ve hýz ile çarpar
    }

    void FixedUpdate()
    {
        // rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // Rigidbody bileþenini yeni pozisyona taþýr
        Vector3 newPosition = rb.position + moveVelocity * Time.fixedDeltaTime;

        //karakterin hareket alanýný sýnýrlamak için
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        //Yeni pozisyonu uygulamak için
        rb.MovePosition(newPosition);

    }

}
