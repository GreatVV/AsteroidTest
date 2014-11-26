using System;
using Game.Players;
using UnityEngine;
using Utils;

namespace Game.Scriptable
{
    public class PlayerFactory : ScriptableObject
    {
        public GameObject PlayerPrefab;
        private static PlayerFactory _instance;

        [NonSerialized]
        private Player _playerInstance;

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

            if (_playerInstance)
            {
                player = _playerInstance;
                player.GameObject.SetActive(true);
            }
            else
            {
                if (!PlayerPrefab)
                {
                    player = new GameObject("player", typeof (Player)).GetComponent<Player>();
                    player.gameObject.AddComponent<BoxCollider>();
                    player.gameObject.layer = LayerMask.NameToLayer(StringConstants.PlayerLayerName);
                }
                else
                {
                    player = ((GameObject) Instantiate(PlayerPrefab)).GetComponent<Player>();
                }
                _playerInstance = player;
            }

            return player;
        }
    }
}