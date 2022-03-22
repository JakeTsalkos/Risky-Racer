using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles controlling of car turning and speed, as well as visual feedback like wheels turning.
/// Also handles collision with objects to end the current run of the level or collect coins.
/// </summary>

public class Car : MonoBehaviour
{
    [SerializeField] private Crashed crash;
    [SerializeField] private Coins coin;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 125f;
    [SerializeField] private float turnSpeedGainPerSecond = 0.6f;
    [SerializeField] private AudioSource audioSource;
    private GameObject FLW;
    private GameObject FRW;
    private GameObject RLW;
    private GameObject RRW;

    private int steerValue;
    private float steerAngle;
    private float spinRotation = 1000;
    public bool hasCrashed = false;
    public bool canDrive = false;

    private void Start()
    {            
        Invoke(nameof(TurnOnDriving), 3f);
    }

    // Movement of car is handled in Update, based on the steerValue and preset speeds
    void Update()
    {
        if (!hasCrashed && canDrive)
        {
            speed += speedGainPerSecond * Time.deltaTime;
            turnSpeed += turnSpeedGainPerSecond * Time.deltaTime;

            transform.Rotate(0, steerValue * turnSpeed * Time.deltaTime, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            TurnWheels(steerValue);
        }
    }

    // Handles different behaviour based on tag of object collided with
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            hasCrashed = true;
            PlayExplosionEffect();
            crash.CarCrashed();
        }
        else if (other.CompareTag("Coin"))
        {
            coin.CollectCoin(other.gameObject);
        }
        else if (other.CompareTag("RespawnGate"))
        {
            coin.RespawnCoins();
        }
        else if (other.CompareTag("Portal"))
        {
            gameObject.transform.position = new Vector3(0f, 0.25f, -20f);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void TurnOnDriving()
    {
        canDrive = true;
    }

    // Turns the front wheels based on which direction the car is turning, resets to straight with no input
    private void TurnWheels(int value)
    {
        steerAngle = steerAngle + value * 100 * Time.deltaTime;
        spinRotation = spinRotation + speed * Time.deltaTime;

        if (steerAngle < -30f)
        {
            steerAngle = -30f;
        }
        else if (steerAngle > 30f)
        {
            steerAngle = 30f;
        }

        switch (steerValue)
        {
            case 0:

                if (steerAngle < -1)
                {
                    steerAngle = steerAngle + 1 * 100 * Time.deltaTime;
                }
                else if (steerAngle > 1)
                {
                    steerAngle = steerAngle - 1 * 100 * Time.deltaTime;
                }
                else
                {
                    steerAngle = 0;
                }
                FLW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                FRW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                break;

            case 1:
                FLW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                FRW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                break;

            case -1:
                FLW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                FRW.transform.localRotation = Quaternion.Euler(spinRotation * 10, steerAngle, 0);
                break;
        }
        RLW.transform.localRotation = Quaternion.Euler(spinRotation * 10, 0, 0);
        RRW.transform.localRotation = Quaternion.Euler(spinRotation * 10, 0, 0);
    }

    private void PlayExplosionEffect()
    {
        gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
        audioSource.Play();
    }

    public void AllocateWheels()
    {
        FLW = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        FRW = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        RLW = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        RRW = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
    }

    public void Steer(int value)
    {
        steerValue = value;
    }   
}
