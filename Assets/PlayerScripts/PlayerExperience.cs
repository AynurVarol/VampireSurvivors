using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public int experiencePoints; // Toplanan deneyim puaný
    private Text experienceText; // UI'deki text öðesi

    private int speedThreshold = 40; // Seviye atlamak için gereken deneyim puaný
    private int shieldThreshold = 60;
    private int fireRateThreshold = 80;

    private bool speedUnlocked = false; // 40 puan için tetik kontrolü
    private bool shieldUnlocked = false; // 90 puan için tetik kontrolü
    private bool fireRateUnlocked = false; // 120 puan için tetik kontrolü



    private bool isPaused = false; // Oyunun duraklatýlýp duraklatýlmadýðýný kontrol eder
    private SkillManager skillManager; // SkillManager referansý
    public bool isInvincible = false;

    void Start()
    {
        // Text bileþenini bul ve debug ile kontrol et
       
        GameObject xpTextObject = GameObject.Find("XPText");
        if (xpTextObject != null && xpTextObject.activeInHierarchy)
        {
            experienceText = xpTextObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("XPText aktif deðil veya bulunamadý!");
        }


        // SkillManager referansýný al
        skillManager = GameObject.FindObjectOfType<SkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("SkillManager sahnede bulunamadý!");
        }

        UpdateExperienceUI(); // Baþlangýçta UI'yi güncelle
    }


// Deneyim puaný ekleme fonksiyonu
    public void AddExperience(int points)
    {
        experiencePoints += points; // Puan ekle
        UpdateExperienceUI(); // UI'yi her zaman güncelle



        //Deneyim puaný threshold'a ulaþtýysa seviye atla ve puaný sýfýrla
        if (experiencePoints >= speedThreshold && !speedUnlocked)
        {
            speedUnlocked = true;

            PauseGame(); // 40ta Oyunu duraklat ve menüyü aç
           
            

        }
        else if (experiencePoints >= shieldThreshold && !shieldUnlocked)
        {
            shieldUnlocked = true; // Tetikleme kontrolü
            Debug.Log("MENÜ PANELÝ AÇ");
            skillManager.OpenSkillMenu();

            //UpdateExperienceUI();

        }
        else if (experiencePoints >= fireRateThreshold && !fireRateUnlocked)
        {
            fireRateUnlocked = true; // Tetikleme kontrolü
            Debug.Log("MENÜ PANELÝ AÇ");
            skillManager.OpenSkillMenu();
            //UpdateExperienceUI();


        }
    }
    //oyuncu yeterli deneyeim puanýna sahip mi kontrol et
    public bool HasEnoughExperience(int amount)
    {
        return experiencePoints >= amount;
    }

    //Oyuncunun yetenek kullanmak için deneyim puaný harcamasýný saðlar
    public void UseExperience(int amount)
    {
        if (experiencePoints >= amount)
        {
            experiencePoints -= amount;
            Debug.Log($"Deneyim harcandý: {amount}, Kalan XP: {experiencePoints}");
            UpdateExperienceUI(); // UI'yi hemen güncelle
        }
        else
        {
            Debug.LogWarning("Yeterli deneyim puaný yok!");
        }
    }

    // Oyunu duraklatma ve menüyü açma fonksiyonu
    void PauseGame()
    {
        if (!isPaused && skillManager != null)
        {
            Time.timeScale = 0f; // Oyunu duraklat
            skillManager.OpenSkillMenu(); // Yetenek menüsünü aç
            isPaused = true; // Durum güncellemesi
        }
    }

    // Oyunu devam ettirme ve menüyü kapatma fonksiyonu
    public void ResumeGame()
    {

        Time.timeScale = 1f; // Oyunu devam ettir
        GameObject xpTextObject = GameObject.Find("XPText");

        if (xpTextObject != null && xpTextObject.activeInHierarchy)
        {
            experienceText = xpTextObject.GetComponent<Text>();
            UpdateExperienceUI(); // UI'yi tekrar güncelle
        }
        else
        {
            //Debug.LogWarning("XPText referansý atanamadý! Sahneye geri eklenmemiþ olabilir.");
        }

        
    }

    // Deneyim arayüzünü güncelleme fonksiyonu
    public void UpdateExperienceUI()
    {
        if (experienceText != null)
        {
            experienceText.text = "XP:" + experiencePoints; // Deneyim puaný güncelle
            Debug.Log($"XP UI güncellendi: {experiencePoints}");


        }
        else
        {
            Debug.LogWarning("Experience Text referansý atanmadý!");
        }
    }
}

