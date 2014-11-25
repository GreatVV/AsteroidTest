using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float _cooldown = 0.1f;

    private float _currentCooldown = 0f;

    [SerializeField]
    private AudioClip _shootSound = null;

    public List<IMovable> ShootedBullets = new List<IMovable>();

    public BulletFactory BulletFactory;
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

        _currentCooldown = _cooldown;

        CheckBulletFactory();

        Bullet bullet = BulletFactory.CreateBullet(transform.position, transform.up, GameLogicParameters.BulletSpeed);
        bullet.transform.rotation = transform.rotation;
        bullet.Destroyed += OnBulletDestroyed;
        bullet.Owner = Owner;
        ShootedBullets.Add(bullet);

        SoundManager.PlaySound(_shootSound, 0.2f);

        return bullet;
    }

    public IMovable Owner { get; set; }

    void Update()
    {
        if (_currentCooldown >= 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public void Start()
    {
        if (Owner == null)
        {
            Owner = GetComponentInParent<MovableBase>();
        }
    }

    public void OnBulletDestroyed(IMovable bullet)
    {
        ShootedBullets.Remove(bullet);
    }
}