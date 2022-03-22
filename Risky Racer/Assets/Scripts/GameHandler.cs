using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the energy system and starting the game.
/// Spawns the correct car based on selection and checks energy before starting.
/// Times the energy recharge and calls for notifications when energy is ready.
/// </summary>

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
   
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] AndroidNotificationHandler androidNotificationHandler;

    private CarPrefabs carPrefabs;
    public int energy;
    public int currentTrackIndex;

    public const string EnergyKey = "Energy";
    public const string EnergyReadyTimeKey = "EnergyReady";

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
        }
    }

    // Updates the car in the menu to the currently selected one
    public void SpawnMenuCar()
    {
        carPrefabs = GameObject.Find("Car Prefabs").GetComponent<CarPrefabs>();
        GameObject car = Instantiate(carPrefabs.carPrefabsArray[PlayerPrefs.GetInt(CustomiseMenu.CarSelectedKey, 0)],
            new Vector3(0, 0, 0),
            Quaternion.Euler(0, 0, 0));
        car.transform.parent = GameObject.Find("Car").transform;
        car.transform.localPosition = new Vector3(0, 0, 0);
        car.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Checks the energy and sets timer or refills energy if time has passed
    public void CheckEnergy()
    {
        CancelInvoke();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyTimeKey, string.Empty);

            if (energyReadyString == string.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
                EnergyRecharged();
            }
            else
            {
                GameObject.Find("Main Menu").GetComponent<MainMenu>().playButtonMainMenu.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                InvokeRepeating(nameof(CountTime), 0f, 1f);
            }
        }
    }

    // Updates menu elements if energy is recharged
    public void EnergyRecharged()
    {
        GameObject.Find("Main Menu").GetComponent<MainMenu>().playButtonMainMenu.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        GameObject.Find("Main Menu").GetComponent<MainMenu>().energyText.text = energy.ToString();
        GameObject.Find("Track Select Menu").GetComponent<TrackSelect>().energyText.text = energy.ToString();
        CountTime();
        CancelInvoke();
    }

    // Deducts energy when track is played and loads track scene.
    // Also runs an ad if half the energy is used.
    // Schedules notifcation for when energy is recharged when it reaches zero.
    public void Play()
    {
        if (energy < 1) { return; }

        if (GameObject.Find("Track Select Menu"))
        {
            currentTrackIndex = GameObject.Find("Track Select Menu").GetComponent<TrackSelect>().trackIndex;
        }

        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);

        if (energy == 0)
        {
            DateTime energyRecharged = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyTimeKey, energyRecharged.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyRecharged);
#endif      
        }

        switch (currentTrackIndex)
        {
            case 0:
                if (energy == 2) { AdManager.Instance.ShowAd("PlayTrack1"); }
                else { SceneManager.LoadScene(2); }           
                break;
            case 1:
                if (energy == 2) { AdManager.Instance.ShowAd("PlayTrack2"); }
                else { SceneManager.LoadScene(3); }
                break;
            case 2:
                if (energy == 2) { AdManager.Instance.ShowAd("PlayTrack3"); }
                else { SceneManager.LoadScene(4); }
                break;
        }      
    }

    // Updates the countdown for recharging energy, taking into account the time when the app was closed.
    // Updates menu elements to reflect energy state.
    public void CountTime()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.GetString(GameHandler.EnergyReadyTimeKey, "Ready") == "Ready") { return; }

            if (energy > 0) 
            { 
                GameObject.Find("Main Menu").GetComponent<MainMenu>().countdownTimerText.text = "You have Energy!";
                return;
            }

            DateTime energyReadyTime = DateTime.Parse(PlayerPrefs.GetString(GameHandler.EnergyReadyTimeKey, "Ready"));
            TimeSpan t = energyReadyTime - DateTime.Now;

            GameObject.Find("Main Menu").GetComponent<MainMenu>().countdownTimerText.text = "Energy in: " + t.Minutes + ":" + t.Seconds;

            if (t.Minutes <= 0 && t.Seconds <= 0)
            {
                GameObject.Find("Main Menu").GetComponent<MainMenu>().countdownTimerText.text = "You have Energy!";
                GameObject.Find("Main Menu").GetComponent<MainMenu>().OnApplicationFocus(true);
            }
        }            
    }
}
