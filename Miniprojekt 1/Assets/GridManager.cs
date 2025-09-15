using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSizeRadius;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        for (int x = -(gridSizeRadius - 1); x <= (gridSizeRadius - 1); x += 1)
        {
            for (int y = -gridSizeRadius; y <= gridSizeRadius; y += 1) //Loop'er gennem alle heltalskoordinater
            {
                //Definerer tile afstande
                float xTilePos = y % 2 == 0 ? x : x + 0.5f;
                float yTilePos = y % 2 == 0 ? y - 0.25f * y : y - 0.25f * y;
                float distCentPos = MathF.Sqrt(xTilePos * xTilePos + yTilePos * yTilePos);
                float distCent = MathF.Sqrt(x * x + y * y);
                float distPos = MathF.Abs(xTilePos) + MathF.Abs(yTilePos);

                //Tjek om tile positionen er inde i hexagonen med radius på gridSizeRadius
                if (distCent < gridSizeRadius &&
                distCentPos < gridSizeRadius &&
                (MathF.Abs(xTilePos) <= (float)gridSizeRadius / 2 || distPos <= gridSizeRadius))
                {
                    //Spawner et tile på alle heltals x- og y-koordinater
                    var spawnedTile = Instantiate(tilePrefab, GetGridPosition(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    //Farver hver andet tile
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);
                }
            }
        }
    }

    public Vector3 GetGridPosition(int x, int y) //Beregner hexagon positionen ud fra et firkantet koordinatsystem
    {
        float xPos = y % 2 == 0 ? x : x + 0.5f;
        float yPos = y % 2 == 0 ? y - 0.25f * y : y - 0.25f * y;
        return new Vector3(xPos, yPos, 0);
    }
}
