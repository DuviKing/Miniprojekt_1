using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public int health;
    public int damage;
    public int moveRange = 1; // how far this unit can move
    public Tile currentTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceOnTile(Tile tile)
    {
        if (currentTile != null)
        {
            currentTile.ClearUnit();
        }
        currentTile = tile;
        Debug.Log($"Unit is on {tile.name}");
        transform.position = tile.transform.position; // snap to tile
    }
}
