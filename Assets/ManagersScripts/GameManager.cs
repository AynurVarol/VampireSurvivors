using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel; // Ba�lang�� paneli
    public GameObject characterSelectionPanel;  // karakter se�im paneli
    public Button startButton;    // Start butonu
    public Button exitButton;    //��k�� butonu
    public PlayerSpawn playerSpawn; 


    void Start()
    {
        // Oyun ba�lad���nda panel a��k olacak
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        // Start butonuna t�klan�nca oyunu ba�lat�r
        if (startButton != null)
        {
            startButton.onClick.AddListener(OpenCharacterSelectionPanel);
        }

        // Exit butonuna t�klama event'ini ekle
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }


        // Oyunu duraklatarak ana sahne ba�lamadan durmas�n� sa�lar
        Time.timeScale = 0f;

        //karakter panelini ba�lang��ta gizle
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false);
        }

        if(playerSpawn != null)
        {
            PlayerSpawn.OnPlayerSpawned += OnPlayerSpawned;
        }
    }


    // karakter se�im panelini a�ar
    public void OpenCharacterSelectionPanel()
    {
        if(startPanel != null)
        {
            startPanel.SetActive(false);// start oaneli kapat
        }

        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(true); //karakter se�im paneli a�
        }
            
    }

    //player spawn olunca karakter se�im panceresini kpat
    private void OnPlayerSpawned(GameObject player)
    {
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false);
        }
        Time.timeScale = 1f; //oyunu ba�lat
    }

    public void  StartGame()
    {
        if(characterSelectionPanel != null)
        {
            characterSelectionPanel.SetActive(false); //  se�im panelini kpat

        }
        Time.timeScale = 1f; // Oyunu ba�lat
        Debug.Log("Oyun ba�lad�!");


    }



    // Oyundan ��k�� yapma fonksiyonu
    public void ExitGame()
    {
        Debug.Log("Oyun kapat�l�yor...");

#if UNITY_EDITOR
        // E�er Unity Edit�r'deysek, sadece play modunu durdur
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Build edilmi� oyunda program� kapat
        Application.Quit();
#endif
    }
}
