using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Spawns the correct prefab at the beginning of the level and connects it to other scripts.
/// </summary>

public class CarSpawnHandler : MonoBehaviour
{
    private Vector3 carPosition = new Vector3(0, 0, 0);
    private CarPrefabs carPrefabs;

    void Start()
    {
        carPrefabs = GameObject.Find("Car Prefabs").GetComponent<CarPrefabs>();

        GameObject car = Instantiate(carPrefabs.carPrefabsArray[PlayerPrefs.GetInt(CustomiseMenu.CarSelectedKey, 0)],
            carPosition,
            Quaternion.Euler(0, 0, 0));

        car.transform.parent = GameObject.Find("Car").transform;
        car.transform.localPosition = carPosition;

        GameObject.Find("Car").GetComponent<Car>().AllocateWheels();
    }
}
