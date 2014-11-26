using System;
using Game.Scriptable;
using UnityEngine;

namespace Game.Players.Control
{
    public class AccelerometerControl : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();

            enabled = Input.touchSupported;
        }

        private void Update()
        {
            if (_player)
            {
                if (Input.touchCount > 0)
                {
                    _player.Shoot();
                }
                /*
            if (Mathf.Abs(Input.acceleration.x) > 0.1f)
            {
                _player.RotationSpeed = -GameLogicParameters.PlayerRotateSpeed * Input.acceleration.x;
            }
            else
            {
                _player.RotationSpeed = 0;
            }

            if (Input.acceleration.z < -0.1f)
            {
                _player.Speed = - GameLogicParameters.DefaultPlayerSpeed * transform.up * Input.acceleration.z;
            }
            else
            {
                _player.Speed = Vector3.zero;
            }

            _player.Rotate(Time.deltaTime);*/

                _player.Rotate(RotationFor(Input.acceleration), Time.deltaTime);
                _player.Speed = GameLogicParameters.DefaultPlayerSpeed * transform.up;
            }
        }

        public Quaternion RotationFor(Vector3 accelerometer)
        {
            var x = accelerometer.x;
            var y = accelerometer.y;
            var z = 0f;
        
            if (Math.Abs(x) < float.Epsilon)
            {
                z = y >= 0 ? 0 : 180;
            }
            else
            {
                if (Math.Abs(y) < float.Epsilon)
                {
                    z = x >= 0 ? -90 : 90;
                }
                else
                {
                    if (x > 0 && y > 0)
                    {
                        z = 270 + Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(x / y));
                    }
                    else
                    {
                        if (x > 0 && y < 0)
                        {
                            z = 180 + Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(y / x));
                        }
                        else
                        {
                            if (x < 0 && y < 0)
                            {
                                z = 90f + Mathf.Rad2Deg * Mathf.Atan(Mathf.Abs(y / x));
                            }
                            else
                            {
                                if (x < 0 && y > 0)
                                {
                                    z = Mathf.Rad2Deg * Mathf.Atan( Mathf.Abs(x / y));
                                }
                            }
                        }
                    }
                }
            }
        
            return Quaternion.Euler(0,0,z);
        }
    }
}