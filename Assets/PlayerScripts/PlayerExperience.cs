using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public int experiencePoints; // Toplanan deneyim puan�
    private Text experienceText; // UI'deki text ��esi

    private int speedThreshold = 40; // Seviye atlamak i�in gereken deneyim puan�
    private int shieldThreshold = 60;
    private int fireRateThreshold = 80;

    private bool speedUnlocked = false; // 40 puan i�in tetik kontrol�
    private bool shieldUnlocked = false; // 90 puan i�in tetik kontrol�
    private bool fireRateUnlocked = false; // 120 puan i�in tetik kontrol�



    private bool isPaused = false; // Oyunun duraklat�l�p duraklat�lmad���n� kontrol eder
    private SkillManager skillManager; // SkillManager referans�
    public bool isInvincible = false;

    void Start()
    {
        // Text bile�enini bul ve debug ile kontrol et
       
        GameObject xpTextObject = GameObject.Find("XPText");
        if (xpTextObject != null && xpTextObject.activeInHierarchy)
        {
            experienceText = xpTextObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("XPText aktif de�il veya bulunamad�!");
        }


        // SkillManager referans�n� al
        skillManager = GameObject.FindObjectOfType<SkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("SkillManager sahnede bulunamad�!");
        }

        UpdateExperienceUI(); // Ba�lang��ta UI'yi g�ncelle
    }


// Deneyim puan� ekleme fonksiyonu
    public void AddExperience(int points)
    {
        experiencePoints += points; // Puan ekle
        UpdateExperienceUI(); // UI'yi her zaman g�ncelle



        //Deneyim puan� threshold'a ula�t�ysa seviye atla ve puan� s�f�rla
        if (experiencePoints >= speedThreshold && !speedUnlocked)
        {
            speedUnlocked = true;

            PauseGame(); // 40ta Oyunu duraklat ve men�y� a�
           
            

        }
        else if (experiencePoints >= shieldThreshold && !shieldUnlocked)
        {
            shieldUnlocked = true; // Tetikleme kontrol�
            Debug.Log("MEN� PANEL� A�");
            skillManager.OpenSkillMenu();

            //UpdateExperienceUI();

        }
        else if (experiencePoints >= fireRateThreshold && !fireRateUnlocked)
        {
            fireRateUnlocked = true; // Tetikleme kontrol�
            Debug.Log("MEN� PANEL� A�");
            skillManager.OpenSkillMenu();
            //UpdateExperienceUI();


        }
    }
    //oyuncu yeterli deneyeim puan�na sahip mi kontrol et
    public bool HasEnoughExperience(int amount)
    {
        return experiencePoints >= amount;
    }

    //Oyuncunun yetenek kullanmak i�in deneyim puan� harcamas�n� sa�lar
    public void UseExperience(int amount)
    {
        if (experiencePoints >= amount)
        {
            experiencePoints -= amount;
            Debug.Log($"Deneyim harcand�: {amount}, Kalan XP: {experiencePoints}");
            UpdateExperienceUI(); // UI'yi hemen g�ncelle
        }
        else
        {
            Debug.LogWarning("Yeterli deneyim puan� yok!");
        }
    }

    // Oyunu duraklatma ve men�y� a�ma fonksiyonu
    void PauseGame()
    {
        if (!isPaused && skillManager != null)
        {
            Time.timeScale = 0f; // Oyunu duraklat
            skillManager.OpenSkillMenu(); // Yetenek men�s�n� a�
            isPaused = true; // Durum g�ncellemesi
        }
    }

    // Oyunu devam ettirme ve men�y� kapatma fonksiyonu
    public void ResumeGame()
    {

        Time.timeScale = 1f; // Oyunu devam ettir
        GameObject xpTextObject = GameObject.Find("XPText");

        if (xpTextObject != null && xpTextObject.activeInHierarchy)
        {
            experienceText = xpTextObject.GetComponent<Text>();
            UpdateExperienceUI(); // UI'yi tekrar g�ncelle
        }
        else
        {
            //Debug.LogWarning("XPText referans� atanamad�! Sahneye geri eklenmemi� olabilir.");
        }

        
    }

    // Deneyim aray�z�n� g�ncelleme fonksiyonu
    public void UpdateExperienceUI()
    {
        if (experienceText != null)
        {
            experienceText.text = "XP:" + experiencePoints; // Deneyim puan� g�ncelle
            Debug.Log($"XP UI g�ncellendi: {experiencePoints}");


        }
        else
        {
            Debug.LogWarning("Experience Text referans� atanmad�!");
        }
    }
}

