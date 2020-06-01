﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRotation : MonoBehaviour
{
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }
}
