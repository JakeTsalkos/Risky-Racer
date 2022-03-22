using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles the crashed state when a player collides with a wall or obstacle.
/// Brings up the crashed menu and displays player stats.
/// </summary>

public class Crashed : MonoBehaviour
{  
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Coins coin;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private GameObject score;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private float fadeDuration = 2f;

    // Enables the crashed menu canvas and updates player stats to screen.
    public void CarCrashed()
    {
        score.GetComponent<ScoreSystem>().isPlaying = false;
        score.GetComponentInChildren<Canvas>().enabled = false;
        finalScore.text = "Score: " + Mathf.FloorToInt(score.GetComponent<ScoreSystem>().score).ToString();
        energyText.text = PlayerPrefs.GetInt(GameHandler.EnergyKey, 0).ToString();
        coinText.text = $"Coins: +{coin.coinsCollected}";

        if (PlayerPrefs.GetInt(GameHandler.EnergyKey, 0) <= 0)
        {
            playAgainButton.interactable = false;
        }
        else playAgainButton.interactable = true;

        var canvasGroup = gameOverMenu.GetComponent<CanvasGroup>();
        StartCoroutine(GameOver(canvasGroup, canvasGroup.alpha, 1));    
    }

    // Brief fade in animation for the menu
    private IEnumerator GameOver(CanvasGroup canvasGroup, float start, float end)
    {
        gameOverMenu.GetComponent<Canvas>().enabled = true;

        float counter = 0f;
        while(counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;
        }       
    }

    public void PlayAgain()
    {
        GameHandler.Instance.Play();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
