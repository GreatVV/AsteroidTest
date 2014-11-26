using Game.Scriptable;
using UnityEngine;

namespace Game.Players.Control
{
    [RequireComponent(typeof (Player))]
    public class KeyboardControl : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (_player)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _player.Shoot();
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _player.RotationSpeed = Input.GetKey(KeyCode.RightArrow)
                                                ? -GameLogicParameters.PlayerRotateSpeed
                                                : GameLogicParameters.PlayerRotateSpeed;
                }
            
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    _player.RotationSpeed = 0;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    _player.Speed = GameLogicParameters.DefaultPlayerSpeed * transform.up;
                }
            
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    _player.Speed = Vector3.zero;
                }

                _player.Rotate(Time.deltaTime);
            }
        }
    }
}