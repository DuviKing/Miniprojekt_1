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
    [SerializeField] private UnitScript unitPrefab;
    [SerializeField] private UnitScript squirePrefab;
    [SerializeField] private UnitScript knightPrefab;
    [SerializeField] private UnitScript shieldKnightPrefab;
    [SerializeField] private UnitScript cavalryPrefab;
    private Tile selectedTile;
    private UnitScript selectedUnit;
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
                    HandleMoveTileClick(clickedTile);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) // right mouse click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null)
                {
                    HandleAttackTileClick(clickedTile);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) // 'A'-knap spawner en unit på feltet med musen over
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null)
                {
                    Tile clickedTile = hit.collider.GetComponent<Tile>();
                    if (clickedTile != null)
                    {
                        var spawnedUnit = Instantiate(unitPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        clickedTile.PlaceUnit(spawnedUnit);
                    }
                }
            }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnitSpawn(squirePrefab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnitSpawn(knightPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnitSpawn(shieldKnightPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnitSpawn(cavalryPrefab);
        }
    }

    private void UnitSpawn(UnitScript unitType)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Tile clickedTile = hit.collider.GetComponent<Tile>();
            if (clickedTile != null)
            {
                var spawnedUnit = Instantiate(unitType, new Vector3(0, 0, 0), Quaternion.identity);
                clickedTile.PlaceUnit(spawnedUnit);
            }
        }
    }

    private void HandleMoveTileClick(Tile tile)
    {
        // Case 1: Selecting a unit
        if (tile.OccupiedUnit != null && selectedUnit == null)
        {
            selectedUnit = tile.OccupiedUnit;
            HighlightTilesInRange(GetTileGridPosition(selectedUnit.currentTile), selectedUnit.moveRange);
            return;
        }

        // Case 2: Selecting a destination within range
        if (selectedUnit != null && tile.activeHighlight && tile.OccupiedUnit == null)
        {
            MoveUnitToTile(selectedUnit, tile);
            ClearHighlights();
            selectedUnit = null;
        }

        //Case 3: Selecting a destination outside range
        if (selectedUnit != null && tile.activeHighlight == false && tile.OccupiedUnit == null)
        {
            ClearHighlights();
            selectedUnit = null;
        }
    }

    private void HandleAttackTileClick(Tile tile)
    {
        if (selectedUnit != null && tile.activeHighlight && tile.OccupiedUnit != null)
        {
            tile.InitiateCombat(selectedUnit);
        }
    }

    private void MoveUnitToTile(UnitScript unit, Tile tile)
    {
        tile.PlaceUnit(unit);
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

    private Vector2 GetTileGridPosition(Tile tile)
    {
        foreach (var kvp in mapTiles)
        {
            if (kvp.Value == tile)
                return kvp.Key;
        }
        return Vector2.zero;
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
                tile.Select();
            }
            else
            {
                tile.Deselect();
            }
        }
    }
    public void ClearHighlights()
    {
        foreach (var kvp in mapTiles)
        {
            kvp.Value.Deselect();
        }
    }
}
