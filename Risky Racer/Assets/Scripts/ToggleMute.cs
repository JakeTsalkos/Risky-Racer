using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the background music and allows it to toggle on and off
/// </summary>

public class ToggleMute : MonoBehaviour
{
    private Sprite soundOnImage;
    [SerializeField] public Sprite soundOffImage;
    [SerializeField] public Image buttonImage;
    private bool isOn = true;

    private AudioSource audioSource;

    void Start()
    {
        soundOnImage = buttonImage.sprite;
        audioSource = GameObject.Find("Game Handler").GetComponent<AudioSource>();
        if (audioSource.mute)
        {
            buttonImage.sprite = soundOffImage;
            isOn = false;
        }
    }

    public void ButtonClicked()
    {
        if (isOn)
        {
            buttonImage.sprite = soundOffImage;
            isOn = false;
            audioSource.mute = true;
        }
        else
        {
            buttonImage.sprite = soundOnImage;
            isOn = true;
            audioSource.mute = false;
        }
    }
}
