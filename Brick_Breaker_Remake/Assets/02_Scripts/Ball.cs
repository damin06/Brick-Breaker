using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void update()
    {

        rb.velocity = Vector2.zero;

    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(0, -1, 0);

        Invoke(nameof(SetRandomTrajectory), 3);
    }

    public void SetRandomTrajectory()
    {
        Vector2 force = new Vector2();
        force.x = UnityEngine.Random.Range(-1f, 1f);
        force.y = -1f;

        rb.AddForce(force.normalized * speed);
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }
}
