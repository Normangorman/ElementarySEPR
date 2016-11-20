using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    public Text Player1ScoreText;
    public Text Player2ScoreText;

    private int playerScore1 = 0;
    private int playerScore2 = 0;

    public int PlayerScore1
    {
        get
        {
            return playerScore1;
        }
        set
        {
            playerScore1++;
            Player1ScoreText.text = playerScore1.ToString();
        }
    }
    public int PlayerScore2
    {
        get
        {
            return playerScore2;
        }
        set
        {
            playerScore2++;
            Player2ScoreText.text = playerScore2.ToString();
        }
    }

    public void Awake()
    {
        Instance = this;
        Player1ScoreText = GameObject.FindGameObjectWithTag("Player1Score").GetComponent<Text>();
        Player2ScoreText = GameObject.FindGameObjectWithTag("Player2Score").GetComponent<Text>();
    }

    public void Score (string wallID) {
        if (wallID == "rightWall")
        {
            PlayerScore1++;
        }
        else
        {
            PlayerScore2++;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

}