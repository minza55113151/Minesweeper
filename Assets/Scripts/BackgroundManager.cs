using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;

    public float h;

    private void Start()
    {
        h = PlayerPrefs.GetFloat("backgroundH", 0f);
    }

    private void Update()
    {
        h += Time.deltaTime;
        h = (h % 360);
        Color color = Color.HSVToRGB(h/360, 0.5f, 0.5f);
        backgroundMaterial.color = color;
        PlayerPrefs.SetFloat("backgroundH", h);
    }
}
