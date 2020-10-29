using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;

    public float playerMoveSpeed = 1f;
    public float jumpPower = 5f;
    public float maxRunSpeed = 5000f;
    public float standingContactDistance = .1f;
    public float stopInputThreshold = .5f;
    public bool capVelocity = true;
    public bool horizontalInputDamping = true;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        if (playerRigidBody != null)
        {
            ApplyInput();
            if (capVelocity)
            {
                CapVelocity();
            }
            else
            {
                Debug.LogWarning("No rigid body at" + gameObject.name);
            }
        }
    }

    void ApplyInput()
    {

        float xInput = horizontalInputDamping ? Input.GetAxis("Horizontal") : Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        //Left and right
        
        float xForce = horizontalInputDamping ? xInput * playerMoveSpeed * Time.deltaTime : Mathf.Sign(xInput) * playerMoveSpeed * Time.deltaTime;
        
        float yForce = 0;

        //Jumping
        if (yInput > 0)
        {
            if (IsOnTopCollider() && playerRigidBody.velocity.y == 0)
            {
                yForce = jumpPower;
            }
        }

        Vector2 force = new Vector2(xForce, yForce);

        playerRigidBody.AddForce(force, ForceMode2D.Impulse);

        if (Mathf.Abs(xInput) <= stopInputThreshold && !horizontalInputDamping)
        {
          playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        } 
    }

    void CapVelocity()
    {
        float cappedXVelocity = Mathf.Min(Mathf.Abs(playerRigidBody.velocity.x), maxRunSpeed) * Mathf.Sign(playerRigidBody.velocity.x);
        float cappedYVelocity = playerRigidBody.velocity.y;

        playerRigidBody.velocity = new Vector2(cappedXVelocity, cappedYVelocity);
    }

    public bool IsOnTopCollider()
    {
        if (playerCollider)
        {
            ContactFilter2D filter2D = new ContactFilter2D();
            filter2D.useTriggers = false;

            RaycastHit2D[] results = new RaycastHit2D[10];

            playerCollider.Cast(new Vector2(0, -1), filter2D, results, standingContactDistance);

            foreach (RaycastHit2D hit in results)
            {
                if (hit.collider && hit.collider.transform.position.z == playerCollider.transform.position.z)
                {
                    return true;
                }
            }

        }
        return false;
    }
}
