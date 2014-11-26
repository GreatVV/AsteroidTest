using System.Linq;
using Game.Players;
using UnityEngine;

namespace Game.Scriptable
{
    public class BulletFactory : ScriptableObject
    {
        public GameObject[] BulletPrefab = new GameObject[0];

        public float BulletSpeed = 10;

        public Bullet CreateBullet(Vector3 position, Vector3 direction, float bulletSpeed = -1)
        {
            if (bulletSpeed <= 0)
            {
                bulletSpeed = BulletSpeed;
            }
            var bullet = ObjectPool.Instance.GetObject<Bullet>();

            if (bullet == null)
            {
                if (BulletPrefab.Any())
                {
                    bullet =
                        ((GameObject) Instantiate(BulletPrefab[Random.Range(0, BulletPrefab.Length)]))
                            .GetComponent<Bullet>();
                }
                else
                {
                    bullet = (new GameObject("bullet", typeof (Bullet))).GetComponent<Bullet>();
                }
                bullet.Destroyed += ObjectPool.Instance.OnDestroyed;
            }

            bullet.Position = position;
            bullet.Speed = direction.normalized * bulletSpeed;

            return bullet;
        }
    }
}