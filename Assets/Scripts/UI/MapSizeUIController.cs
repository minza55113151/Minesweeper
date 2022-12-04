using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapSizeUIController : MonoBehaviour
{
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private string valueName;

    private int val;
    
    [SerializeField] private int minValue;
    [SerializeField] private int defaultValue;
    [SerializeField] private int maxValue;

    private void Start()
    {
        val = PlayerPrefs.GetInt(valueName, defaultValue); ;
        valueText.text = val.ToString();
        decreaseButton.onClick.AddListener(Decrease);
        increaseButton.onClick.AddListener(Increase);
    }

    public void Increase()
    {
        if (val < maxValue)
        {
            val++;
            valueText.text = val.ToString();
            PlayerPrefs.SetInt(valueName, val);
        }
    }

    public void Decrease()
    {
        if (val > minValue)
        {
            val--;
            valueText.text = val.ToString();
            PlayerPrefs.SetInt(valueName, val);
        }
    }
}


