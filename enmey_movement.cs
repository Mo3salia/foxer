using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmey_movement : MonoBehaviour
{
    [Range(0, 6)] [SerializeField] private float Movespeed=1f;
     private Rigidbody2D MyRigidbody;
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingRight())
            MyRigidbody.velocity = new Vector2(Movespeed, 0f);
        else
            MyRigidbody.velocity = new Vector2(- Movespeed, 0f);

    }

    private bool IsFacingRight()
    {

        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(MyRigidbody.velocity.x)),transform.localScale.y);
    }
}
