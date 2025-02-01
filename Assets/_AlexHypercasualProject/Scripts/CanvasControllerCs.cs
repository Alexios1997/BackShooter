using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using AlexHyperCasualGames;
using TMPro;


public class CanvasControllerCs : MonoBehaviour
{
    public GameObject StartMenuPanel;
    public GameObject InGameUiPanel;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject CurrentIndicator;
    public GameObject TapToStart;
    public GameObject ShopButton;
    public GameObject SliderGo;
    public GameObject TextLevelBar;
    public List<GameObject> LevelIndicators = new List<GameObject>();
    public GameObject TextLevelFail;
    public GameObject TextLevelSuccess;
    public GameObject TextCoinNum;
    public int CoinNumber;
    private float CurrentBarValue = 0;


    public static CanvasControllerCs Instance { get; private set; }

    private void Awake()
    {
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
    }


    public void UnlockWeapon(int cost)
    {

        
    }


    private void OnEnable()
    {
        PlayerControllerSc.onGamePlay += ChangePanelToPlay;
    }
    private void OnDisable()
    {
        PlayerControllerSc.onGamePlay -= ChangePanelToPlay;
    }

    public void Start()
    {
        int CurrentLevelIndex = PlayerPrefs.GetInt("Level Index");
        TextLevelBar.GetComponent<TextMeshProUGUI>().text = "Level " + (CurrentLevelIndex + 1).ToString();
        TextLevelFail.GetComponent<TextMeshProUGUI>().text = "Level " + (CurrentLevelIndex + 1).ToString() + " Failed";
        TextLevelSuccess.GetComponent<TextMeshProUGUI>().text = "Level " + (CurrentLevelIndex + 1).ToString() + " Finished";
        //Debug.Log(PlayerPrefs.GetInt("Level Index"));
        CoinNumber = PlayerPrefs.GetInt("CoinNumber");
        TextCoinNum.GetComponent<TextMeshProUGUI>().text = CoinNumber.ToString();





        switch (CurrentLevelIndex%5)
        {
            case 0:
                
                 CurrentIndicator.GetComponent<RectTransform>().localPosition = LevelIndicators[0].GetComponent<RectTransform>().localPosition;
                break;
            case 1:
               
                LevelIndicators[0].GetComponent<Image>().enabled = !LevelIndicators[0].GetComponent<Image>().enabled;
                CurrentIndicator.GetComponent<RectTransform>().localPosition = LevelIndicators[1].GetComponent<RectTransform>().localPosition;
                //LevelIndicators[0]
                break;
            case 2:
                
                LevelIndicators[0].GetComponent<Image>().enabled = !LevelIndicators[0].GetComponent<Image>().enabled;
                LevelIndicators[1].GetComponent<Image>().enabled = !LevelIndicators[1].GetComponent<Image>().enabled;
                CurrentIndicator.GetComponent<RectTransform>().localPosition = LevelIndicators[2].GetComponent<RectTransform>().localPosition;
                //LevelIndicators[0]
                break;
            case 3:
                
                LevelIndicators[0].GetComponent<Image>().enabled = !LevelIndicators[0].GetComponent<Image>().enabled;
                LevelIndicators[1].GetComponent<Image>().enabled = !LevelIndicators[1].GetComponent<Image>().enabled;
                LevelIndicators[2].GetComponent<Image>().enabled = !LevelIndicators[2].GetComponent<Image>().enabled;
                CurrentIndicator.GetComponent<RectTransform>().localPosition = LevelIndicators[3].GetComponent<RectTransform>().localPosition;
                //LevelIndicators[0]
                break;
            case 4:
                
                LevelIndicators[0].GetComponent<Image>().enabled = !LevelIndicators[0].GetComponent<Image>().enabled;
                LevelIndicators[1].GetComponent<Image>().enabled = !LevelIndicators[1].GetComponent<Image>().enabled;
                LevelIndicators[2].GetComponent<Image>().enabled = !LevelIndicators[2].GetComponent<Image>().enabled;
                LevelIndicators[3].GetComponent<Image>().enabled = !LevelIndicators[3].GetComponent<Image>().enabled;
                CurrentIndicator.GetComponent<RectTransform>().localPosition = LevelIndicators[4].GetComponent<RectTransform>().localPosition;
                //LevelIndicators[0]
                break;

        }
       






        CurrentIndicator.GetComponent<RectTransform>().DOScale(0.5f, 0.6f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        TapToStart.GetComponent<RectTransform>().DOScale(1.1f, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        ShopButton.GetComponent<RectTransform>().DOScale(0.65f, 2.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void ChangePanelToPlay()
    {
        StartMenuPanel.GetComponent<RectTransform>().DOLocalMoveY(-2800f, 0.2f);
        GameControllerSc.Instance.SetState(Constants.GameState.Playing);
        
        StartCoroutine(StartGame());
        
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.2f);
        StartMenuPanel.SetActive(false);
        InGameUiPanel.SetActive(true);
        InGameUiPanel.GetComponent<RectTransform>().DOLocalMoveY(0, 0.1f);
    }

    public void SetRaceStats(float minV,float MaxV)
    {

        SliderGo.GetComponent<Slider>().minValue = minV;
        SliderGo.GetComponent<Slider>().maxValue = MaxV;
    }
    public void SetRaceBar(float TakenCurrentPosition)
    {
        SliderGo.GetComponent<Slider>().value = TakenCurrentPosition;
    }
    public void GameOverCanvas()
    {
        InGameUiPanel.SetActive(false);
        GameOverPanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.1f);
    }
    public void WinPanelCanvas()
    {
        InGameUiPanel.SetActive(false);
        WinPanel.GetComponent<RectTransform>().DOLocalMoveX(0, 0.1f);
        PlayerPrefs.SetInt("CoinNumber", CoinNumber);
        
    }
    


}
