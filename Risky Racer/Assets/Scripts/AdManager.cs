using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

/// <summary>
/// Standard Ad manager. Ads play to recharge energy or after using half the total energy.
/// After ad is complete, load the level or recharge energy based on instructionKey.
/// </summary>

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager Instance;
    private AudioSource audioSource;
    private bool wasAudioMuted;

    // Change to False for final product 
    [SerializeField] private bool testMode = true;

    private string instructionKey;
    
#if UNITY_ANDROID
    private string gameID = "4416067";
#elif UNITY_IOS
    private string gameID = "4416066";
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this);
            Advertisement.Initialize(gameID, testMode);
            audioSource = GameObject.Find("Game Handler").GetComponent<AudioSource>();
        }
    }

    public void ShowAd(string instruction)
    {
        wasAudioMuted = audioSource.mute;
        audioSource.mute = true;
        instructionKey = instruction;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (!wasAudioMuted) audioSource.mute = false;

        switch (showResult)
        {
            case ShowResult.Finished:
                if (instructionKey == "Menu")
                {
                    GameHandler.Instance.EnergyRecharged();
                }
                else if (instructionKey == "PlayTrack1")
                {
                    SceneManager.LoadScene(2);
                }
                else if (instructionKey == "PlayTrack2")
                {
                    SceneManager.LoadScene(3);
                }
                else if (instructionKey == "PlayTrack3")
                {
                    SceneManager.LoadScene(4);
                }
                break;
            case ShowResult.Skipped:
                // Ad was skipped
                SceneManager.LoadScene(0);
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                SceneManager.LoadScene(0);
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
}