using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class PlayerFollower : MonoBehaviour
{
    public GameObject player;
    
    public bool followPlayerOnX = false;
    public bool followPlayerOnY = true;

    public float xBuffer = 1f;
    public float yBuffer = 1f;

    public Vector3 cameraVelocity = new Vector3(0,1,0);
    public float smoothTime = 2f;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        if(followPlayerOnX)
        {
            if(screenPosition.x < xBuffer || screenPosition.x > (Screen.width - xBuffer))
            {
                MoveCameraToPlayer();
            }
        }

        if(followPlayerOnY)
        {
            if (screenPosition.y < yBuffer || screenPosition.y > (Screen.height - yBuffer))
            {
                MoveCameraToPlayer();
            }
        }

    }

    void MoveCameraToPlayer()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, smoothTime);
    }
}
