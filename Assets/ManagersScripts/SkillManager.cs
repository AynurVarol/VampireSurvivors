using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    // Panel ve UI bileþenleri
    public Image experienceMenuPanel; // Deneyim menüsü panel
    public Text warningText; // Uyarý metni
    public Text skillMessageText; // yetenek mesajý için txt

    public Button increaseSpeedButton;
    public Button increaseFireRateButton;
    public Button activateShieldButton;

    public PlayerExperience playerExperience; // PlayerExperience referansý
    public AutoAttack playerAutoAttack; // AutoAttack bileþenine referans

    public float playerSpeed = 5f; // Oyuncunun baþlangýç hýzý
    public float playerDamage = 10f; // Oyuncunun baþlangýç hasar gücü
  

    // Yetenekler için gerekli deneyim puanlarý
    private int shieldExperienceRequirement = 20;
    private int fireRateExperienceRequirement = 70;
    private int speedExperienceRequirement = 80;


    private float shieldCooldown = 10f;  // Kalkan için bekleme süresi
    private float fireRateCooldown = 5f; // Ateþ hýzý için bekleme süresi
    private float speedCooldown = 8f;    // Hýz için bekleme süresi

    private float shieldCooldownTimer = 0f;
    private float fireRateCooldownTimer = 0f;
    private float speedCooldownTimer = 0f;


   
    private bool hasSpeedBoost = false;
    private bool hasFireRateBoost = false;
    private bool hasShield = false;

    // Yeteneklerin aktif olup olmadýðýný tutan bayraklar
    private bool speedUnlocked = false;
    private bool fireRateUnlocked = false;
    private bool shieldUnlocked = false;


   
    void Start()
    {
        InitializeUI();
        BindButtons(); // Butonlara dinleyici eklenir
    }

    #region Yardýmcý Fonksiyonlar
    private void Update()
    {
        // Cooldown zamanlayýcýlarýný güncelle
        shieldCooldownTimer -= Time.deltaTime;
        fireRateCooldownTimer -= Time.deltaTime;
        speedCooldownTimer -= Time.deltaTime;

        // Klavyeden giriþleri kontrol et
        if (Input.GetKeyDown(KeyCode.Q) && shieldCooldownTimer <= 0f && hasShield)
        {
            ActivateShield();
            Debug.Log("kalkan aktif klaye");
            ShowSkillMessage("Oyuncunuun kalkaný aktif edili!");
            shieldCooldownTimer = shieldCooldown; // Cooldown süresini baþlat
        }
       
        if (Input.GetKeyDown(KeyCode.E) && fireRateCooldownTimer <= 0f && hasFireRateBoost)
        {
            IncreaseFireRate();
            Debug.Log("ateþ hýzý arttý klave");
            ShowSkillMessage("Oyuncunun ateþ hýzý arttýrýldý!");
            fireRateCooldownTimer = fireRateCooldown;
        }

        if (Input.GetKeyDown(KeyCode.R) && speedCooldownTimer <= 0f && hasSpeedBoost)
        {
            IncreaseSpeed();
            Debug.Log("hýz arttý klavye");
            ShowSkillMessage("Oyuncunun hýzý arttýrýldý!");
            speedCooldownTimer = speedCooldown;
        }
    }

    private void ShowSkillMessage(string message)
    {
        if(skillMessageText != null)
        {
            skillMessageText.text = message; //mesajý ayarla
            skillMessageText.gameObject.SetActive(true); //mesajý göster
            CancelInvoke(nameof(HideSkillMessage)); //önceki çaðrýlarý iptal et
            Invoke(nameof(HideSkillMessage), 1f); //1 saniye sonra gizle

        }
    }

    private void HideSkillMessage()
    {
        if(skillMessageText != null)
        {
            skillMessageText.gameObject.SetActive(false); // mesajý gizle
        }
    }
    #endregion

    private void InitializeUI()
    {
        // Uyarý metnini gizle
        if (warningText != null) warningText.gameObject.SetActive(false);

        // Deneyim menüsü panelini bul ve gizle
        if (experienceMenuPanel == null)
        {
            experienceMenuPanel = GameObject.Find("ExperienceMenuPanel").GetComponent<Image>();
            if (experienceMenuPanel == null)
                Debug.LogError("ExperienceMenuPanel bulunamadý!");
        }
        if (experienceMenuPanel != null) experienceMenuPanel.gameObject.SetActive(false);
    }

    //her buton için yetenek ve gerekli deneyim puaný atamasý
    private void BindButtons()
    {
        increaseSpeedButton.onClick.AddListener(() => TryActivateSkill(IncreaseSpeed, speedExperienceRequirement));
        increaseFireRateButton.onClick.AddListener(() => TryActivateSkill(IncreaseFireRate, fireRateExperienceRequirement));
        activateShieldButton.onClick.AddListener(() => TryActivateSkill(ActivateShield, shieldExperienceRequirement));
    }

    //seçilen yetenek için yeterli deneyim puaný varsa yeteneði etkinleþtþir
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
            string warningMessage = warningText != null ? warningText.text : "yeterli deneyim puaný yok";
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
       
        // Menüyü kapat ama XPText'in devre dýþý kalmadýðýndan emin ol
        GameObject xpTextObject = GameObject.Find("XPText");
        if (xpTextObject != null)
        {
            xpTextObject.SetActive(true); // XPText'i aktif tut
        }

        experienceMenuPanel.gameObject.SetActive(false); // Skill menüsünü kapat
        ResumeGame();
    }

    public void ShowWarningMessage(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
       // CancelInvoke(nameof(HideWarningMessage)); // daha önceki çaðrýlarý iptal et 
        Invoke(nameof(HideWarningMessage), 1f); // 1saniye sonra mesajý gizle
    }

    private void HideWarningMessage()
    {
        warningText.gameObject.SetActive(false);
    }


    #region Yetenek Fonksiyonlarý
    // Hasar artýrma fonksiyonu
    public void IncreaseDamage()
    {
        playerDamage *= 2f; // Hasar gücünü iki katýna çýkar
        Debug.Log("Hasar gücü artýrýldý: " + playerDamage);

        playerExperience.ResumeGame(); // Oyunu devam ettir ve paneli kapat

        // playerExperience.UpdateExperienceUI();
        //ResumeGame();
    }



    //oyuncunun hýzýný arttýr
    public void IncreaseSpeed()
    {
        if (!hasSpeedBoost)
        {
            playerSpeed += 2f;
            hasSpeedBoost = true;
            Debug.Log("Hýz artýrýldý: " + playerSpeed);
            ShowSkillMessage("Hýz arttýrýldý!");
            playerExperience.ResumeGame(); // Oyunu devam ettir
            //playerExperience.UpdateExperienceUI();
        }
    }

    //oyuncunun ateþ hýzýný arttýrýr
    public void IncreaseFireRate()
    {
        if (!hasFireRateBoost)
        {
            StartCoroutine(FireRateBoostCoroutine());
            hasFireRateBoost = true;
            Debug.Log("ateþ hýzý: "+ fireRateExperienceRequirement);
            ShowSkillMessage("Ateþ hýzý arttýrýldý!");
            playerExperience.ResumeGame();


        }
    }
    #endregion


    #region Geçici Güçlendirmeler
    //Coroutinr kullanarak geçiçi bir ateþ artýþ hýzý saðlanýr
    IEnumerator FireRateBoostCoroutine()
    {
        float originalRate = playerAutoAttack.attackRate;
        playerAutoAttack.UpdateAttackRate(originalRate * 2f);
        yield return new WaitForSeconds(5f);
        playerAutoAttack.UpdateAttackRate(originalRate);
    }

    //kalkan gücü oyuncuyu bir süre yenilmez yapar
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
        float shieldDuration = 5f; // Süreyi artýr
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
