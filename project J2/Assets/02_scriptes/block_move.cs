using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_move : MonoBehaviour
{
    [SerializeField] private GameObject ballobjectl;
    [SerializeField] stageData stageData;
    [SerializeField] private AudioClip itemAudio;
    public float moveSpeed = 0.7f;
    private Quaternion basePosition = new(0, 0, 0, 0);
    public float maxBounceAngle = 75f;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.PauseActive)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 vec = Vector3.zero;
        vec.x = Input.acceleration.x * moveSpeed;
        if (vec.sqrMagnitude > 1)
            vec.Normalize();

        // vec*=Time.deltaTime; 
        transform.Translate(vec * moveSpeed);
    }
    private void LateUpdate()
    {
        //움직인 제안
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                                              Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            AudioSource.PlayClipAtPoint(itemAudio, new Vector3(0, 0, 0));
            GameObject ballclone = Instantiate(ballobjectl);
            ballclone.transform.position = new Vector3(0, 0, 0);
            gameManager.ballcount++;
            Destroy(collision.gameObject);
        }
        test ball = collision.gameObject.GetComponent<test>();

        if (ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / maxOffset) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
    public void speedbar(float speed)
    {
        moveSpeed = speed;
    }
}
