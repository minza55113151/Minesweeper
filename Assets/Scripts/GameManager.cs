using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private float explodeTime;

    public bool isGameEnd = false;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameWin()
    {
        isGameEnd = true;
        ScoreManager.instance.SetHighScore();
        UIManager.instance.OpenWinPanel();
    }
    
    public void GameOver()
    {
        isGameEnd = true;
        //Invoke("Restart", timeBeforeRestart);
        MapManager.instance.OpenAll(explodeTime);
        StartCoroutine(MapManager.instance.ExplodeAll(explodeTime));
    }
}
    