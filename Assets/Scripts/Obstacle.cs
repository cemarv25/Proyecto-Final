using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float movementSpeed = 7;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        if(transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

}
