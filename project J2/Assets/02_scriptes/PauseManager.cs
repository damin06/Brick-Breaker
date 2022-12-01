using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text PausScoreTXT;
    [SerializeField] private GameObject pauspanel;
    [SerializeField] private Image blakcfade;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private Slider SpeedBarSlider;
    [SerializeField] private Button PauseButton;

    private GameManager GM;
    test BallOBj;
    public float moveSpeed;
    void Start()
    {
        //GM.GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GM = FindObjectOfType<GameManager>();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PauseButton.gameObject.SetActive(false);
        }
        else
        {
            PauseButton.gameObject.SetActive(true);
        }
    }

    public void GamePuase()
    {
        //if (GM.GameOver == false)
        //{
        // if (GM.PauseActive == false)
        // {
        PausScoreTXT.text = "SCORE : " + GM.score.ToString();

        GM.PauseActive = true;
        blakcfade.gameObject.SetActive(true);
        pauspanel.SetActive(true);
        Time.timeScale = 0;
        // }
        // else
        // {
        //     //StartCoroutine(GameCount());

        // }
        //}
    }

    public void QuitPause()
    {
        GM.PauseActive = false;
        blakcfade.gameObject.SetActive(false);
        pauspanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void voluem()
    {
        AudioListener.volume = VolumeSlider.value;
    }

    public void speedbar()
    {
        moveSpeed = SpeedBarSlider.value;
    }

    public void home()
    {
        QuitPause();
        SceneManager.LoadScene("home");
    }
}
