using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PillController))]
public class PillCollisionDetection : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject player;
    private PillController pillController;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        pillController = GetComponent<PillController>();
        if(player == null)
        {
            Debug.LogWarning("Player is not assigned!");
        }
    }

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size * transform.localScale, 0);

        foreach(Collider2D hit in hits)
        {
            if (hit.gameObject == player)
            {
                pillController.ConsumePill();
            }
        }
    }
}
