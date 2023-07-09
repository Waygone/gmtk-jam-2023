using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI itemsCleaned;
    string currentLevelName;


    void Start()
    {
        score.text = "Total Score: " + (PlayerPrefs.GetFloat("Level_1")+ PlayerPrefs.GetFloat("Level_2")).ToString();
        itemsCleaned.text = "Total Items Cleaned: " + (PlayerPrefs.GetFloat("Level_2i") + PlayerPrefs.GetFloat("Level_1i")).ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
