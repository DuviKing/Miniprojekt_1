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

    [Header("Unit Sounds")]
    [SerializeField] private AudioClip[] attackSounds;
    public static AudioClip sharedPlacementSound;
    public static AudioClip[] sharedDeathSounds;

    void Start() { }
    void Update() { }

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
        // Play shared placement sound
        if (sharedPlacementSound != null)
            LydEffektManger.instance.spilLyd(sharedPlacementSound, transform, 1f);
    }

    public void PlayAttackSound()
    {
        if (attackSounds != null && attackSounds.Length > 0)
            LydEffektManger.instance.spilrandomLyd(attackSounds, transform, 1f);
    }

    public void PlayDeathSound()
    {
        if (sharedDeathSounds != null && sharedDeathSounds.Length > 0)
            LydEffektManger.instance.spilrandomLyd(sharedDeathSounds, transform, 1f);
    }

    public void TakeDamage()
    {
        //health -= amount;
        if (health <= 0)
        {
            PlayDeathSound();
           // Destroy(gameObject);
        }
    }
}
