using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    /*
    generate map
    then generate mine when first click
    
     */
    public static MapManager instance;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int mineCount;
    [SerializeField] private GameObject blockPrefab;
    

    private BlockManager[,] blockManagers;
    public bool isGenerateMine;

    private int _flagRemain;
    public int flagRemain
    {
        get => _flagRemain;
        set
        {
            if (_flagRemain == value)
                return;
            _flagRemain = value;
            UIManager.instance?.SetFlagRemainText(_flagRemain);
        }
    }
    private void Start()
    {
        instance = this;
        mapWidth = PlayerPrefs.GetInt("mapWidth", 10);
        mapHeight = PlayerPrefs.GetInt("mapHeight", 10);
        mineCount = PlayerPrefs.GetInt("mapMine", 15);
        GenerateMap();
        if (mineCount >= mapWidth * mapHeight)
        {
            mineCount = mapWidth * mapHeight;
            GenerateMine(-1, -1);
        }
        flagRemain = mineCount;

    }
    private void Update()
    {
        
    }
    public bool CheckWin()
    {
        if (GameManager.instance.isGameEnd)
            return false;
        if (flagRemain != 0)
            return false;
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // if wrong flag block
                if (blockManagers[x, y].currentBlock.name == "FlagBlock" && !blockManagers[x, y].isMine)
                {
                    return false;
                }
                // if not open block
                if (blockManagers[x, y].currentBlock.name == "BlackBlock")
                {
                    return false;
                }
                if (blockManagers[x, y].currentBlock.name == "QuestionMarkBlock")
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void GenerateMap()
    {
        blockManagers = new BlockManager[mapWidth, mapHeight];
        float offsetWidth = (float)mapWidth / 2;
        float offsetHeight = (float)mapHeight / 2;
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject block = Instantiate(blockPrefab, new Vector3(x - offsetWidth, 0.5f, y - offsetHeight), Quaternion.identity);
                block.transform.SetParent(transform);
                blockManagers[x, y] = block.GetComponent<BlockManager>();
                blockManagers[x, y].SetXY(x, y);
            }
        }
    }
    public void GenerateMine(int x, int y)
    {
        isGenerateMine = true;
        int[,] mines = Minesweeper.GenerateMines(mapWidth, mapHeight, mineCount, x, y);
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                blockManagers[i, j].SetMine(mines[i,j]);
            }
        }
    }
    public void Open(int x, int y)
    {
        //check around it then open of it can
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (x + i >= 0 && x + i < mapWidth && y + j >= 0 && y + j < mapHeight)
                {
                    blockManagers[x + i, y + j].Open("WhiteBlock");
                }
            }
        }
        
    }
    public void OpenNumber(BlockManager block)
    {
        int flagBlock = 0;
        //check surrond
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (block.x + i >= 0 && block.x + i < mapWidth && block.y + j >= 0 && block.y + j < mapHeight
                    && blockManagers[block.x + i, block.y + j].currentBlock.name == "FlagBlock")
                {
                    flagBlock++;
                }
            }
        }
        if(block.mineCount == flagBlock)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    if (block.x + i >= 0 && block.x + i < mapWidth && block.y + j >= 0 && block.y + j < mapHeight
                        && blockManagers[block.x + i, block.y + j].currentBlock.name == "BlackBlock")
                    {
                        blockManagers[block.x + i, block.y + j].LeftClick();
                    }
                }
            }
        }
    }
    public void OpenAll(float time)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                if (blockManagers[i, j].isMine)
                {
                    blockManagers[i, j].Open("BombBlock");
                }
                else
                {
                    //blockManagers[i, j].Open("WhiteBlock");
                }
            }
        }
    }
    public IEnumerator ExplodeAll(float time)
    {
        float t = time / mineCount * 0.8f;
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                

                if (blockManagers[i, j].isMine)
                {
                    blockManagers[i, j].Explode();
                    yield return new WaitForSeconds(t);
                }
                else
                {
                    //blockManagers[i, j].Open("WhiteBlock");
                }
            }
        }
    }
}
