using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    public float hoverHeight = .25f;
    public float hoverSpeed = .5f;
    Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, hoverHeight * Mathf.Cos(Time.time * hoverSpeed) + initialPosition.y, initialPosition.z);
    }
}
