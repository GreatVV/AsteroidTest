using UnityEngine;

public class TouchControl : MonoBehaviour
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
                _player.Speed = GameLogicParameters.DefaultPlayerSpeed * transform.up * Input.acceleration.z;
            }
            else
            {
                _player.Speed = Vector3.zero;
            }

            _player.Rotate(Time.deltaTime);
        }
    }
}