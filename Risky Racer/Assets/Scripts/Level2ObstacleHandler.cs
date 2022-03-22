using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the moving obstacles of Level 2.
/// Currently controls the trucks movement patterns based on target position.
/// </summary>

public class Level2ObstacleHandler : MonoBehaviour
{
    [SerializeField] private GameObject position1;
    [SerializeField] private GameObject position2;
    [SerializeField] private GameObject position3;
    [SerializeField] private GameObject position4;

    [SerializeField] private GameObject truck;

    [SerializeField] private float truckMoveSpeed;
    [SerializeField] private bool isTurningRight;
    [SerializeField] private Car car;

    private string target = "Position2";

    void Start()
    {
        truck.transform.position = position1.transform.position;
    }

    void Update()
    {
        if (car.hasCrashed)
        { 
            Invoke(nameof(carCrashed), 3f);
        }
                 
        switch (target)
        {
            case "Position1":
                truck.transform.position = Vector3.MoveTowards(truck.transform.position, position1.transform.position, truckMoveSpeed * Time.deltaTime);
                break;

            case "Position2":
                truck.transform.position = Vector3.MoveTowards(truck.transform.position, position2.transform.position, truckMoveSpeed * Time.deltaTime);
                break;

            case "Position3":
                truck.transform.position = Vector3.MoveTowards(truck.transform.position, position3.transform.position, truckMoveSpeed * Time.deltaTime);
                break;

            case "Position4":
                truck.transform.position = Vector3.MoveTowards(truck.transform.position, position4.transform.position, truckMoveSpeed * Time.deltaTime);
                break;
        }    
            
        if (truck.transform.position == position1.transform.position)
        {
            target = "Position2";
            if (isTurningRight)
            {
                truck.transform.Rotate(0, 90, 0);
                return;
            }
            truck.transform.Rotate(0, -90, 0);
        }
        else if (truck.transform.position == position2.transform.position)
        {
            target = "Position3";
            if (isTurningRight)
            {
                truck.transform.Rotate(0, 90, 0);
                return;
            }
            truck.transform.Rotate(0, -90, 0);
        }
        else if(truck.transform.position == position3.transform.position)
        {
            target = "Position4";
            if (isTurningRight)
            {
                truck.transform.Rotate(0, 90, 0);
                return;
            }
            truck.transform.Rotate(0, -90, 0);
        }
        else if (truck.transform.position == position4.transform.position)
        {
            target = "Position1";
            if (isTurningRight)
            {
                truck.transform.Rotate(0, 90, 0);
                return;
            }
            truck.transform.Rotate(0, -90, 0);
        }
    }

    private void carCrashed()
    {
        Destroy(this);
    }
}
