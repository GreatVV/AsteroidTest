﻿using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UfoTest
{
    [Test]
    public void CreateTest()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();

        Assert.AreEqual(0, field.AllMovableObjects.Count);

        field.CreateUfo();

        Assert.AreEqual(1, field.AllMovableObjects.Count);
        Assert.That(field.AllMovableObjects.First().GameObject.GetComponent<Ufo>() != null);
    }

    [Test]
    public void UfoBulletTest()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();

        Assert.AreEqual(0, field.AllMovableObjects.Count);

        field.CreateUfo();

        var ufo = field.AllMovableObjects.First() as Ufo;
        Assert.IsNotNull(ufo);

        Assert.AreEqual(1, field.AllMovableObjects.Count);

        ufo.TryShoot();

        var bullet = field.AllMovableObjects.First(x=>x is Bullet) as Bullet;
        Assert.IsNotNull(bullet);
        Assert.AreSame(ufo, bullet.Owner);
        Assert.AreEqual(2, field.AllMovableObjects.Count);

        field.SpawnPlayer();

        Assert.AreNotEqual(field.Player.gameObject.layer, bullet.gameObject.layer);
        Assert.AreEqual(LayerMask.NameToLayer(StringConstants.PlayerLayerName), field.Player.gameObject.layer);
        Assert.AreEqual(LayerMask.NameToLayer(StringConstants.AsteroidLayerName), bullet.gameObject.layer);

        field.Player.IsUndestructable = false;

        Assert.AreEqual(3, field.AllMovableObjects.Count);

        field.OnCollided(bullet, field.Player);

        Assert.That(!field.Player);
    }

    [Test]
    public void WeaponTest()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SpawnPlayer();

        var weapon = field.Player.Weapons.First();

        var targetBulletSpeed = 15;

        weapon.BulletSpeed = targetBulletSpeed;

        var bullet = weapon.Shoot();

        Assert.AreEqual(targetBulletSpeed, bullet.Speed.magnitude);
    }
}