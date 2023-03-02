using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        Vector3 vec = Vector3.zero;
        if (Application.platform == RuntimePlatform.Android)
        {
            vec.x = Input.acceleration.x * speed;
            if (vec.sqrMagnitude > 1)
                vec.Normalize();
        }
        else
        {
            vec.x = Input.GetAxisRaw("Horizontal");
        }

        //transform.Translate(vec * speed);
        rb.velocity = vec * speed;
        //rb.AddForce(vec * speed);
    }
}
