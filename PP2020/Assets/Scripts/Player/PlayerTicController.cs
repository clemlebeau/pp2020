using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerTicController : MonoBehaviour
{
    public float minTime = 5f;
    public float maxTime = 10f;
    private float timeLeft;
    public Camera camera;

    public float screenShakeDuration = 2f;

    public CharacterController2D characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }
    void Start()
    {
        timeLeft = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Tic();
            timeLeft = Random.Range(minTime, maxTime);
        }
    }

    void Tic()
    {
        Debug.Log("henlo je suis un tic moment yes moment");
        camera.GetComponent<ShakeBehavior>().TriggerScreenShake(screenShakeDuration);
        int tic = Random.Range(0, 4);
        switch (tic)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
