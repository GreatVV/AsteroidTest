using UnityEngine;

public class PlayerFactory : ScriptableObject
{
    public GameObject PlayerPrefab;
    public static PlayerFactory Instance { get; set; }

    public Player CreatePlayer()
    {
        Player player;
        if (!PlayerPrefab)
        {
            player = new GameObject("player", typeof (Player)).GetComponent<Player>();
            player.gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            player = ((GameObject)Instantiate(PlayerPrefab)).GetComponent<Player>();
        }
        
        return player;
    }
}