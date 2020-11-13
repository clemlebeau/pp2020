using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CatController : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Distance from the player at which the cat will stop moving")]
    float distanceTolerance = .5f;

    [SerializeField, Tooltip("Time it takes for the cat to jump on the player if he doesn't move for too long")]
    float defaultTimeToJump = 5f;
    float timeToJump;

    public GameObject player;

    private BoxCollider2D boxCollider;

    private bool grounded;

    private Vector2 velocity;

    CharacterController2D playerController;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        timeToJump = defaultTimeToJump;
        playerController = player.GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        float moveInput = 0f;

        if (grounded)
        {
            velocity.y = 0;
        }

        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) >= distanceTolerance)
        {
            moveInput = -Mathf.Sign(gameObject.transform.position.x - player.transform.position.x);
            timeToJump = defaultTimeToJump;
        }
        else
        {
            timeToJump -= Time.deltaTime;
        }

        if (timeToJump <= 0 && player.transform.position.y > gameObject.transform.position.y && grounded)
        {
            velocity.y = Mathf.Sqrt(2 * Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) * Mathf.Abs(Physics2D.gravity.y));
            timeToJump = defaultTimeToJump;
        }

        if (!playerController.alive)
        {
            timeToJump = defaultTimeToJump;
            moveInput = 0;
        }

        float acceleration = grounded ? walkAcceleration : 0; //could be airAcceleration, depends
        float deceleration = groundDeceleration;


        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);

        if (velocity.x != 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = velocity.x < 0;
            //transform.localScale = new Vector3(transform.localScale.x * Mathf.Sign(velocity.x), transform.localScale.y, transform.localScale.z);
        }

        grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size * transform.localScale, 0);
        foreach (Collider2D hit in hits)
        {
            if(hit.gameObject == player)
            {
                playerController.KillPlayer();
            }
            //if (hit == boxCollider || hit.isTrigger)
            //    continue;
            if(hit.gameObject.layer != 3)//Layer 3 is ground, so this if statement makes sure that the cat goes through everything except the ground, but there may be a problem with walls.
            {
                continue;
            }
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }
}
