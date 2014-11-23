using System.Linq;
using UnityEngine;

public class BulletFactory : ScriptableObject
{
    public static BulletFactory Instance { get; set; }

    public GameObject[] BulletPrefab = new GameObject[0];

    public Bullet CreateBullet(Vector3 position, Vector3 direction, float speed)
    {
        Bullet bullet;
        if (BulletPrefab.Any())
        {
            bullet = ((GameObject) Instantiate(BulletPrefab[Random.Range(0, BulletPrefab.Length)])).GetComponent<Bullet>();
        }
        else
        {
            bullet = (new GameObject("bullet", typeof (Bullet))).GetComponent<Bullet>();
        }
        
        bullet.Position = position;
        bullet.Speed = direction.normalized * speed;
        
        return bullet;
    }
}