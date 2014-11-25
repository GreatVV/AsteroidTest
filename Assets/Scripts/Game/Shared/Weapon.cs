using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public float Cooldown = 0.1f;

    private float _currentCooldown = 0f;

    [SerializeField]
    private AudioClip _shootSound = null;

    public List<IMovable> ShootedBullets = new List<IMovable>();

    public BulletFactory BulletFactory;
    private MovableBase _owner;
    public float BulletSpeed = -1f;

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

    public Bullet Shoot()
    {
        if (_currentCooldown > 0)
            return null;

        _currentCooldown = Cooldown;

        CheckBulletFactory();

        Bullet bullet = BulletFactory.CreateBullet(transform.position, transform.up, BulletSpeed);
        bullet.transform.rotation = transform.rotation;
        bullet.Destroyed += OnBulletDestroyed;
        bullet.Owner = Owner;
        bullet.gameObject.layer = Owner.gameObject.layer;
        ShootedBullets.Add(bullet);

        SoundManager.PlaySound(_shootSound, 0.2f);

        return bullet;
    }

    public MovableBase Owner
    {
        get
        {
            if (!_owner)
            {
                _owner = GetComponentInParent<MovableBase>();
            }
            return _owner;
        }
        set
        {
            _owner = value;
        }
    }

    void Update()
    {
        if (_currentCooldown >= 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void OnBulletDestroyed(IMovable bullet)
    {
        ShootedBullets.Remove(bullet);
    }
}