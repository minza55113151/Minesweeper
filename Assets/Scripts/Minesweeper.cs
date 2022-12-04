using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minesweeper
{
    public static int[,] GenerateMines(int width, int height, int mines, int firstX, int firstY)
    {
        int[,] grid = new int[width, height];
        //if mines more than grids
        if (mines >= width * height)
        {
            mines = width * height;
            firstX = -1;
            firstY = -1;
        }
        
        //fill mines
        int placedMines = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //can place and not first
                if (placedMines < mines && !(i == firstX && j == firstY))
                {
                    grid[i, j] = -1;
                    placedMines++;
                }                
            }
        }
        Shuffle(grid, firstX, firstY);

        //assign value around mines
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == -1)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && grid[x + i, y + j] != -1)
                            {
                                grid[x + i, y + j]++;
                            }
                        }
                    }
                }
            }
        }

        return grid;
    }

    private static void Shuffle(int[,] arr, int firstX, int firstY)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                int x = Random.Range(0, arr.GetLength(0));
                int y = Random.Range(0, arr.GetLength(1));
                if (!(x == firstX && y == firstY) && !(i == firstX && j == firstY))
                {
                    int temp = arr[i, j];
                    arr[i, j] = arr[x, y];
                    arr[x, y] = temp;
                }
            }
        }
    }
}
