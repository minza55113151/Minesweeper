using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
    
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI flagRemainText;

    public float time;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private TextMeshProUGUI highScoreText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
    }
        
    private void Update()
    {
        if (!GameManager.instance.isGameEnd)
        {
            time += Time.deltaTime;
            timeText.text = ((int)time).ToString();
        }
    }

    //win
    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
    //restart
    public void Restart()
    {
        GameManager.instance.Restart();
    }
    //mapsize
    //decrease increase x y button
    public void SetFlagRemainText(int value)
    {
        flagRemainText.text = value.ToString();
    }
    public void SetHighScoreText(int score)
    {
        if(score == 1000000000)
        {
            highScoreText.text = "Inf";
        }
        else
        {
            highScoreText.text = score.ToString();
        }
    }



}
