using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerTicController : MonoBehaviour
{
    public float minTime = 5f;
    public float maxTime = 10f;
    private float timeLeft;

    [NonSerialized]
    public float whistleTicTime = 0f;
    public float defaultWhistleTicTime = 2f;
    public float minNoteSpawnTime = 0f;
    public float maxNoteSpawnTime = .25f;
    private float nextNoteSpawn;
    public GameObject notePrefab;

    [NonSerialized]
    public bool ticJump = false;

    public float minMoveTicTime = 1f;
    public float maxMoveTicTime = 1.5f;
    [NonSerialized]
    public float ticMove = 0f;
    [NonSerialized]
    public float moveTicTime;


    public Camera camera;

    public float screenShakeDuration = 2f;

    public CharacterController2D characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
    }
    void Start()
    {
        timeLeft = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        if (characterController.alive)
        {
            TicTimers();
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Tic();
                ResetTicTime();
            }

            Whistle();
        }
    }

    public void ResetTicTime()
    {
        timeLeft = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Whistle()
    {
        if (whistleTicTime > 0)
        {
            nextNoteSpawn -= Time.deltaTime;
            if (nextNoteSpawn <= 0)
            {
                nextNoteSpawn = UnityEngine.Random.Range(minNoteSpawnTime, maxNoteSpawnTime);
                SpawnNote();
            }
        }
    }

    void SpawnNote()
    {
        Vector3 spawnPosition = transform.position;
        int direction = GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        spawnPosition.x += (.35f) * direction;
        spawnPosition.x += UnityEngine.Random.Range(-.1f, .1f);
        spawnPosition.y += UnityEngine.Random.Range(-.1f, .1f) + .5f;

        Instantiate(notePrefab, spawnPosition, transform.rotation);
    }

    void TicTimers()
    {
        whistleTicTime -= Time.deltaTime;
        moveTicTime -= Time.deltaTime;
    }
    void Tic()
    {
        camera.GetComponent<ShakeBehavior>().TriggerScreenShake(screenShakeDuration);
        int tic = UnityEngine.Random.Range(0, 3);
        switch (tic)
        {
            case 0:
                //Jump
                ticJump = true;
                break;
            case 1:
                //Move left
                ticMove = -1;
                moveTicTime = UnityEngine.Random.Range(minMoveTicTime, maxMoveTicTime);
                break;
            case 2:
                //Move right
                ticMove = 1;
                moveTicTime = UnityEngine.Random.Range(minMoveTicTime, maxMoveTicTime);
                break;
            case 3:
                //Whistle
                whistleTicTime = defaultWhistleTicTime;
                nextNoteSpawn = UnityEngine.Random.Range(minNoteSpawnTime, maxNoteSpawnTime);
                break;
        }
    }
}
