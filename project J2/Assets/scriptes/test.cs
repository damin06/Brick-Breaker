using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 10f;
    GameManager gameManager;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void update()
    {
        if(gameManager.PauseActive)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        rigidbody.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 3.55f);
    }

    public void SetRandomTrajectory()
    {
        Vector2 force = new Vector2();
        force.x = UnityEngine.Random.Range(-1f, 1f);
        force.y = -1f;

        rigidbody.AddForce(force.normalized * speed);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }
}
