using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]

public class Ghoul : MonoBehaviour
{

    public float walkspeed = 3f;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;

    public enum WalkingDirection { Right, Left }

    private WalkingDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkingDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (_walkDirection == WalkingDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (_walkDirection == WalkingDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }


        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkingDirection.Right)
        {
            WalkDirection = WalkingDirection.Left;
        }
        else if (WalkDirection == WalkingDirection.Left) 
        { 
            WalkDirection = WalkingDirection.Right;
        }
        else
        {
            Debug.LogError("This object is screwed");
        }
    }


    private void FixedUpdate()
    {
        if (touchingDirection.isGrounded && touchingDirection.isOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkspeed * walkDirectionVector.x, rb.velocity.y);
    }

}
