using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI score;
    public TextMeshProUGUI itemsCleaned;
    string currentLevelName;


    void Start()
    {
        currentLevelName = PlayerPrefs.GetString("CurrentLevel");
        currentLevel.text = currentLevelName.Replace("_", " ");
        score.text = PlayerPrefs.GetFloat(currentLevelName).ToString();
        itemsCleaned.text = "Items Cleaned: " + PlayerPrefs.GetFloat(currentLevelName + "i").ToString();
    }

    public void Next()
    {
        string[] levelNumber = currentLevelName.Split("_");
        int next = int.Parse(levelNumber[1]) + 1;
        string level = "Level_"+ next.ToString();
        
        SceneManager.LoadScene(level);
        //SceneManager.LoadScene("MainMenu");
    }
}
