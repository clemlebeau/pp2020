using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KeyController : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D boxCollider;
    [NonSerialized]
    public bool unlocked = false;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!unlocked)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size * transform.localScale, 0);

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == player)
                {
                    unlocked = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<Hovering>().enabled = false;
                }
            }
        }
    }
}
