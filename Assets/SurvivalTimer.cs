using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public Text timerText; // UI'daki s�re g�sterimi
    public Text levelText; // seviye mesaj� i�in u� text
    private float elapsedTime = 0f; // Ge�en s�reyi tutar
    private bool isPlayerAlive = true; // Oyuncunun hayatta olup olmad���n� kontrol eder
    private int currentLevel = 1; // oyuncunun mevcur seviytesi
    private float timeToLevelUp = 60f; // seviye atlamak i�in gereken s�re
    private const float levelUpIncrease = 30f; // 3.seviyeden sonra her seviye i�in eklenecek s�re
    

    void OnEnable()
    {
        Health.PlayerDied += StopTimer; // Oyuncu �ld���nde timer� durdur
    }

    void OnDisable()
    {
       Health.PlayerDied -= StopTimer; // Eventten ��k�� yap
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            UpdateTimerUI(); // UI'daki s�reyi g�ncelle
           if(elapsedTime >= timeToLevelUp)//seviye atlama kontrolu
            {
                LevelUp();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Dakikay� hesapla
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Artan saniyeyi hesapla
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // "MM:SS" format�nda g�ster
        
        
    }

    void StopTimer()
    {
        Debug.Log("StopTimer �al��t�!"); // Kontrol i�in
        isPlayerAlive = false; // Oyuncu �ld�, sayac� durdur
    }

    void LevelUp()
    {
        currentLevel++; //seviye artar
        elapsedTime = 0f; //ge��en s�reyi s�f�rla
        ShowLevelMessage(); // seviye mesaj�n� gs�ter

        //Seviye i�in gereken s�reyi ayarla
        if(currentLevel > 3)
        {
            timeToLevelUp += levelUpIncrease; // 3.seviyeden sonra s�reyi artt�r
        }
        Debug.Log("YEN� SEV�YE:" + currentLevel);

    }
     void ShowLevelMessage()
    {
        if(levelText != null)
        {
            levelText.text = "SEV�YE" + currentLevel;
            levelText.gameObject.SetActive(true);

            //mesaj� gixle 
            Invoke(nameof(HideLevelMessage), 1f);

        }
    }

    void HideLevelMessage()
    {
        if(levelText != null)
        {
            levelText.gameObject.SetActive(false);
        }
    }
}
