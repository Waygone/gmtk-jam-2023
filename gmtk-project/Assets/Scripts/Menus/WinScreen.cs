using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI score;
    string currentLevelName;


    void Start()
    {
        currentLevelName = PlayerPrefs.GetString("CurrentLevel");
        currentLevel.text = currentLevelName.Replace("_", " ");
        score.text = PlayerPrefs.GetFloat(currentLevelName).ToString();
    }

    public void Next()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string[] levelNumber = currentScene.Split("_");
        string level = "Level_"+levelNumber[1];
        if (SceneManager.GetSceneByName(level).IsValid())
        {
            SceneManager.LoadScene(level);
        }
        else
            SceneManager.LoadScene("MainMenu");
    }
}
