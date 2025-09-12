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
                    //Spawner et tile på alle heltals x- og y-koordinater
                var spawnedTile = Instantiate(tilePrefab, GetGridPosition(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    //Farver hver andet tile
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);
            }
        }
    }

    public Vector3 GetGridPosition(int x, int y) //Beregner hexagon positionen ud fra et firkantet koordinatsystem
    {
        return new Vector3(y % 2 == 0 ? x : x + 0.5f, y % 2 == 0 ? y - 0.25f * y : y - 0.25f * y, 0);
    }
}
