using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float movementSpeed = 7;
    public GameObject powerUpIndicator;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * movementSpeed);

        if(transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

}
