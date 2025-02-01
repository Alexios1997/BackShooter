using System.Collections;
using System.Collections.Generic;
using AlexHyperCasualGames;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameControllerSc : MonoBehaviour
{
    public static GameControllerSc Instance { get; private set; }

    public List<GameObject> Levels = new List<GameObject>();
    public Camera mainCam;
    public Constants.GameState CurrentGameState;
    private int CurrentLevelToLoadIndex = 0;
    public bool DeleteAllPlayerPrefs;
    // Set Singleton 
    private void Awake()
    {
        if (DeleteAllPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        
        Levels[PlayerPrefs.GetInt("Level Index")].SetActive(true);
        
        
        Application.targetFrameRate = 30;
        #region Singleton
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
       
        SetState(Constants.GameState.StartUIAnim);

    }

    public void SetState(Constants.GameState ChangedState)
    {
        CurrentGameState = ChangedState;
    }

    public void ShoppingStateOn()
    {
        SetState(Constants.GameState.Shopping);
    }
    public void ShoppingStateOff()
    {
        SetState(Constants.GameState.StartUIAnim);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("BackShooterMainScene");
    }
    public void NextLevelButton()
    {
        CurrentLevelToLoadIndex = PlayerPrefs.GetInt("Level Index");
        CurrentLevelToLoadIndex++;
        PlayerPrefs.SetInt("Level Index", CurrentLevelToLoadIndex);
        Debug.Log(PlayerPrefs.GetInt("Level Index"));
        SceneManager.LoadScene("BackShooterMainScene");
    }
}
