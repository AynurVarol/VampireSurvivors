using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float attackRange = 0.7f; // Karakterin vurma/at�� mesafesi
    public float attackRate = 1.0f; // Atak h�z� (1 saniyede 1 at��)
    public int damage = 10; // Verilen hasar miktar�
    private float nextAttackTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack(); // Sald�r� fonksiyonu
            nextAttackTime = Time.time + 1f / attackRate; // Sonraki sald�r� zaman�n� ayarlar
        }
    }

    void Attack()
    {
        // Etraftaki d��manlar� bulur (3D'de bir k�re etraf�nda d��manlar� arar)
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")) // E�er d��man tespit edilirse
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage); // D��mana hasar verilir
            }
        }
    }

    public void UpdateAttackRate(float newRate)
    {
        attackRate = newRate; // Sald�r� h�z�n� g�ncelle
    }

    void OnDrawGizmosSelected()
    {
        // Sald�r� menzilini g�rmek i�in bir k�re �izer (sadece Unity Editor'da)
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}