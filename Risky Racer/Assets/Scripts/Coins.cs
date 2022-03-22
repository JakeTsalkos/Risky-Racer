using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the spawning and collection of coins.
/// Also animates the coins to spin and bob on the spot.
/// </summary>

public class Coins : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject coinParent;
    [SerializeField] private int rotationSpeed;

    private List<GameObject> coinList = new List<GameObject>();
    private int totalCoins;

    public int coinsCollected = 0;
    public AnimationCurve myCurve;
    public const string CurrentCoinsKey = "CurrentCoins";

    private void Start()
    {
        totalCoins = PlayerPrefs.GetInt(CurrentCoinsKey, 0);
        foreach (Transform coin in coinParent.transform)
        {
            coinList.Add(coin.gameObject);
        }
    }

    // Spins and bobs the coins up and down
    private void FixedUpdate()
    {
        foreach (Transform coin in coinParent.transform)
        {
            coin.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            coin.transform.position = new Vector3(coin.transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), coin.transform.position.z);
        }
    }

    private void OnDestroy()
    {
        totalCoins += coinsCollected;
        PlayerPrefs.SetInt(CurrentCoinsKey, totalCoins);
    }

    public void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        coinText.text = coinsCollected.ToString();
        coin.SetActive(false);
    }

    public void RespawnCoins()
    {
        foreach (Transform coin in coinParent.transform)
        {
            coin.gameObject.SetActive(true);
        }
    }
}
