using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerTicController : MonoBehaviour
{
    public float minTime = 5f;
    public float maxTime = 10f;
    private float timeLeft;

    public float minMoveTicTime = 1f;
    public float maxMoveTicTime = 1.5f;


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
        if (characterController.alive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Tic();
                timeLeft = Random.Range(minTime, maxTime);
            }
        }
    }

    void Tic()
    {
        camera.GetComponent<ShakeBehavior>().TriggerScreenShake(screenShakeDuration);
        int tic = Random.Range(0, 2);
        switch (tic)
        {
            case 0:
                //Jump
                characterController.ticJump = true;
                break;
            case 1:
                //Move left
                characterController.ticMove = -1;
                characterController.moveTicTime = Random.Range(minMoveTicTime, maxMoveTicTime);
                break;
            case 2:
                //Move right
                characterController.ticMove = 1;
                characterController.moveTicTime = Random.Range(minMoveTicTime, maxMoveTicTime);
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
