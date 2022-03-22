using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles all behaviour on the customise menu. 
/// Each car and it's info are hidden and shown based on the current car index.
/// </summary>

public class CustomiseMenu : MonoBehaviour
{
    [SerializeField] private GameObject car1Container;
    [SerializeField] private GameObject car2Container;
    [SerializeField] private GameObject car3Container;
    [SerializeField] private GameObject car2BuyButton;
    [SerializeField] private GameObject car3BuyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject platform;

    // For testing cars only, delete for final product.
    private bool carLocked = true;
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    private CarPrefabs carPrefabs;
    private GameObject car;
    private Vector3 carPosition =  new Vector3(0, 0, 0);
    private Quaternion carRotation = Quaternion.Euler(0, 150, 0);
    private int carIndex = 0;

    public const string CarSelectedKey = "CarSelected";  

    private void Start()
    {
        carPrefabs = GameObject.Find("Car Prefabs").GetComponent<CarPrefabs>();
        car = Instantiate(carPrefabs.carPrefabsArray[carIndex], carPosition, carRotation);

        UpdateMenu();
    }

    private void Update()
    {
        platform.transform.Rotate(0, 20 * Time.deltaTime, 0);
        if (car != null)
        {
            car.transform.Rotate(0, 20 * Time.deltaTime, 0);
            carRotation = car.transform.rotation;
        }
    }

    // Unlocks all cars for testing purposes. Delete for final product
    public void UnlockRelockCar()
    {
        if (carLocked)
        {
            carLocked = false;
            selectButton.GetComponent<Button>().interactable = true;
            lockImage.SetActive(false);
        }
        else
        {
            carLocked = true;
            selectButton.GetComponent<Button>().interactable = false;
            lockImage.SetActive(true);
        }
    }

    // Cycles to previous car, updates visuals and car index
    public void PreviousButton()
    {
        if (carIndex == 0)
        {
            carIndex = carPrefabs.carPrefabsArray.Length - 1;
        }
        else carIndex--;

        Destroy(car);

        car = Instantiate(carPrefabs.carPrefabsArray[carIndex], carPosition, carRotation);

        UpdateMenu();        
    }

    // Cycles to next car, updates visuals and car index
    public void NextButton()
    {
        if (carIndex == carPrefabs.carPrefabsArray.Length - 1)
        {
            carIndex = 0;
        }
        else carIndex++;

        Destroy(car);

        car = Instantiate(carPrefabs.carPrefabsArray[carIndex], carPosition, carRotation);

        UpdateMenu();
    }

    // Selects the car and reloads main menu
    public void SelectButton()
    {
        PlayerPrefs.SetInt(CarSelectedKey, carIndex);
        SceneManager.LoadScene(0);
    }

    // Shows and hides relevant information and images based on car index
    public void UpdateMenu()
    {
        switch (carIndex)
        {
            // Car 1
            case 0:
                car1Container.SetActive(true);
                car2Container.SetActive(false);
                car3Container.SetActive(false);

                lockImage.SetActive(false);
                selectButton.GetComponent<Button>().interactable = true;
                break;

            // Car 2
            case 1:
                car1Container.SetActive(false);
                car2Container.SetActive(true);
                car3Container.SetActive(false);

                if (PlayerPrefs.GetInt(Store.TwoCarUnlockedKey) == 0)
                {
                    selectButton.GetComponent<Button>().interactable = false;
                    lockImage.SetActive(true);
                    car2BuyButton.SetActive(true);
                }
                else if (PlayerPrefs.GetInt(Store.TwoCarUnlockedKey) == 1)
                {
                    lockImage.SetActive(false);
                    selectButton.GetComponent<Button>().interactable = true;
                    car2BuyButton.GetComponent<Button>().interactable = false;
                }
                break;

            // Car 3
            case 2:
                car1Container.SetActive(false);
                car2Container.SetActive(false);
                car3Container.SetActive(true);

                if (PlayerPrefs.GetInt(Store.ThreeCarUnlockedKey) == 0)
                {
                    selectButton.GetComponent<Button>().interactable = false;
                    lockImage.SetActive(true);
                    car3BuyButton.SetActive(true);
                }
                else if (PlayerPrefs.GetInt(Store.TwoCarUnlockedKey) == 1)
                {
                    lockImage.SetActive(false);
                    selectButton.GetComponent<Button>().interactable = true;
                    car3BuyButton.GetComponent<Button>().interactable = false;
                }
                break;
        }   
    }
}
