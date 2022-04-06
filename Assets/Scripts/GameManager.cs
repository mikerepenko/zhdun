using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded() {  }
    public void onRewardedVideoFailedToLoad() {  }
    public void onRewardedVideoShown() {  }
    public void onRewardedVideoClosed() {  }
    public void onRewardedVideoFinished(int amount, string name)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);
        MaxMoney.text = PlayerPrefs.GetInt("Money").ToString();
    }
    #endregion
    //public int money;
    public Text MaxMoney;
    public Text record;
    public Text thisScore;
    public int scoreCount = 0;
    public GameObject newRecord;
    public bool isManager;
    private int gM;
    public int count;
    private int countGameover;
    public AudioSource buyAds;
    public GameObject goButtom;
    public Text award;
    public GameObject firstMoney;
    private int rund;
    private bool isRecord;



    void Awake()
    {

        //Обратный вызов
        //Appodeal.confirm(Appodeal.SKIPPABLE_VIDEO);
        //Appodeal.setTesting(false);
    }

    void Start()
    {
        isRecord = true;
        //Appodeal.hide(Appodeal.BANNER_BOTTOM);

        countGameover = PlayerPrefs.GetInt("CGO");

        //Получение первого подарка
        if (PlayerPrefs.GetInt("FirstGame") != 1)
        {
            firstMoney.SetActive(true);
            AddMoney(500);
            PlayerPrefs.SetInt("FirstGame", 1);
            buyAds.Play();
        }

        if (PlayerPrefs.GetInt("FirstStart") == 1)
        {
            isManager = false;
        }
        else
            isManager = true;
        if (gameObject.name == "GameManager")
            gM = 1;

        MaxMoney.text = PlayerPrefs.GetInt("Money").ToString();
        if(gameObject.name == "GameManager")
            record.text = record.text + PlayerPrefs.GetInt("Score").ToString();
        else
            record.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void AddMoney(int count)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + count);
        MaxMoney.text = PlayerPrefs.GetInt("Money").ToString();
    }

	void Update () {
        if (gM == 1)
        {
            if (isManager)
            {
                scoreCount += 2;
                thisScore.text = scoreCount.ToString();
                //PlayerPrefs.SetInt("ScoreThis", scoreCount);
            }
        }
        else
            MaxMoney.text = PlayerPrefs.GetInt("Money").ToString();
        //Запись рекорда с учетом условия
        if ((PlayerPrefs.GetInt("Score")) < scoreCount && isRecord)
        {
            newRecord.SetActive(true);
            isRecord = false;
        }
    }
    public void StartScore()
    {
        isManager = true;
    }
    public void StopScore()
    {
        //StartBanner();
        countGameover++;
        if (PlayerPrefs.GetInt("CGO") <= 100)
            PlayerPrefs.SetInt("CGO", countGameover);
        else
            PlayerPrefs.SetInt("CGO", 0);


        isManager = false;
        PlayerPrefs.SetInt("ScoreThis", scoreCount);

        if ((PlayerPrefs.GetInt("Score")) < scoreCount)
        {
            PlayerPrefs.SetInt("Score", scoreCount);

            if(scoreCount >= 40000)
            {
                PlayerPrefs.SetInt("BuySave16", 1);
                PlayerPrefs.SetInt("buyCar", 6);
            }
            record.text = "Рекорд: " + (PlayerPrefs.GetInt("Score")).ToString();

            if (scoreCount / 1000 >= 1 && PlayerPrefs.GetInt("Award") < 1000 && scoreCount / 1000 <= 4)
            {
                goButtom.SetActive(true);
                award.text = "+" + (scoreCount / 1000 * 500);
                AddMoney(scoreCount / 1000 * 500);
                buyAds.Play();
                PlayerPrefs.SetInt("Award", 1000);
            }
                //Выдача денег за рекорд
                if (PlayerPrefs.GetInt("Award") / 5000 < scoreCount / 5000)
            {
                goButtom.SetActive(true);
                award.text = "+" + (scoreCount / 5000 * 500);
                AddMoney(scoreCount / 5000 * 500);
                buyAds.Play();
                PlayerPrefs.SetInt("Award", PlayerPrefs.GetInt("Award") + 5000);
            }
        }
    }
}
