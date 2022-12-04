using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int mapWidth;
    private int mapHeight;
    private Camera mainCamera;

    private void Start()
    {
        mapWidth = PlayerPrefs.GetInt("mapWidth", 10);
        mapHeight = PlayerPrefs.GetInt("mapHeight", 10);
        mainCamera = Camera.main;
        int y = (mapWidth > mapHeight ? mapWidth : mapHeight) + 4;
        mainCamera.transform.position = new Vector3(0f, y, -0.5f);
    }
}
