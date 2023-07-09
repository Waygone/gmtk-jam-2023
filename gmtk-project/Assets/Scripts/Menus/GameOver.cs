using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI score;
    public TextMeshProUGUI itemsCleaned;
    string currentLevelName;


    void Start()
    {
        currentLevelName = PlayerPrefs.GetString("CurrentLevel");
        currentLevel.text = currentLevelName.Replace("_", " ");
        score.text = "Score: " + PlayerPrefs.GetFloat(currentLevelName).ToString();
        itemsCleaned.text = "Items Cleaned: " + PlayerPrefs.GetFloat(currentLevelName + "i").ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(currentLevelName);
    }
}
