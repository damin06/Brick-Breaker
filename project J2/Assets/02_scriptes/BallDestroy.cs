using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroy : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private AudioClip destrouAudio;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            gameManager.ballcount--;
            other.gameObject.SetActive(false);
            if (gameManager.ballcount <= 0)
            {
                AudioSource.PlayClipAtPoint(destrouAudio, new Vector3(0, 0, 0));
            }
        }
    }
}
