using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAR_move : MonoBehaviour
{
    [SerializeField] private float PointA;
    [SerializeField] private float PointB;
    private float move = -0.3f;
    private Rigidbody2D m_Rigidbody2D;

    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        moveEnmey();

    }
    private void moveEnmey()
    {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (transform.position.x <= PointA)
        {
            // ... flip the player.
            transform.localScale = new Vector3(-1, 1, 1);
            move = 0.3f;
            targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (transform.position.x >= PointB)
        {
            // ... flip the player.
            transform.localScale = new Vector3(1, 1, 1); ;
            move = -0.3f;
            targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }
}
    
   
