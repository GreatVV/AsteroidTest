using System;
using System.Linq;
using UnityEngine;

public class Player : MovableBase, ITeleportable
{
    public float RotationSpeed;

    [SerializeField]
    private AudioClip _deathSound = null;

    private bool _isUndestructable = true;

    private float _undestructableTime = GameLogicParameters.UndestructableTime;

    [SerializeField]
    private Weapon[] _weapons = null;

    #region Events

    protected virtual void FireIndestructableStateChanged(bool state)
    {
        Action<bool> handler = IndestructableStateChanged;
        if (handler != null)
        {
            handler(state);
        }
    }

    public event Action<bool> IndestructableStateChanged;

    #endregion

    public Field Field { get; set; }

    public Weapon[] Weapons
    {
        get
        {
            return _weapons;
        }
    }

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

    #region ITeleportable Members

    public bool WasTeleported { get; set; }

    public override void SelfDestroy()
    {
        SoundManager.PlaySound(_deathSound, 0.5f);

        if (Field.Player == this)
        {
            Field.Player = null;
        }

        base.SelfDestroy();
    }

    #endregion

    public void Shoot()
    {
        if (Weapons == null || Weapons.Length == 0)
        {
            var weapon = new GameObject("weapon", typeof (Weapon)).GetComponent<Weapon>();
            _weapons = new[] {weapon};
        }

        foreach (var weapon in Weapons)
        {
            var bullet = weapon.Shoot();
            if (bullet)
            {
                Field.AddMovable(bullet);
            }
        }
    }

    public void Start()
    {
        if (Weapons == null || Weapons.Length == 0)
        {
            Debug.LogError("You didn't add any weapon");
        }
    }

    public void Update()
    {
        _undestructableTime -= Time.deltaTime;
        if (_undestructableTime < 0)
        {
            IsUndestructable = false;
        }
    }

    public void Rotate(float timePassed)
    {
        transform.Rotate(new Vector3(0, 0, timePassed * RotationSpeed));
    }
}