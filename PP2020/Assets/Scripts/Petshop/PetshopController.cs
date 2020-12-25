using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class PetshopController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public GameObject player;
    public GameObject fadeImage;
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

        foreach(Collider2D hit in hits)
        {
            if (hit.gameObject == player)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        player.GetComponent<CharacterController2D>().gameEnded = true;
        //Time.timeScale = 0;
        fadeImage.SetActive(true);
    }
}
