using UnityEngine;

public class PlayerFactory : ScriptableObject
{
    public GameObject PlayerPrefab;
    private static PlayerFactory _instance;

    public static PlayerFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                return CreateInstance<PlayerFactory>();
            }

            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    public Player CreatePlayer()
    {
        Player player;
        if (!PlayerPrefab)
        {
            player = new GameObject("player", typeof (Player)).GetComponent<Player>();
            player.gameObject.AddComponent<BoxCollider>();
            player.gameObject.layer = LayerMask.NameToLayer(StringConstants.PlayerLayerName);
        }
        else
        {
            player = ((GameObject)Instantiate(PlayerPrefab)).GetComponent<Player>();
        }
        
        return player;
    }
}