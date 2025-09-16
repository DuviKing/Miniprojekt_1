using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private float hexYOffsetMultiplier = 0.25f;
    [SerializeField] private int gridSizeRadius;
    [SerializeField] private Tile tilePrefab;
    private Tile selectedTile;
    private Dictionary<Vector2, Tile> mapTiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left mouse click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null)
                {
                    if (clickedTile.unitOnTile == true)
                    {
                        HighlightTilesInRange(clickedTile.transform.position, 1);
                    }
                    else
                    {
                        SelectTile(clickedTile);
                    }
                }
            }
        }
    }

    private void SelectTile(Tile tile)
    {
        // Deselect forrige tile
            if (selectedTile != null)
            {
                selectedTile.Deselect();
            }
        foreach (var kvp in mapTiles)
        {
            Tile currentTile = kvp.Value;
            currentTile.Deselect();
        }

        // Select ny tile
        selectedTile = tile;
        selectedTile.Select();

        Debug.Log($"{tile.name} is now selected");
    }

    void GenerateGrid()
    {
        mapTiles = new Dictionary<Vector2, Tile>();
        for (int x = -(gridSizeRadius - 1); x <= (gridSizeRadius - 1); x += 1)
        {
            for (int y = -gridSizeRadius; y <= gridSizeRadius; y += 1) //Loop'er gennem alle heltalskoordinater
            {
                //Definerer tile afstande
                float xTilePos = y % 2 == 0 ? x : x + 0.5f;
                float yTilePos = y - hexYOffsetMultiplier * y;
                float distCentPos = MathF.Sqrt(xTilePos * xTilePos + yTilePos * yTilePos);
                float distCent = MathF.Sqrt(x * x + y * y);
                float distPos = MathF.Abs(xTilePos) + MathF.Abs(yTilePos);

                //Tjek om tile positionen er inde i hexagonen med radius på gridSizeRadius
                if (distCent < gridSizeRadius &&
                distCentPos < gridSizeRadius &&
                (MathF.Abs(xTilePos) <= (float)gridSizeRadius / 2 || distPos <= gridSizeRadius))
                {
                    //Spawner et tile på alle heltals x- og y-koordinater
                    var spawnedTile = Instantiate(tilePrefab, new Vector3(xTilePos, yTilePos, 0), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    //Farver hver andet tile
                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);

                    //Tilføjer tile position til dictionary
                    mapTiles[new Vector2(xTilePos, yTilePos)] = spawnedTile;
                }
            }
        }
    }

    public void HighlightTilesInRange(Vector2 center, int range)
    {
        foreach (var kvp in mapTiles)
        {
            Vector2 tilePos = kvp.Key;
            Tile tile = kvp.Value;

            // Hex distance formula (approx for your offset coords)
            float distance = Vector2.Distance(center, tilePos);

            if (distance <= range)
            {
                Debug.Log($"{tile.name}: {tilePos}");
                tile.Select();
            }
            else
            {
                tile.Deselect();
            }
        }
    }
}
