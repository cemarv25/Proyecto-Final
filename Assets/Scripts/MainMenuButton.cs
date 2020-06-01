using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public GameObject[] cars;
    public void SelectCar(int carIndex)
    {
        // Destroy other cars
        for(int i = 0; i < cars.Length; i++)
        {
            if(i != carIndex)
            {
                Destroy(cars[i]);
            }
            else if (i == carIndex)
            {
                cars[i].transform.position = new Vector3(-15, 0, -7);
                cars[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                cars[i].GetComponent<MainMenuRotation>().enabled = false;
                cars[i].GetComponent<PlayerController>().enabled = true;
            }
        }
    }
}
