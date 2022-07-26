using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class eagleAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float NextWayPointDistance = 3f;
    [SerializeField] private Transform enemyGfx;
    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D Rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        Rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 diraction = ((Vector2)path.vectorPath[currentWayPoint] - Rb.position).normalized;
        Vector2 force = diraction * speed * Time.deltaTime;
        Rb.AddForce(force);
        float distance = Vector2.Distance(Rb.position,path.vectorPath[currentWayPoint]);

        if(distance < NextWayPointDistance)
        {
            currentWayPoint++;
        }
        if (force.x >= 0.01f)
        {
            enemyGfx.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGfx.localScale = new Vector3(1f, 1f, 1f);
        }
    }   


   void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(Rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete (Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
}
