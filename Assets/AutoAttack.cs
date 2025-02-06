using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float attackRange = 0.7f; // Karakterin vurma/atýþ mesafesi
    public float attackRate = 1.0f; // Atak hýzý (1 saniyede 1 atýþ)
    public int damage = 10; // Verilen hasar miktarý
    private float nextAttackTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack(); // Saldýrý fonksiyonu
            nextAttackTime = Time.time + 1f / attackRate; // Sonraki saldýrý zamanýný ayarlar
        }
    }

    void Attack()
    {
        // Etraftaki düþmanlarý bulur (3D'de bir küre etrafýnda düþmanlarý arar)
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")) // Eðer düþman tespit edilirse
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage); // Düþmana hasar verilir
            }
        }
    }

    public void UpdateAttackRate(float newRate)
    {
        attackRate = newRate; // Saldýrý hýzýný güncelle
    }

    void OnDrawGizmosSelected()
    {
        // Saldýrý menzilini görmek için bir küre çizer (sadece Unity Editor'da)
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}