using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public LevelManagerSo levelManagerSo;

    void Start()
    {
        LevelText.text = "Level" + " " + levelManagerSo.NumberOfLevels.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
