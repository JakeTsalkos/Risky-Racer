using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the main menu navigation and buttons.
/// Updates player info like energy and coins on app focus.
/// </summary>

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject trackSelectMenuCanvas;
    [SerializeField] public Button playButtonMainMenu;
    [SerializeField] public TMP_Text energyText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] public TMP_Text countdownTimerText;
    [SerializeField] private TMP_Text currentGoldText;

    private void Start()
    {
        OnApplicationFocus(true);
    }

    // Updates scores, energy and gold based on player prefs when opening the game
    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        GameHandler.Instance.SpawnMenuCar();
        GameHandler.Instance.CheckEnergy();

        highScoreText.text = $"High Score: {PlayerPrefs.GetInt(ScoreSystem.HighScore1Key, 0)}";             
        energyText.text = GameHandler.Instance.energy.ToString();
        currentGoldText.text = PlayerPrefs.GetInt(Coins.CurrentCoinsKey, 0).ToString();

        GameHandler.Instance.CheckEnergy();
    }

    public void CallPlay()
    {   
        GameHandler.Instance.Play();
    }

    public void Customise()
    {
        SceneManager.LoadScene(1);
    }

    public void RefillEnergy()
    {
        AdManager.Instance.ShowAd("Menu");
    }

    public void OpenTrackSelectMenu()
    {
        trackSelectMenuCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        trackSelectMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
