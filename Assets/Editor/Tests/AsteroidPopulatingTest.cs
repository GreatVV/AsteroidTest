using System.Collections.Generic;
using Game;
using Game.Asteroids;
using Game.Players;
using Game.Scriptable;
using Game.Shared;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class AsteroidPopulatingTest
{
    [Test]
    public void AsteroidAutoRotationTest()
    {
        var rotate = new GameObject("Rotate", typeof (AutoRotate)).GetComponent<AutoRotate>();

        rotate.Speed = new Vector3(0, 45, 0);

        rotate.transform.rotation = Quaternion.Euler(0, 90, 0);

        const int timePassed = 1;

        rotate.Rotate(timePassed);

        Assert.AreEqual(135, (int) rotate.transform.rotation.eulerAngles.y);
        Assert.AreEqual(0, rotate.transform.rotation.eulerAngles.x);
    }

    [Test]
    public void CreateAsteroidTest()
    {
        var position = new Vector3(5, 5);
        var speed = new Vector3(1, 0);

        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        var asteroidManager = new AsteroidManager(field);
        Asteroid newAsteroid = asteroidManager.CreateAsteroid(position, speed);

        Assert.AreEqual(position, newAsteroid.Position);
        Assert.AreEqual(speed, newAsteroid.Speed);
        Assert.Contains(newAsteroid, field.AllMovableObjects);
    }

    [Test]
    public void DefaultPopulatingTest()
    {
        var bullet = new GameObject("bullet", typeof (Bullet)).GetComponent<Bullet>();
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();

        field.AddMovable(bullet);

        var asteroidManager = new AsteroidManager(field);
        Asteroid asteroid = asteroidManager.CreateAsteroid(Vector3.zero, Vector3.one, 2);

        List<Asteroid> newAsteroids = asteroidManager.CheckCollisions(bullet, asteroid);

        Assert.AreEqual(2, newAsteroids.Count);
        Assert.AreEqual(1, newAsteroids[0].TimeToDivide);
        Assert.AreEqual(1, newAsteroids[1].TimeToDivide);
    }

    [Test]
    public void DestroyAsteroidTest()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        var bullet = new GameObject("bullet", typeof (Bullet)).GetComponent<Bullet>();
        field.AddMovable(bullet);
        Assert.AreEqual(1, field.AllMovableObjects.Count);

        var asteroidManager = new AsteroidManager(field);
        Asteroid asteroid = asteroidManager.CreateAsteroid(new Vector3(5, 5), new Vector3(1, 0), 0);

        Assert.AreEqual(2, field.AllMovableObjects.Count);

        List<Asteroid> newAsteroids = asteroidManager.CheckCollisions(bullet, asteroid);

        Assert.AreEqual(0, newAsteroids.Count);
    }

    [Test]
    public void PlayerMoveTest()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        field.SpawnPlayer();
        var player = field.Player;
        player.Speed = new Vector3(0, 1, 0);
       
        const float timePassed = 0.2f;
        field.UpdateMove(timePassed);

        Assert.AreEqual(1, field.AllMovableObjects.Count);
        Assert.AreEqual(new Vector3(0, 0.2f, 0), player.Position);
    }

    [Test]
    public void RotateTest()
    {
        Vector3 newSpeed = AsteroidManager.RotateSpeed(new Vector3(0, 1), 30);
        Assert.AreEqual(0, newSpeed.x);
        Assert.AreEqual(0.5f, newSpeed.y);

        newSpeed = AsteroidManager.RotateSpeed(Vector3.zero, 60);
        Assert.AreEqual(Vector3.zero, newSpeed);
    }
}