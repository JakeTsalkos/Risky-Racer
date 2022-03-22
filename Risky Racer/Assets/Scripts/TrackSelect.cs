using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles tracks select menu and updates elements based on the track you are viewing.
/// </summary>

public class TrackSelect : MonoBehaviour
{
    [SerializeField] private Sprite[] trackImageArray;
    [SerializeField] private Image trackScreenshotImage;
    [SerializeField] private TMP_Text trackTitleText;
    [SerializeField] private string[] trackTitles;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private Image lockedImage;
    [SerializeField] private Button playButtonSelectMenu;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] public TMP_Text energyText;

    public int trackIndex = 0;
    private int indexMax;

    public const string Track1UnlockedKey = "Track1";
    public const string Track2UnlockedKey = "Track2";
    public const string Track3UnlockedKey = "Track3";

    private void Start()
    {
        trackIndex = 0;
        indexMax = trackImageArray.Length - 1;
        energyText.text = GameHandler.Instance.energy.ToString();
        PlayerPrefs.SetInt(Track1UnlockedKey, 1);

        UpdateDisplay();

        previousButton.interactable = false;
    }

    public void PreviousTrack()
    {
        trackIndex--;        

        UpdateDisplay();

        if (trackIndex == 0)
        {
            previousButton.interactable = false;            
        }
        nextButton.interactable = true;
    }

    public void NextTrack()
    {
        trackIndex++;

        UpdateDisplay();

        if (trackIndex == indexMax)
        {         
            nextButton.interactable = false;
        }
        previousButton.interactable = true;
    }

    // Updates name, image and highscore elements based on trackIndex.
    // Only unlocks track if high enough score is reached on previous track.
    private void UpdateDisplay()
    {
        trackScreenshotImage.sprite = trackImageArray[trackIndex];
        trackTitleText.text = trackTitles[trackIndex];

        switch (trackIndex)
        {
            case 0:
                highScoreText.text = "Highscore: " + PlayerPrefs.GetInt(ScoreSystem.HighScore1Key, 0);

                if (PlayerPrefs.GetInt(Track1UnlockedKey, 0) == 0)
                {
                    lockedImage.enabled = true;
                    playButtonSelectMenu.interactable = false;
                }
                else if (PlayerPrefs.GetInt(Track1UnlockedKey, 0) == 1)
                {
                    lockedImage.enabled = false;
                    playButtonSelectMenu.interactable = true;
                }
                break;

            case 1:
                highScoreText.text = "Highscore: " + PlayerPrefs.GetInt(ScoreSystem.HighScore2Key, 0);

                if (PlayerPrefs.GetInt(ScoreSystem.HighScore1Key, 0) > 10)
                {
                    PlayerPrefs.SetInt(Track2UnlockedKey, 1);
                }

                if (PlayerPrefs.GetInt(Track2UnlockedKey, 0) == 0)
                {
                    lockedImage.enabled = true;
                    playButtonSelectMenu.interactable = false;
                }
                else if (PlayerPrefs.GetInt(Track2UnlockedKey, 0) == 1)
                {
                    lockedImage.enabled = false;
                    playButtonSelectMenu.interactable = true;
                }
                break;

            case 2:
                highScoreText.text = "Highscore: N/A";
                lockedImage.enabled = true;
                playButtonSelectMenu.interactable = false;

                // Uncomment when for when a third track is added

                /*highScoreText.text = "Highscore: " + PlayerPrefs.GetInt(ScoreSystem.HighScore3Key, 0);

                if (PlayerPrefs.GetInt(ScoreSystem.HighScore2Key, 0) > 10)
                {
                    PlayerPrefs.SetInt(Track3UnlockedKey, 1);
                }

                if (PlayerPrefs.GetInt(Track3UnlockedKey, 0) == 0)
                {
                    lockedImage.enabled = true;
                    playButtonSelectMenu.interactable = false;
                }
                else if (PlayerPrefs.GetInt(Track3UnlockedKey, 0) == 1)
                {
                    lockedImage.enabled = false;
                    playButtonSelectMenu.interactable = true;
                }*/
                break;
        }
    }
}
