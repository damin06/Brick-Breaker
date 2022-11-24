using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class blcok : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject scoreTXT;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject particle;
    [SerializeField] private AudioClip destrouAudio;
    [SerializeField] private AudioClip ponitAudio;
    [SerializeField] private TextMeshProUGUI blockHPTXT;


    [SerializeField] private float speed = 4;
    GameManager gameManager;
    private int blockscore = 0;
    private int blockHP;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        if (gameManager.stage >= 0 && gameManager.stage < 10)
        {
            blockHP = UnityEngine.Random.Range(1, 4);
        }
        else if (gameManager.stage >= 10 && gameManager.stage < 20)
        {
            blockHP = UnityEngine.Random.Range(1, 5);
        }
        else if (gameManager.stage >= 20 && gameManager.stage < 30)
        {
            blockHP = UnityEngine.Random.Range(2, 6);
        }
        else if (gameManager.stage > 30)
        {
            blockHP = UnityEngine.Random.Range(3, 7);
        }
        blockHPTXT.text = blockHP.ToString();
        blockscore = blockHP * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isMove)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (blockHP == 1)
        {
            spriteRenderer.color = new Color(0.9529412f, 0.6235294f, 0.4509804f);
        }
        else if (blockHP == 2)
        {
            spriteRenderer.color = new Color(0.9686275f, 0.5607843f, 0.4156863f);
        }
        else if (blockHP == 3)
        {
            spriteRenderer.color = new Color(0.9843138f, 0.5019608f, 0.3803922f);
        }
        else if (blockHP == 4)
        {
            spriteRenderer.color = new Color(0.9960785f, 0.4392157f, 0.3529412f);
        }
        else if (blockHP == 5)
        {
            spriteRenderer.color = new Color(1, 0.372549f, 0.3333333f);
        }
        else if (blockHP == 6)
        {
            spriteRenderer.color = new Color(1, 0.3098039f, 0.3176471f);
        }
        else if (blockHP == 7)
        {
            spriteRenderer.color = new Color(1, 0.254902f, 0.3176471f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            blockHP--;
            if (blockHP < 1)
            {
                AudioSource.PlayClipAtPoint(ponitAudio, transform.position);
                gameManager.score += blockscore;
                gameManager.blockcount--;
                GameObject scoreText = Instantiate(scoreTXT);
                scoreText.GetComponent<scoretXT>().socre = blockscore;
                scoreText.transform.position = transform.position;
                GameObject particleins = Instantiate(particle);
                particleins.transform.position = gameObject.transform.position;
                int random = UnityEngine.Random.Range(0, 5);
                if (random < 2)
                {
                    GameObject gameitem = Instantiate(item);
                    gameitem.transform.position = transform.position;
                    Destroy(gameitem, 3);
                }
                Destroy(gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(destrouAudio, transform.position);
            }
            blockHPTXT.text = blockHP.ToString();
        }
        // if(other.gameObject.CompareTag("wall"))
        // {
        //     gameManager.blockcount--;
        //     Destroy(other.gameObject);
        // }
    }
}
