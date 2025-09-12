using Unity.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
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
        for (int x = 0; x < _width; x += 1)
        {
            for (int y = 0; y < _height; y += 1)
            {
                var spawnedTile = Instantiate(tilePrefab, GetGridPosition(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        //Flytter kameraet ind i midten af skærmen
        mainCamera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }
    public Vector3 GetGridPosition(int x, int y) //Beregner hexagon positionen ud fra et firkantet koordinatsystem
    {
        return new Vector3(y % 2 == 0 ? x : x + 0.5f, y % 2 == 0 ? y - 0.25f * y : y - 0.25f * y, 0);
    }
}
