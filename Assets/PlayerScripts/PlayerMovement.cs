using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public float speed = 5f; // Karakterin hareket h�z�
    private Rigidbody rb; // Karakterin fiziksel hareketini kontrol eden bile�en
    private Vector3 moveVelocity; // Hareket y�n� ve h�z�
    private float minZ = -7.62f, maxZ = 7.62f;
    private float minX = -11.22f, maxX = 11.22f;

   

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al�r
       
    }


    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); // 3D'de hareket (X ve Z ekseni)
        moveVelocity = moveInput.normalized * speed; // Giri�leri normalize eder ve h�z ile �arpar
    }

    void FixedUpdate()
    {
        // rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // Rigidbody bile�enini yeni pozisyona ta��r
        Vector3 newPosition = rb.position + moveVelocity * Time.fixedDeltaTime;

        //karakterin hareket alan�n� s�n�rlamak i�in
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        //Yeni pozisyonu uygulamak i�in
        rb.MovePosition(newPosition);

    }

}
