using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public Text timerText; // UI'daki süre gösterimi
    public Text levelText; // seviye mesajý için uý text
    private float elapsedTime = 0f; // Geçen süreyi tutar
    private bool isPlayerAlive = true; // Oyuncunun hayatta olup olmadýðýný kontrol eder
    private int currentLevel = 1; // oyuncunun mevcur seviytesi
    private float timeToLevelUp = 60f; // seviye atlamak için gereken süre
    private const float levelUpIncrease = 30f; // 3.seviyeden sonra her seviye için eklenecek süre
    

    void OnEnable()
    {
        Health.PlayerDied += StopTimer; // Oyuncu öldüðünde timerý durdur
    }

    void OnDisable()
    {
       Health.PlayerDied -= StopTimer; // Eventten çýkýþ yap
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            UpdateTimerUI(); // UI'daki süreyi güncelle
           if(elapsedTime >= timeToLevelUp)//seviye atlama kontrolu
            {
                LevelUp();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Dakikayý hesapla
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Artan saniyeyi hesapla
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // "MM:SS" formatýnda göster
        
        
    }

    void StopTimer()
    {
        Debug.Log("StopTimer çalýþtý!"); // Kontrol için
        isPlayerAlive = false; // Oyuncu öldü, sayacý durdur
    }

    void LevelUp()
    {
        currentLevel++; //seviye artar
        elapsedTime = 0f; //geççen süreyi sýfýrla
        ShowLevelMessage(); // seviye mesajýný gsöter

        //Seviye için gereken süreyi ayarla
        if(currentLevel > 3)
        {
            timeToLevelUp += levelUpIncrease; // 3.seviyeden sonra süreyi arttýr
        }
        Debug.Log("YENÝ SEVÝYE:" + currentLevel);

    }
     void ShowLevelMessage()
    {
        if(levelText != null)
        {
            levelText.text = "SEVÝYE" + currentLevel;
            levelText.gameObject.SetActive(true);

            //mesajý gixle 
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
