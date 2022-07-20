using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog_move : MonoBehaviour
{
    


    private bool facingRight = false;
    private bool isGrounded = false;
    private bool isFalling = false;
    private bool isJumping = false;
    [SerializeField] private float jumpForcex = 2f;
    [SerializeField] private float jumpForceY = 4f;
    private float lastYPos = 0;
    [SerializeField] private float groundPosition = 0;
    public static bool die = false;

    public enum Animations
    { 
        Idle = 0,
        Jumping = 1,
        Falling = 2,
        DIE=3
    };
    private Animations currentAnim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;


    [SerializeField] private float idleTime = 4;
    [SerializeField] private float currentIdleTime = 0;
    private bool isIdle = true;
    private int sidemove = 0;

    

    void Start()
    {
        lastYPos =transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime >= idleTime)
            {

                currentIdleTime = 0;

                if (sidemove == 2)
                {
                    facingRight = !facingRight;
                    sidemove = 0;
                }
                sidemove++;
                spriteRenderer.flipX = facingRight;
                jump();
            }
        }
            if(transform.position.y <= groundPosition)
            isGrounded = true;
            else
            isGrounded = false;
           
            if ( isGrounded && !isJumping && !die)
            {
                isFalling = false;
                isJumping = false;
                isIdle = true;
            ChangeAnimation(Animations.Idle);
            this.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (transform.position.y > lastYPos && !isGrounded  && !isIdle && !die)
            {
                isJumping = true;
                isFalling = false;
             this.transform.GetChild(0).gameObject.SetActive(false);
            ChangeAnimation(Animations.Jumping);
            }
            else if (transform.position.y < lastYPos && !isGrounded  && !isIdle && !die)
            {
                isJumping = false;
                isFalling = true;
             this.transform.GetChild(0).gameObject.SetActive(false);
            ChangeAnimation(Animations.Falling);
            }
            else if(die)
            {
            ChangeAnimation(Animations.DIE);
            }
            
            lastYPos = transform.position.y;

        

    }
    private void jump()
    {
        isJumping = true;
        isIdle = false;
        int direction = 0;
        if (facingRight == true)
        {

            direction = 1;
        }
        else
        {
            direction = -1;

        }

        rb.velocity = new Vector2(jumpForcex * direction, jumpForceY);
        Debug.Log("Jump!");
       // isJumping = false;
    }
    void ChangeAnimation(Animations newAnim)
    {
        if (currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.SetInteger("state", (int)newAnim);

        }
    }

}
