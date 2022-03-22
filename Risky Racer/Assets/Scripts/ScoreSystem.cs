using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays the score as the player is playing a track.
/// Updates the high scores for each track if a new high score is reached.
/// </summary>

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Coins coin; 
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplyer;

    public const string HighScore1Key = "HighScore1";
    public const string HighScore2Key = "HighScore2";
    public const string HighScore3Key = "HighScore3";

    public float score;

    public bool isPlaying = true;
    private bool isScoring = false;

    private void Start()
    {
        Invoke(nameof(StartScoring), 3f);
    }

    void Update()
    {
        if (isPlaying && isScoring)
        {
            score += Time.deltaTime * scoreMultiplyer;
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }       
    }

    private void OnDestroy()
    {
        switch (GameHandler.Instance.currentTrackIndex)
        {
            case 0:
                if (score > PlayerPrefs.GetInt(HighScore1Key, 0))
                {
                    PlayerPrefs.SetInt(HighScore1Key, Mathf.FloorToInt(score));
                }
                break;
            case 1:
                if (score > PlayerPrefs.GetInt(HighScore2Key, 0))
                {
                    PlayerPrefs.SetInt(HighScore2Key, Mathf.FloorToInt(score));
                }
                break;
            case 2:
                if (score > PlayerPrefs.GetInt(HighScore3Key, 0))
                {
                    PlayerPrefs.SetInt(HighScore3Key, Mathf.FloorToInt(score));
                }
                break;
        }     
    }

    private void StartScoring()
    {
        scoreText.GetComponentInParent<Canvas>().enabled = true;
        isScoring = true;
    }
}
