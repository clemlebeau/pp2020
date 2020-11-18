using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollingSpeed = .5f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 offset = new Vector2(scrollingSpeed * Time.deltaTime, 0);

        gameObject.GetComponent<Renderer>().material.mainTextureOffset += offset;
    }
}
