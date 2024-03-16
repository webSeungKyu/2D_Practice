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

    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
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


        }
        else if (PlayerController.gameState == "gameOver")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button button = nextButton.GetComponent<Button>();
            button.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            PlayerController.gameState = "gameEnd";
        }else if (PlayerController.gameState == "playing")
        {
            
        }
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
