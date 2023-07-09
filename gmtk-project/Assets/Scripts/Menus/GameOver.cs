using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
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

    public void Retry()
    {
        SceneManager.LoadScene(currentLevelName);
    }
}
