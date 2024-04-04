using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "LevelManagerSO", menuName = "GameDatas/ScriptableObject")]
public class LevelManagerSo : ScriptableObject
{
      public List<string> LevelsSceneNames = new List<string>();
      public string MainMenu;
      public int NumberOfLevels;
      public int MoneyValue = 0;
      
    public void PlayerPrefsClear()
    {
        PlayerPrefs.DeleteAll();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(LevelManagerSo))]
    public class ButonEklemeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelManagerSo script = (LevelManagerSo)target;
            if (GUILayout.Button("Data Clear"))
            {
                script.PlayerPrefsClear();
            }
        }
    }
#endif


}
