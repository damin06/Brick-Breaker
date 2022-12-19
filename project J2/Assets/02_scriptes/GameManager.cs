using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class GameManager : MonoBehaviour
{

    Vector3 pos1 = new Vector3(-7, 5.5f, 0);
    Vector3 pos2 = new Vector3(-3.5f, 5.5f, 0);
    Vector3 pos3 = new Vector3(0, 5.5f, 0);
    Vector3 pos4 = new Vector3(3.5f, 5.5f, 0);
    Vector3 pos5 = new Vector3(7, 5.5f, 0);
    Vector3 pos6 = new Vector3(-5.25f, 5.5f, 0);
    Vector3 pos7 = new Vector3(-1.75f, 5.5f, 0);
    Vector3 pos8 = new Vector3(1.75f, 5.5f, 0);
    Vector3 pos9 = new Vector3(5.25f, 5.5f, 0);


    [SerializeField] private JSON jSON;

    [Header("UI")]
    [SerializeField] Button quitbutton;
    //public GameObject pauspanel;
    [SerializeField] private Image backgorund;
    [SerializeField] private TextMeshProUGUI scoreTXT;
    [SerializeField] private TextMeshProUGUI gameoverTXT;
    [SerializeField] private TextMeshProUGUI bestsocteTXT;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI countTXT;
    private RectTransform backgroundRext;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueTXT;
    [SerializeField] private TextMeshProUGUI continueCountTXT;



    [SerializeField] stageData stageData;
    [SerializeField] private GameObject wall;
    private PauseManager pauseManager;
    private AdMobManager ad;
    private block_move bl;
    private RewardedAd rewardedAd;

    // [SerializeField] private GameObject item1;
    // [SerializeField] private GameObject item2;




    public bool GameOver = false;
    public bool PauseActive = false;
    public bool isMove = false;
    private bool isReStarted = false;


    public int ballcount = 1;
    public int score = 0;
    private int bestscore;
    public float time;
    public int stage = 0;
    private float currentcooltime;
    [SerializeField] private float cooltime = 10;

    [SerializeField] private float downtime = 0.24f;
    public int blockcount = 0;
    private float voluemSetting;
    test Test;


    void Awake()
    {
        jSON.LoadPlayerDataToJson();
        Data PlayerData = jSON.playerData;
        Test = GameObject.FindObjectOfType<test>().GetComponent<test>();
        StartCoroutine(spawnwall());
        StartCoroutine(GameCount());
        pauseManager = FindObjectOfType<PauseManager>();
        backgroundRext = backgorund.GetComponent<RectTransform>();
        ad = FindObjectOfType<AdMobManager>();
        bl = FindObjectOfType<block_move>();

        GameOver = false;

        var requestConfiguration = new RequestConfiguration
   .Builder()
   .SetTestDeviceIds(new List<string>() { "1DF7B7CC05014E8" }) // test Device ID
   .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);

        // if (PausScoreTXT = null)
        // {
        //     PausScoreTXT = Text.Ga;
        // }

        //bestscore = PlayerData.bestscore;
        //voluemSetting = PlayerData.voluem;
        //bestscore = PlayerPrefs.GetInt("bestscore");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (blockcount <= 0)
        {
            StartCoroutine(spawnwall());
            return;
        }
        if (ballcount <= 0 && !GameOver)
        {
            StartCoroutine(endgame());
            return;
        }

        //currentcooltime+=Time.deltaTime;
        // if(currentcooltime>cooltime)
        // {
        //     StartCoroutine(spawn());
        //     currentcooltime=0;
        // }
    }
    // IEnumerator spawnwall()
    // {
    // List<Vector3> spawnlist = new List<Vector3>() {pos1,pos2,pos3,pos4,pos5,pos6,pos7,pos8,pos9,pos10,pos11,pos12,pos13};
    // int spawncount = Random.Range(1,14);
    //     for(int i = 0; i < spawncount; i++)
    //     {
    //         int rand = Random.Range(0,spawnlist.Count);
    //         GameObject block = Instantiate(wall);
    //         block.transform.position=spawnlist[rand];
    //         spawnlist.RemoveAt(rand);
    //     }
    //     yield return null;
    // }
    IEnumerator spawnwall()
    {
        int count = Random.Range(1, 4);
        for (int a = 1; a < count; a++)
        {
            StartCoroutine(spawn1());
            yield return new WaitForSeconds(1);
            StartCoroutine(spawn2());
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator spawn()
    {
        if (stage % 2 == 0)
        {
            List<Vector3> spawnlist = new List<Vector3>() { pos6, pos7, pos8, pos9 };
            int spawncount = Random.Range(0, spawnlist.Count);
            for (int i = 0; i < spawncount; i++)
            {
                int rand = Random.Range(0, spawnlist.Count);
                GameObject block = Instantiate(wall);
                block.transform.position = spawnlist[rand];
                spawnlist.RemoveAt(rand);
                blockcount++;
            }
        }
        else if (stage % 2 == 1)
        {
            List<Vector3> spawnlist = new List<Vector3>() { pos1, pos2, pos3, pos4, pos5 };
            int spawncount = Random.Range(0, spawnlist.Count);
            for (int i = 0; i < spawncount; i++)
            {
                int rand = Random.Range(0, spawnlist.Count);
                GameObject block = Instantiate(wall);
                block.transform.position = spawnlist[rand];
                spawnlist.RemoveAt(rand);
                blockcount++;
            }
        }
        yield return new WaitForSeconds(0.1f);
        stage++;
        isMove = true;
        yield return new WaitForSeconds(downtime);
        isMove = false;
    }
    IEnumerator spawn1()
    {
        List<Vector3> spawnlist = new List<Vector3>() { pos1, pos2, pos3, pos4, pos5 };
        int spawncount = Random.Range(0, spawnlist.Count);
        for (int i = 0; i < spawncount; i++)
        {
            int rand = Random.Range(0, spawnlist.Count);
            GameObject block = Instantiate(wall);
            block.transform.position = spawnlist[rand];
            spawnlist.RemoveAt(rand);
            blockcount++;
        }
        stage++;
        isMove = true;
        yield return new WaitForSeconds(downtime);
        isMove = false;
    }

    IEnumerator spawn2()
    {
        List<Vector3> spawnlist = new List<Vector3>() { pos6, pos7, pos8, pos9 };
        int spawncount = Random.Range(0, spawnlist.Count);
        for (int i = 0; i < spawncount; i++)
        {
            int rand = Random.Range(0, spawnlist.Count);
            GameObject block = Instantiate(wall);
            block.transform.position = spawnlist[rand];
            spawnlist.RemoveAt(rand);
            blockcount++;
        }
        stage++;
        isMove = true;
        yield return new WaitForSeconds(downtime);
        isMove = false;
    }

    // IEnumerator spawnItem()
    // {
    //     float x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
    //     float y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);
    //     Vector3 vec =new Vector3(x,y,0);
    //     int randomitme = Random.Range(1,3);

    //     switch(randomitme)
    //     {
    //             case 1 :
    //         GameObject itemA = Instantiate(item1);
    //         item1.transform.position = vec;
    //         break;
    //             case 2 :
    //         GameObject itemB = Instantiate(item2);
    //         itemB.transform.position = vec;
    //         break;
    //     }

    //     float randomspawntime =Random.Range(2,5.8f);
    //     yield return new WaitForSeconds(randomspawntime);
    //     StartCoroutine(spawnItem());
    // }
    IEnumerator endgame()
    {
        //PauseActive = true;
        pauseManager.QuitPause();
        //PauseActive = false;
        Handheld.Vibrate();
        GameOver = true;

        //blakcfade.gameObject.SetActive(false);
        //pauspanel.SetActive(false);
        //Time.timeScale = 1;
        //backgorund.transform.DOMove(new Vector3(430, 240, 0), 2);
        backgroundRext.DOAnchorPosY(0, 2);
        yield return new WaitForSeconds(0.5f);
        if (score > bestscore)
        {
            bestscore = score;
            //PlayerPrefs.SetInt("bestscore", bestscore);

            Data PlayerData = jSON.playerData;
            PlayerData.bestscore = bestscore;

            bestsocteTXT.DOFade(1, 2);
            StartCoroutine(bestscoreText());
        }
        jSON.SavePlayerDataToJson();
        scoreTXT.text = "score : " + score.ToString();
        scoreTXT.DOFade(1, 2);
        gameoverTXT.DOFade(1, 2);

        isRe();
        //quitbutton.gameObject.SetActive(true);
    }

    private void isRe()
    {
        if (!isReStarted)
        {
            StartCoroutine("ContinueGameTXT");
        }
        else
        {
            restartButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            StopCoroutine("ContinueGameTXT");
        }
    }
    IEnumerator ContinueGameTXT()
    {
        continueButton.gameObject.SetActive(true);
        continueTXT.DOFade(1, 2);
        continueCountTXT.DOFade(1, 2);
        yield return new WaitForSeconds(2.2f);
        continueCountTXT.text = "2";
        yield return new WaitForSeconds(1);
        continueCountTXT.text = "1";
        yield return new WaitForSeconds(1);

        // int a = 5;
        // for (int i = a; i <= 0; i--)
        // {
        //     continueCountTXT.text = i.ToString();
        //     yield return new WaitForSeconds(1);
        // }

        //yield return new WaitForSeconds(a);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        StopCoroutine("ContinueGameTXT");
    }

    IEnumerator bestscoreText()
    {
        bestsocteTXT.transform.DOScale(1.5f, 0.6f);
        yield return new WaitForSeconds(0.6f);
        bestsocteTXT.transform.DOScale(1, 0.6f);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(bestscoreText());

    }

    IEnumerator GameCount()
    {
        countTXT.gameObject.SetActive(true);
        countTXT.text = "3";
        yield return new WaitForSeconds(1);
        countTXT.text = "2";
        yield return new WaitForSeconds(1);
        countTXT.text = "1";
        yield return new WaitForSeconds(0.5f);
        countTXT.gameObject.SetActive(false);
        //Test.SetRandomTrajectory();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // IEnumerator restartGame()
    // {
    //     backgorund.transform.DOMove(new Vector3(430,500,0),0.5f);
    //     restartButton.transform.DOScale(0.6f,0.5f);
    //     scoreTXT.DOFade(0,0.5f);
    //     gameoverTXT.DOFade(0,0.5f);
    //     bestsocteTXT.DOFade(1,0);
    //     yield return new WaitForSeconds(0.5f);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }


    public void QuitGame()
    {
        jSON.SavePlayerDataToJson();
        Application.Quit();
    }

    public void ContinueGameButton()
    {
        Debug.Log("렛츠고");
        // ad.LoadRewardAd();
        // ad.rewardAd.OnUserEarnedReward += (sender, e) =>
        // {
        //     StartCoroutine("ContinueGame");
        // };
        // this.rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        // AdRequest request = new AdRequest.Builder().Build();
        // this.rewardedAd.LoadAd(request);
        rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        rewardedAd.Show();
        //Time.timeScale = 0;
    }

    IEnumerator ContinueGame()
    {
        GameOver = false;
        isReStarted = true;
        backgroundRext.DOAnchorPosY(946, 1);
        bestsocteTXT.DOFade(0, 1.5f);
        scoreTXT.DOFade(0, 1.5f);
        gameoverTXT.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.7f);
        bl.SpawnBall();
        GameCount();
    }

}
