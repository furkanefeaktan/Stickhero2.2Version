using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class LevelManager : MonoBehaviour
{
    public GameObject EnemyParent;
    public GameObject WinPanel;
    public GameObject DiePanel;
    private PlayerFight _playerFight;
    private coinReward _coinReward;

    public LevelManagerSo levelManagerSo;
    
    private bool _panelActive = false;
    public int moneyEarned = 100; 


    public void LevelCompleted()
    {
                    _coinReward.CountCoins();
            levelManagerSo.MoneyValue += moneyEarned;
         _coinReward.MoneyValueText.text = levelManagerSo.MoneyValue.ToString();
    }

    void Start()
    {
        _coinReward = FindObjectOfType<coinReward>();
        Screen.orientation = ScreenOrientation.Portrait; // Screen Vertical sync
        _panelActive = false;
       _playerFight = FindObjectOfType<PlayerFight>();

       _coinReward.MoneyValueText.text = levelManagerSo.MoneyValue.ToString();
    }

    void FixedUpdate()
    {    //Block screen rotation
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        if(EnemyParent == null)
        return;

        if(EnemyParent.transform.childCount == 0)
        {
            if(_panelActive)
            return;
            WinPanel.SetActive(true);
            LevelCompleted();
            levelManagerSo.NumberOfLevels++;
            _panelActive = true;
        }
        if(_playerFight.PlayerHp <= 0.5f)
        {
             if(_panelActive)
            return;
            
            DiePanel.SetActive(true);
            _panelActive = true;
        }
    }

     public void StartGame()
    {
        if (levelManagerSo.LevelsSceneNames.Contains("Level" + levelManagerSo.NumberOfLevels.ToString()))
        {
            SceneManager.LoadScene("Level" + levelManagerSo.NumberOfLevels.ToString());
        }
    }
    public void MainMenu()
    {
         SceneManager.LoadScene(levelManagerSo.MainMenu);
    } 
}
