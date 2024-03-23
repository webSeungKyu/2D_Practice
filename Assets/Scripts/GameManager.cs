using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSprite;
    public Sprite gameClearSprite;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image titleImage;

    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;


    public Text scoreText;
    public static int totalScore;
    public int stageScore = 0;

    public AudioClip audioClipGameOver;
    public AudioClip audioClipGameClear;

    public GameObject inputUI; // 터치스크린용 조작 UI 패널
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameClear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button button = restartButton.GetComponent<Button>();
            button.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSprite;
            PlayerController.gameState = "gameEnd";

            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }

            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();

            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                Debug.Log("사운드 멈춘 후 재생");
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(audioClipGameClear);
            }
            else
            {
                Debug.Log("soundPlayer null");
            }
            inputUI.SetActive(false); // 조작 UI 숨기기
        }
        else if (PlayerController.gameState == "gameOver")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button button = nextButton.GetComponent<Button>();
            button.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            PlayerController.gameState = "gameEnd";

            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }

            AudioSource soundPlayer = GetComponent<AudioSource>();
            if(soundPlayer != null)
            {
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(audioClipGameOver); //Play()와는 다르게 소리를 중첩해서 재생할 수 있다.
            }
            else
            {
                Debug.Log("soundPlayer null");
            }
            inputUI.SetActive(false); // 조작 UI 숨기기
        }
        else if (PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();

            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    int nowTime = (int)timeCnt.displayTime; // 소수점 표시 제거
                    timeText.GetComponent<Text>().text = nowTime.ToString();
                    if(nowTime == 0)
                    {
                        playerController.GameOver();
                    }
                }
            }
            if(playerController.score != 0)
            {
                stageScore += playerController.score;
                playerController.score = 0;
                UpdateScore();
            }
        }
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.text = score.ToString();
    }

    public void Jump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent< PlayerController>();
        playerController.Jump();
    }
}
