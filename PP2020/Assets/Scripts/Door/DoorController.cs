using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorController : MonoBehaviour
{
    public bool requiresKey = false;
    public GameObject key;
    BoxCollider2D boxCollider;
    public GameObject player;
    public GameObject destination;
    public GameObject nextCameraPosition;
    public Camera camera;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size * transform.localScale, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == player)
            {
                if (requiresKey)
                {
                    if (key.GetComponent<KeyController>().unlocked)
                    {
                        GoThroughDoor();
                    }
                } else
                {
                        GoThroughDoor();
                }
            }
        }
    }

    void GoThroughDoor()
    {
        player.GetComponent<CharacterController2D>().respawnPoint = destination;
        player.GetComponent<CharacterController2D>().RevivePlayer();
        camera.transform.position = nextCameraPosition.transform.position;
        camera.GetComponent<ShakeBehavior>().initialPosition = nextCameraPosition.transform.position;
    }
}
