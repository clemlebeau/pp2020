using System;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Deceleration applied when character is in the air and not attempting to move.")]
    float airDeceleration = 0;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Animator animator;

    private Vector2 velocity;

    private bool grounded;

    public bool alive;

    public Color deadColor = new Color(198f / 255, 20f / 255, 20f / 255);

    private SpriteRenderer playerSpriteRenderer;

    public GameObject firstRespawnPoint;
    [NonSerialized]
    public GameObject respawnPoint;
    
    public float respawnTime = 5f;
    float respawnTimer = 0;

    public GameObject cat;
    public float catDelay = 5f;
    private float catSpawnTimer;
    private bool catAlive = false;

    [NonSerialized]
    public bool gameEnded = false;

    #region Tic variables

    PlayerTicController ticController;



    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        alive = true;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        ticController = GetComponent<PlayerTicController>();
        respawnPoint = firstRespawnPoint;
        catSpawnTimer = catDelay;
    }

    private void Start()
    {
        RevivePlayer();
    }

    private float GetHorizontalAxis()
    {
        if (!alive || ticController.whistleTicTime > 0)
            return 0;
        float axis = Input.GetAxisRaw("Horizontal");
        if (ticController.ticMove != 0)
        {
            if (ticController.moveTicTime > 0)
            {
                axis = ticController.ticMove;
            }
        }

        return axis;
    }

    private bool GetJump()
    {
        if (!alive || ticController.whistleTicTime > 0)
            return false;
        bool willJump = Input.GetButtonDown("Jump");
        if (ticController.ticJump)
        {
            willJump = true;
            ticController.ticJump = false;
        }
        return willJump;
    }

    private void Update()
    {

        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        else if (!alive && respawnTimer <= 0)
        {
            RevivePlayer();
        }

        if (!catAlive)
        {
            if (catSpawnTimer > 0)
            {
                catSpawnTimer -= Time.deltaTime;
            }
            else if (respawnTimer <= 0)
            {
                SpawnCat();
            }
        }

        float moveInput = GetHorizontalAxis();

        if (grounded)
        {
            velocity.y = 0;

            if (GetJump())
            {
                //Calculate the velocity required to achieve the target jump height with a derived known formula
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : airDeceleration;


        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        animator.SetFloat("playerSpeed", Mathf.Abs(velocity.x));

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);

        if (velocity.x != 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = velocity.x < 0;
            //transform.localScale = new Vector3(transform.localScale.x * Mathf.Sign(velocity.x), transform.localScale.y, transform.localScale.z);
        }

        grounded = false;

        //If character is getting pushed into unwanted colliders, re run this multiple times until there are no more collisions.
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider || hit.isTrigger)
                continue;
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                
                if (hit.bounds.center.y + hit.bounds.size.y / 2 > gameObject.GetComponent<Renderer>().bounds.center.y - gameObject.GetComponent<Renderer>().bounds.size.y)
                {
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
                else
                {
                    transform.Translate((colliderDistance.pointA - colliderDistance.pointB) * Time.deltaTime);
                }
                //if(hit.transform.position.y > gameObject.transform.position.y)
                //{
                //    velocity.y = 0; //set velocity to zero if the player collides with something above him to prevent "sticking" under something while jumping
                //}

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }

    public void KillPlayer()
    {
        if (alive)
        {
            alive = false;
            playerSpriteRenderer.color = deadColor;

            respawnTimer = respawnTime;
        }
    }

    public void RevivePlayer()
    {
        gameObject.transform.position = respawnPoint.transform.position;
        alive = true;
        playerSpriteRenderer.color = new Color(1, 1, 1);

        KillCat();
        catSpawnTimer = catDelay;
        catAlive = false;
    }

    public void KillCat()
    {
        cat.GetComponent<CatController>().enabled = false;
        cat.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SpawnCat()
    {
        catAlive = true;
        cat.transform.position = respawnPoint.transform.position;
        cat.GetComponent<CatController>().enabled = true;
        cat.GetComponent<SpriteRenderer>().enabled = true;
    }
}

//allo^pipi
