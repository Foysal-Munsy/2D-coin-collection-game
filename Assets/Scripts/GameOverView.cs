// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class GameOverView : MonoBehaviour
// {
//     public TMP_Text coinsLabel;
//     public TMP_Text scoreText;
//     private static PlayerController sharedInstance;

//     public static GameOverView GetInstance()
//     {
//         return sharedInstance;
//     }
//     private void Awake()
//     {
//         sharedInstance = this;
//     }

//     public void UpdateGui()
//     {
//         if (GameManager.GetInstance().currentGameState == GameState.GameOver)
//         {
//             coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
//             scoreText.text = PlayerController.GetInstance().GetDistance().ToString();
//             highestScoreText.text = PlayerController.GetInstance().GetMaxScore().ToString();
//         }

//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverView : MonoBehaviour
{
    public TMP_Text coinsLabel; // Text for displaying collected coins
    public TMP_Text scoreText; // Text for displaying the current score
    public TMP_Text highestScoreText; // Text for displaying the highest score (added declaration)

    private static GameOverView sharedInstance; // Singleton for GameOverView
    private PlayerController playerController; // Reference to the PlayerController script

    public static GameOverView GetInstance()
    {
        return sharedInstance;
    }

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        // Find the PlayerController in the scene and assign it
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene. Please ensure it's added.");
        }
    }

    public void UpdateGui()
    {
        // Ensure GameManager and PlayerController references are valid
        if (GameManager.GetInstance() != null && playerController != null)
        {
            if (GameManager.GetInstance().currentGameState == GameState.GameOver)
            {
                coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
                scoreText.text = playerController.GetDistance().ToString();
                highestScoreText.text = playerController.GetMaxScore().ToString();
            }
        }
        else
        {
            Debug.LogError("GameManager or PlayerController instance is missing!");
        }
    }
}
