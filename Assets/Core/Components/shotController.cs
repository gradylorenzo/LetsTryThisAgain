using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotController : MonoBehaviour
{
    public float speed = 2.0f;
    public float lifespan = 10.0f;
    public Vector3 targetPoint;

    private Vector3 startPoint;
    private float startTime;

    private void Awake()
    {
        startPoint = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time > startTime + lifespan)
        {
            Die();
        }

        if(Vector3.Distance(transform.position, startPoint) > Vector3.Distance(targetPoint, startPoint))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed);
    }

    public void SetTargetPoint(Vector3 p)
    {
        targetPoint = p;
    }
}
