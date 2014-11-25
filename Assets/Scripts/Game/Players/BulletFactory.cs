using System.Linq;
using UnityEngine;

public class BulletFactory : ScriptableObject
{
    public static BulletFactory Instance { get; set; }

    public GameObject[] BulletPrefab = new GameObject[0];

    public float BulletSpeed = 10;

    public Bullet CreateBullet(Vector3 position, Vector3 direction, float bulletSpeed = -1)
    {
        if (bulletSpeed <= 0)
        {
            bulletSpeed = BulletSpeed;
        }
        Bullet bullet;
        if (BulletPrefab.Any())
        {
            bullet =
                ((GameObject) Instantiate(BulletPrefab[Random.Range(0, BulletPrefab.Length)])).GetComponent<Bullet>();
        }
        else
        {
            bullet = (new GameObject("bullet", typeof (Bullet))).GetComponent<Bullet>();
        }

        bullet.Position = position;
        bullet.Speed = direction.normalized * bulletSpeed;

        return bullet;
    }
}