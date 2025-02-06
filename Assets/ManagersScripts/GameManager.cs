using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel; // Baþlangýç paneli
    public GameObject characterSelectionPanel;  // karakter seçim paneli
    public Button startButton;    // Start butonu
    public Button exitButton;    //çýkýþ butonu
    public PlayerSpawn playerSpawn; 


    void Start()
    {
        // Oyun baþladýðýnda panel açýk olacak
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        // Start butonuna týklanýnca oyunu baþlatýr
        if (startButton != null)
        {
            startButton.onClick.AddListener(OpenCharacterSelectionPanel);
        }

        // Exit butonuna týklama event'ini ekle
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }


        // Oyunu duraklatarak ana sahne baþlamadan durmasýný saðlar
        Time.timeScale = 0f;

        //karakter panelini baþlangýçta gizle
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false);
        }

        if(playerSpawn != null)
        {
            PlayerSpawn.OnPlayerSpawned += OnPlayerSpawned;
        }
    }


    // karakter seçim panelini açar
    public void OpenCharacterSelectionPanel()
    {
        if(startPanel != null)
        {
            startPanel.SetActive(false);// start oaneli kapat
        }

        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(true); //karakter seçim paneli aç
        }
            
    }

    //player spawn olunca karakter seçim panceresini kpat
    private void OnPlayerSpawned(GameObject player)
    {
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false);
        }
        Time.timeScale = 1f; //oyunu baþlat
    }

    public void  StartGame()
    {
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false); //  seçim panelini kpat

        }
        Time.timeScale = 1f; // Oyunu baþlat
        Debug.Log("Oyun baþladý!");


    }



    // Oyundan çýkýþ yapma fonksiyonu
    public void ExitGame()
    {
        Debug.Log("Oyun kapatýlýyor...");

#if UNITY_EDITOR
        // Eðer Unity Editör'deysek, sadece play modunu durdur
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Build edilmiþ oyunda programý kapat
        Application.Quit();
#endif
    }
}
