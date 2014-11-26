using System.Collections.Generic;
using System.Linq;
using Game.Players;
using Game.Scriptable;
using Game.Shared;
using UnityEngine;

namespace Game.Asteroids
{
    public class Ufo : MovableBase, ITeleportable
    {
        [SerializeField]
        private Weapon[] _weapons = new Weapon[0];

        public Field Field;

        public void Update()
        {
            TryShoot();

            if (Speed.magnitude < 0.5f)
            {
                Speed = new Vector3(Random.Range(Instance.EnemyFactory.MinSpeed.x, Instance.EnemyFactory.MaxSpeed.x), Random.Range(Instance.EnemyFactory.MinSpeed.y, Instance.EnemyFactory.MaxSpeed.y));
            }
        }

        public void TryShoot()
        {
            var bullets = Shoot();
            foreach (var bullet in bullets)
            {
                Field.AddMovable(bullet);
            }
        }

        public bool WasTeleported { get; set; }

        public Weapon[] Weapons
        {
            get
            {
                return _weapons;
            }
        }

        private IEnumerable<Bullet> Shoot()
        {
            CheckWeapon();
            return _weapons.Select(weapon => weapon.Shoot()).Where(bullet => bullet);
        }

        public void CheckWeapon()
        {
            if (_weapons == null || _weapons.Length == 0)
            {
                Debug.LogWarning("There is no any weapon - create new one");

                var weapon = new GameObject("weapon", typeof (Weapon)).GetComponent<Weapon>();
                weapon.Owner = this;
                weapon.Cooldown = 0.1f;
                _weapons = new[]
                           {
                               weapon
                           };
            }
        }
    }
}