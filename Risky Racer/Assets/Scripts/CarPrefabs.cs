using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to handle selection of car prefabs.
/// </summary>

public class CarPrefabs : MonoBehaviour
{
    [SerializeField] public GameObject[] carPrefabsArray;

    public static CarPrefabs Instance;

    void Awake()
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
}
