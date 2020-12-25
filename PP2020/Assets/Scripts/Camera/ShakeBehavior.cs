using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    private Transform transform;

    private float shakeDuration = 0f;

    public float shakeMagnitude = .7f;

    public float dampingSpeed = 1f;

    [NonSerialized]
    public Vector3 initialPosition;

    void Awake()
    {
        transform = GetComponent<Transform>();
    }
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if(shakeDuration > 0)
        {
            transform.localPosition = initialPosition + UnityEngine.Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;//NEEDS TO BE CHANGED bc it causes bugs with the follow player script
        }
    }

    public void TriggerScreenShake(float duration)
    {
        shakeDuration = duration;
    }
}
