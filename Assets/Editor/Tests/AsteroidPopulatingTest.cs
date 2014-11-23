using NUnit.Framework;
using UnityEngine;
using System.Collections;

[TestFixture]
public class AsteroidPopulatingTest {

    [Test]
    public void CreateAsteroidTest()
    {
        var position = new Vector3(5, 5);
        var speed = new Vector3(1, 0);

        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10,10);

        var asteroidManager = new AsteroidManager(field);
        var newAsteroid = asteroidManager.CreateAsteroid(position, speed, takeFactoryValues:false);

        Assert.AreEqual(position, newAsteroid.Position);
        Assert.AreEqual(speed, newAsteroid.Speed);
        Assert.Contains(newAsteroid, field.AllMovableObjects);
    }

    [Test]
    public void RotateTest()
    {
        var newSpeed = AsteroidManager.RotateSpeed(new Vector3(0, 1), 30);
        Assert.AreEqual( 0, newSpeed.x);
        Assert.AreEqual( 0.5f, newSpeed.y);

        newSpeed = AsteroidManager.RotateSpeed(Vector3.zero, 60);
        Assert.AreEqual(Vector3.zero, newSpeed);
    }

    [Test]
    public void DefaultPopulatingTest()
    {
        var bullet = new GameObject("bullet",typeof(Bullet)).GetComponent<Bullet>();
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();

        field.AddMovable(bullet);

        var asteroidManager = new AsteroidManager(field);
        var asteroid = asteroidManager.CreateAsteroid(Vector3.zero, Vector3.one, 2);
        
        var newAsteroids = asteroidManager.CheckCollisions(bullet, asteroid);

        Assert.AreEqual(2, newAsteroids.Count);
        Assert.AreEqual(1, newAsteroids[0].TimeToDivide);
        Assert.AreEqual(1, newAsteroids[1].TimeToDivide);
    }

    [Test]
    public void DestroyAsteroidTest()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10,10);

        var bullet = new GameObject("bullet", typeof(Bullet)).GetComponent<Bullet>();
        field.AddMovable(bullet);
        Assert.AreEqual(1, field.AllMovableObjects.Count);

        var asteroidManager = new AsteroidManager(field);
        var asteroid = asteroidManager.CreateAsteroid(new Vector3(5,5), new Vector3(1,0), 0);

        Assert.AreEqual(2, field.AllMovableObjects.Count);

        var newAsteroids = asteroidManager.CheckCollisions(bullet, asteroid);

        Assert.AreEqual(0, newAsteroids.Count);
        Assert.AreEqual(4, field.AllMovableObjects.Count);
    }

    [Test]
    public void PlayerMoveTest()
    {
        PlayerFactory.Instance = ScriptableObject.CreateInstance<PlayerFactory>();

        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        var player = PlayerFactory.Instance.CreatePlayer();
        player.Speed = new Vector3(0, 1, 0);

        field.SpawnPlayer(player, true);

        const float timePassed = 0.2f;
        field.UpdateMove(timePassed);


        Assert.AreEqual(1, field.AllMovableObjects.Count);
        Assert.AreEqual(new Vector3(0,0.2f, 0), player.Position);
    }

    [Test]
    public void AsteroidAutoRotationTest()
    {
        var rotate = new GameObject("Rotate", typeof (AutoRotate)).GetComponent<AutoRotate>();

        rotate.Speed = new Vector3(0, 45, 0);

        rotate.transform.rotation = Quaternion.Euler(0, 90, 0);

        const int timePassed = 1;

        rotate.Rotate(timePassed);

        Assert.AreEqual(135, (int)rotate.transform.rotation.eulerAngles.y);
        Assert.AreEqual(0, rotate.transform.rotation.eulerAngles.x);


    }
}
