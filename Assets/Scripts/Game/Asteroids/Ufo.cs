using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ufo : MovableBase, ITeleportable
{
    [SerializeField]
    private Weapon[] weapons;

    private Field _field;

    private void Start()
    {
        _field = FindObjectOfType<Field>();
    }

    public void Update()
    {
        foreach (var bullet in Shoot())
        {
            _field.AddMovable(bullet);
        }  

        if (Speed.magnitude < 0.5f)
        {
            Speed = new Vector3(Random.Range(EnemyFactory.Instance.MinSpeed.x, EnemyFactory.Instance.MaxSpeed.x), Random.Range(EnemyFactory.Instance.MinSpeed.y, EnemyFactory.Instance.MaxSpeed.y));
        }
    }

    public bool WasTeleported { get; set; }

    public IEnumerable<Bullet> Shoot()
    {
        return weapons.Select(weapon => weapon.Shoot()).Where(bullet => bullet);
    }
}