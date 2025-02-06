using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    // Panel ve UI bile�enleri
    public Image experienceMenuPanel; // Deneyim men�s� panel
    public Text warningText; // Uyar� metni
    public Text skillMessageText; // yetenek mesaj� i�in txt

    public Button increaseSpeedButton;
    public Button increaseFireRateButton;
    public Button activateShieldButton;

    public PlayerExperience playerExperience; // PlayerExperience referans�
    public AutoAttack playerAutoAttack; // AutoAttack bile�enine referans

    public float playerSpeed = 5f; // Oyuncunun ba�lang�� h�z�
    public float playerDamage = 10f; // Oyuncunun ba�lang�� hasar g�c�
  

    // Yetenekler i�in gerekli deneyim puanlar�
    private int shieldExperienceRequirement = 20;
    private int fireRateExperienceRequirement = 70;
    private int speedExperienceRequirement = 80;


    private float shieldCooldown = 10f;  // Kalkan i�in bekleme s�resi
    private float fireRateCooldown = 5f; // Ate� h�z� i�in bekleme s�resi
    private float speedCooldown = 8f;    // H�z i�in bekleme s�resi

    private float shieldCooldownTimer = 0f;
    private float fireRateCooldownTimer = 0f;
    private float speedCooldownTimer = 0f;


   
    private bool hasSpeedBoost = false;
    private bool hasFireRateBoost = false;
    private bool hasShield = false;

    // Yeteneklerin aktif olup olmad���n� tutan bayraklar
    private bool speedUnlocked = false;
    private bool fireRateUnlocked = false;
    private bool shieldUnlocked = false;


   
    void Start()
    {
        InitializeUI();
        BindButtons(); // Butonlara dinleyici eklenir
    }

    #region Yard�mc� Fonksiyonlar
    private void Update()
    {
        // Cooldown zamanlay�c�lar�n� g�ncelle
        shieldCooldownTimer -= Time.deltaTime;
        fireRateCooldownTimer -= Time.deltaTime;
        speedCooldownTimer -= Time.deltaTime;

        // Klavyeden giri�leri kontrol et
        if (Input.GetKeyDown(KeyCode.Q) && shieldCooldownTimer <= 0f && hasShield)
        {
            ActivateShield();
            Debug.Log("kalkan aktif klaye");
            ShowSkillMessage("Oyuncunuun kalkan� aktif edili!");
            shieldCooldownTimer = shieldCooldown; // Cooldown s�resini ba�lat
        }
       
        if (Input.GetKeyDown(KeyCode.E) && fireRateCooldownTimer <= 0f && hasFireRateBoost)
        {
            IncreaseFireRate();
            Debug.Log("ate� h�z� artt� klave");
            ShowSkillMessage("Oyuncunun ate� h�z� artt�r�ld�!");
            fireRateCooldownTimer = fireRateCooldown;
        }

        if (Input.GetKeyDown(KeyCode.R) && speedCooldownTimer <= 0f && hasSpeedBoost)
        {
            IncreaseSpeed();
            Debug.Log("h�z artt� klavye");
            ShowSkillMessage("Oyuncunun h�z� artt�r�ld�!");
            speedCooldownTimer = speedCooldown;
        }
    }

    private void ShowSkillMessage(string message)
    {
        if(skillMessageText != null)
        {
            skillMessageText.text = message; //mesaj� ayarla
            skillMessageText.gameObject.SetActive(true); //mesaj� g�ster
            CancelInvoke(nameof(HideSkillMessage)); //�nceki �a�r�lar� iptal et
            Invoke(nameof(HideSkillMessage), 1f); //1 saniye sonra gizle

        }
    }

    private void HideSkillMessage()
    {
        if(skillMessageText != null)
        {
            skillMessageText.gameObject.SetActive(false); // mesaj� gizle
        }
    }
    #endregion

    private void InitializeUI()
    {
        // Uyar� metnini gizle
        if (warningText != null) warningText.gameObject.SetActive(false);

        // Deneyim men�s� panelini bul ve gizle
        if (experienceMenuPanel == null)
        {
            experienceMenuPanel = GameObject.Find("ExperienceMenuPanel").GetComponent<Image>();
            if (experienceMenuPanel == null)
                Debug.LogError("ExperienceMenuPanel bulunamad�!");
        }
        if (experienceMenuPanel != null) experienceMenuPanel.gameObject.SetActive(false);
    }

    //her buton i�in yetenek ve gerekli deneyim puan� atamas�
    private void BindButtons()
    {
        increaseSpeedButton.onClick.AddListener(() => TryActivateSkill(IncreaseSpeed, speedExperienceRequirement));
        increaseFireRateButton.onClick.AddListener(() => TryActivateSkill(IncreaseFireRate, fireRateExperienceRequirement));
        activateShieldButton.onClick.AddListener(() => TryActivateSkill(ActivateShield, shieldExperienceRequirement));
    }

    //se�ilen yetenek i�in yeterli deneyim puan� varsa yetene�i etkinle�t�ir
    private void TryActivateSkill(System.Action skillAction, int requiredXP)
    {
        if (playerExperience.experiencePoints >= requiredXP)
        {
            playerExperience.UseExperience(requiredXP);
            skillAction.Invoke();
            CloseSkillMenu();
        }
        else
        {
            string warningMessage = warningText != null ? warningText.text : "yeterli deneyim puan� yok";
            ShowWarningMessage(warningMessage);
           // warningText.gameObject.SetActive(false);
        }
    }


    public void OpenSkillMenu()
    {
        
        if (experienceMenuPanel != null)
        {
            experienceMenuPanel.gameObject.SetActive(true);
            Time.timeScale = 0f; // Oyunu duraklat
        }
    }

    public void CloseSkillMenu()
    {
       
        // Men�y� kapat ama XPText'in devre d��� kalmad���ndan emin ol
        GameObject xpTextObject = GameObject.Find("XPText");
        if (xpTextObject != null)
        {
            xpTextObject.SetActive(true); // XPText'i aktif tut
        }

        experienceMenuPanel.gameObject.SetActive(false); // Skill men�s�n� kapat
        ResumeGame();
    }

    public void ShowWarningMessage(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
       // CancelInvoke(nameof(HideWarningMessage)); // daha �nceki �a�r�lar� iptal et 
        Invoke(nameof(HideWarningMessage), 1f); // 1saniye sonra mesaj� gizle
    }

    private void HideWarningMessage()
    {
        warningText.gameObject.SetActive(false);
    }


    #region Yetenek Fonksiyonlar�
    // Hasar art�rma fonksiyonu
    public void IncreaseDamage()
    {
        playerDamage *= 2f; // Hasar g�c�n� iki kat�na ��kar
        Debug.Log("Hasar g�c� art�r�ld�: " + playerDamage);

        playerExperience.ResumeGame(); // Oyunu devam ettir ve paneli kapat

        // playerExperience.UpdateExperienceUI();
        //ResumeGame();
    }



    //oyuncunun h�z�n� artt�r
    public void IncreaseSpeed()
    {
        if (!hasSpeedBoost)
        {
            playerSpeed += 2f;
            hasSpeedBoost = true;
            Debug.Log("H�z art�r�ld�: " + playerSpeed);
            ShowSkillMessage("H�z artt�r�ld�!");
            playerExperience.ResumeGame(); // Oyunu devam ettir
            //playerExperience.UpdateExperienceUI();
        }
    }

    //oyuncunun ate� h�z�n� artt�r�r
    public void IncreaseFireRate()
    {
        if (!hasFireRateBoost)
        {
            StartCoroutine(FireRateBoostCoroutine());
            hasFireRateBoost = true;
            Debug.Log("ate� h�z�: "+ fireRateExperienceRequirement);
            ShowSkillMessage("Ate� h�z� artt�r�ld�!");
            playerExperience.ResumeGame();


        }
    }
    #endregion


    #region Ge�ici G��lendirmeler
    //Coroutinr kullanarak ge�i�i bir ate� art�� h�z� sa�lan�r
    IEnumerator FireRateBoostCoroutine()
    {
        float originalRate = playerAutoAttack.attackRate;
        playerAutoAttack.UpdateAttackRate(originalRate * 2f);
        yield return new WaitForSeconds(5f);
        playerAutoAttack.UpdateAttackRate(originalRate);
    }

    //kalkan g�c� oyuncuyu bir s�re yenilmez yapar
    public void ActivateShield()
    {
        if (!hasShield)
        {
            StartCoroutine(ShieldCoroutine());
            hasShield = true;
            playerExperience.isInvincible = true;
            Debug.Log("kalkan aktif edildi");
            ShowSkillMessage("Kalkan aktif edildi");
            playerExperience.ResumeGame();
        }
    }

    IEnumerator ShieldCoroutine()
    {
        float shieldDuration = 5f; // S�reyi art�r
        playerExperience.isInvincible = true;
        yield return new WaitForSeconds(shieldDuration);
        playerExperience.isInvincible = false;

    }
    #endregion



    private void ResumeGame()
    {
        Time.timeScale = 1f;
        playerExperience.UpdateExperienceUI();
    }
     
}
