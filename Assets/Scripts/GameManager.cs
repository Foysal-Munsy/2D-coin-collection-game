using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState
{
    Menu,
    InGame,
    GameOver
}
public class GameManager : MonoBehaviour
{
    // Starts ours game
    public GameState currentGameState = GameState.Menu;
    private static GameManager sharedInstance;
    public Canvas mainMenu;
    public Canvas gameMenu;
    public Canvas gameOverMenu;
    int collectedCoins = 0;

    private void Awake()
    {
        sharedInstance = this;
    }
    public static GameManager GetInstance()
    {
        return sharedInstance;
    }
    public void StartGame()
    {
        print("start game");
        LevelGenerator.sharedInstance.createInitialBlocks();
        PlayerController.GetInstance().StartGame();
        ChangeGameState(GameState.InGame);
        ViewInGame.GetInstance().ShowHighestScore();

    }
    private void Start()
    {
        // StartGame();
        currentGameState = GameState.Menu;
        mainMenu.enabled = true;
        gameMenu.enabled = false;
        gameOverMenu.enabled = false;
    }

    private void Update()
    {
        // currentGameState != GameState.InGame && --> after die it can return to start
        if (currentGameState != GameState.InGame && Input.GetButtonDown("s"))
        {
            ChangeGameState(GameState.InGame);
            StartGame();
        }
    }
    // private void Update()
    // {
    //     // Allow returning to the start state after dying
    //     if (currentGameState != GameState.InGame && Input.GetKeyDown(KeyCode.S))
    //     {
    //         ChangeGameState(GameState.InGame);
    //         StartGame();
    //     }
    // }


    // when player dies
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllBlocks();

        ChangeGameState(GameState.GameOver);
        GameOverView.GetInstance().UpdateGui();
    }
    //called when the player decides to quick the game
    // and back to the main menu
    public void BackToMainMenu()
    {
        ChangeGameState(GameState.Menu);
    }
    void ChangeGameState(GameState newGameState)
    {

        switch (newGameState)
        {
            case GameState.Menu:
                mainMenu.enabled = true;
                gameMenu.enabled = false;
                gameOverMenu.enabled = false;
                break;
            case GameState.InGame:
                mainMenu.enabled = false;
                gameMenu.enabled = true;
                gameOverMenu.enabled = false;
                break;
            case GameState.GameOver:
                mainMenu.enabled = false;
                gameMenu.enabled = false;
                gameOverMenu.enabled = true;
                break;
            default:
                currentGameState = GameState.Menu;
                break;
        }
        currentGameState = newGameState;
    }
    public void CollectCoins()
    {
        collectedCoins++;
        ViewInGame.GetInstance().UpdateCoins();
    }
    public int GetCollectedCoins()
    {
        return collectedCoins;
    }
}
