using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapDifficultController : MonoBehaviour
{
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private string valueName;
    [SerializeField] public enum Difficult { Easy, Medium, Hard };
    [SerializeField] private Difficult difficult;
    
    private string difficultName;

    private void Start()
    {    
        valueText.text = difficult.ToString();
    }
}


