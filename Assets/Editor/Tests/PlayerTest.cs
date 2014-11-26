using System.Linq;
using Game;
using Game.Shared;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class PlayerTest
{
    [Test]
    public void RotateTest()
    {
        const int width = 10;
        const int height = 10;

        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(width, height);

        field.StartGame();

        field.Player.transform.rotation = Quaternion.Euler(0, 0, 0);

        field.Player.RotationSpeed = 10;
        float timePassed = 0.4f;
        field.Player.Rotate(timePassed);

        Assert.AreEqual(4, Mathf.RoundToInt(field.Player.transform.rotation.eulerAngles.z));
    }

    [Test]
    public void Shoot()
    {
        const int width = 10;
        const int height = 10;

        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(width, height);

        field.StartGame();

        field.Player.transform.rotation = Quaternion.Euler(0, 0, 0);

        field.Player.Shoot();

        Assert.AreEqual(1, field.Player.Weapons[0].ShootedBullets.Count);

        IMovable bullet = field.Player.Weapons[0].ShootedBullets.First();

        Assert.AreEqual(new Vector3(0, 10), bullet.Speed);
    }
}