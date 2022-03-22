using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Handles the countdown timer at the start of the level, displaying a simple countdown then disappearing.
/// </summary>

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject canvas;
    private int countdownTime = 3;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(0.5f);

        countdownText.CrossFadeAlpha(0, 1f, false);
    }
}
