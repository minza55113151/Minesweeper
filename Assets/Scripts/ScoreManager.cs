using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    int width;
    int height;
    int mine;
    int time;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        width = PlayerPrefs.GetInt("mapWidth", 10);
        height = PlayerPrefs.GetInt("mapHeight", 10);
        mine = PlayerPrefs.GetInt("mapMine", 15);
        time = PlayerPrefs.GetInt($"{width} {height} {mine}", 1000000000);
        UIManager.instance.SetHighScoreText(time);
    }
    public void SetHighScore()
    {
        if (UIManager.instance.time < time)
        {
            PlayerPrefs.SetInt($"{width} {height} {mine}", (int)UIManager.instance.time);
        }    
    }
}
