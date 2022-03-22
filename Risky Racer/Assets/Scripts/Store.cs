using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


/// <summary>
/// Handles the purchasing through the play store of locked cars.
/// Updates PlayerPrefs if car is unlocked.
/// </summary>

public class Store : MonoBehaviour
{
    [SerializeField] private CustomiseMenu customise;
    [SerializeField] private GameObject restoreButton;

    private const string TwoCarID = "com.lastdrop.riskyracer.twocar";
    private const string ThreeCarID = "com.lastdrop.riskyracer.threecar";

    public const string TwoCarUnlockedKey = "TwoCarUnlocked";
    public const string ThreeCarUnlockedKey = "ThreeCarUnlocked";

    private void Awake()
    {
        // Allows you to check if app is running on IOS
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        // Check ID to change the right keyword
        if (product.definition.id == TwoCarID)
        {
            PlayerPrefs.SetInt(TwoCarUnlockedKey, 1);
        }      
        else if (product.definition.id == ThreeCarID)
        {
            PlayerPrefs.SetInt(ThreeCarUnlockedKey, 1);
        }

        customise.UpdateMenu();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"Failed to purchase product {product.definition.id} because {reason}");
    }
}
