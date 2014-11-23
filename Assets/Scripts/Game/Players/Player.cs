using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableBase, ITeleportable
{
    public BulletFactory BulletFactory;
    public float RotationSpeed;

    public List<IMovable> ShootedBullets = new List<IMovable>();

    public Field Field { get; set; }

    #region ITeleportable Members

    public bool WasTeleported { get; set; }

    public bool IsUndestructable
    {
        get
        {
            return _isUndestructable;
        }
        set
        {
            _isUndestructable = value;
            FireIndestructableStateChanged(value);
        }
    }

    public event Action<bool> IndestructableStateChanged;

    protected virtual void FireIndestructableStateChanged(bool state)
    {
        Action<bool> handler = IndestructableStateChanged;
        if (handler != null)
        {
            handler(state);
        }
    }

    [SerializeField]
    private AudioClip _shootSound = null;
    [SerializeField]
    private AudioClip _deathSound = null;

    #endregion

    public void Shoot()
    {
        CheckBulletFactory();

        Bullet bullet = BulletFactory.CreateBullet(Position, transform.up, GameLogicParameters.BulletSpeed);
        bullet.transform.rotation = transform.rotation;
        bullet.Destroyed += OnBulletDestroyed;
        ShootedBullets.Add(bullet);

        SoundManager.PlaySound(_shootSound, 0.2f);

        Field.AddMovable(bullet);
    }

   

    public override void SelfDestroy()
    {
        SoundManager.PlaySound(_deathSound, 0.5f);

        if (Field.Player == this)
        {
            Field.Player = null;
        }

        base.SelfDestroy();
    }

    public void OnBulletDestroyed(IMovable bullet)
    {
        ShootedBullets.Remove(bullet);
    }

    private void Start()
    {
        CheckBulletFactory();
        
    }

    private void CheckBulletFactory()
    {
        if (!BulletFactory)
        {
            if (!BulletFactory.Instance)
            {
                BulletFactory.Instance = ScriptableObject.CreateInstance<BulletFactory>();
            }

            BulletFactory = BulletFactory.Instance;
        }
    }

    private bool _isUndestructable = true;

    private float _undestructableTime = GameLogicParameters.UndestructableTime;

    public void Update()
    {
        _undestructableTime -= Time.deltaTime;
        if (_undestructableTime < 0)
        {
            IsUndestructable = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            RotationSpeed = Input.GetKey(KeyCode.RightArrow) ? -GameLogicParameters.PlayerRotateSpeed : GameLogicParameters.PlayerRotateSpeed;
        }
        else
        {
            RotationSpeed = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Speed = GameLogicParameters.DefaultPlayerSpeed * transform.up;
        }
        else
        {
            Speed = Vector3.zero;
        }

        Rotate(Time.deltaTime);
    }

    public void Rotate(float timePassed)
    {
        transform.Rotate(new Vector3(0, 0, timePassed * RotationSpeed));
    }
}