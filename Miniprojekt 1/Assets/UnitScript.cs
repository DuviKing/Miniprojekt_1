using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public int health;
    public int damage;
    public int moveRange = 1; // how far this unit can move
    public int attackRange = 1; // how far this unit can attack
    public Tile currentTile;
    public int actionPoints;
    public int actionPointsMax;
    public bool unitTeam1; // true for team 1, false for team 2
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
            actionPoints = actionPoints - 1; // bruger en action point ved at flytte
        }
        currentTile = tile;
        Debug.Log($"Unit is on {tile.name}");
        transform.position = tile.transform.position; // snap to tile
    }
}
