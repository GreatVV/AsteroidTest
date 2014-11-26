using System.Linq;
using Game;
using Game.Asteroids;
using Game.Players;
using Game.Scriptable;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class FieldCreationTest
{
    [SetUp]
    public void SetUp()
    {
        ObjectPool.Instance.Clear();
    }

    [Test]
    public void BulletCreationTest()
    {
        var bulletFactory = ScriptableObject.CreateInstance<BulletFactory>();
        Bullet bullet = bulletFactory.CreateBullet(Vector3.zero, new Vector3(1, 0));

        Assert.AreEqual(Vector3.zero, bullet.Position);
        Assert.AreEqual(bulletFactory.BulletSpeed, bullet.Speed.magnitude);
    }

    [Test]
    public void CreatePlayer()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);
        field.StartGame();

        Assert.AreEqual(5, field.AllMovableObjects.Count);
        Assert.AreEqual(4, field.AllMovableObjects.Count(x => x is Asteroid));
        Assert.AreEqual(1, field.AllMovableObjects.Count(x => x is Player));
        Assert.AreEqual(new Vector3(0, 0, 0), field.Player.Position);
    }

    [Test]
    public void CreateRandomAsteroids()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        field.CreateRandomAsteroids(4);

        Assert.AreEqual(4, field.AllMovableObjects.Count);
    }

    [Test]
    public void DeleteTest()
    {
        var session = new GameObject("session", typeof (Session)).GetComponent<Session>();
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        session.Field = field;
        session.Restart();

        Assert.AreEqual(5, field.AllMovableObjects.Count);

        field.DeleteAllMovableObjects();

        Assert.AreEqual(0, field.AllMovableObjects.Count);
    }

    [Test]
    public void PlayerDiedTest()
    {
        var session = GetSession();

        var field = GetField();
        field.SetSize(10, 10);

        session.Field = field;
        session.Restart();

        Assert.AreEqual(Instance.GameLogicParameters.StartNumberOfLifes, session.Lifes);

        field.Remove(field.Player);

        Assert.AreEqual(Instance.GameLogicParameters.StartNumberOfLifes - 1, session.Lifes);

        Assert.AreNotSame(null, field.Player);
    }

    private static Session GetSession()
    {
        return new GameObject("session", typeof (Session)).GetComponent<Session>();
    }

    private static Field GetField()
    {
        return new GameObject("field", typeof (Field)).GetComponent<Field>();
    }

    [Test]
    public void PlayerShootCreationTest()
    {
        var field = GetField();
        field.SetSize(10, 10);
        field.StartGame();
        
        var player = field.Player;
        player.Reload();
        player.Shoot();
        

        var bullets = field.AllMovableObjects.Where(x => x is Bullet);
        Assert.AreEqual(field.Player.Weapons.Count(), bullets.Count());
        Assert.AreEqual(1 + 2 + Instance.GameLogicParameters.DefaultNumberOfNewAsteroids, field.AllMovableObjects.Count);
    }

    [Test]
    public void RandomFieldPoint()
    {
        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        int width = 10;
        int height = 10;
        field.SetSize(width, height);
        field.SpawnPlayer();

        Vector3 randomPosition = field.RandomFreePosition;

        Assert.Greater(randomPosition.x, -width / 2f);
        Assert.Greater(randomPosition.y, -height / 2f);
        Assert.Less(randomPosition.x, width / 2f);
        Assert.Less(randomPosition.y, height / 2f);
        Assert.Greater(Vector3.Distance(field.Player.Position, randomPosition), 2);
    }

    [Test]
    public void SetToCameraSize()
    {
        float cameraWidth = 16f;
        float cameraHeight = 9f;

        float aspect = cameraWidth / cameraHeight;

        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
        field.SetToCameraSize = true;

        field.SetSizeWithAspect(cameraHeight, aspect);

        Assert.AreEqual(16f, field.Width);
        Assert.AreEqual(9f, field.Height);
    }

    [Test]
    public void ObjectPoolTest()
    {
        ObjectPool.Instance.Clear();
        var field = GetField();
        var session = GetSession();

        session.Field = field;

        field.SpawnPlayer();

        var player = field.Player;
       

        Assert.AreEqual(1, field.AllMovableObjects.Count);

        player.Shoot();

        Assert.AreEqual(1 + player.Weapons.Count(), field.AllMovableObjects.Count);

        var bullets = field.AllMovableObjects.Where(x => x is Bullet).ToArray();

        foreach (var bullet in bullets)
        {
            field.Remove(bullet);
            Assert.IsNotNull(bullet);
            Assert.AreEqual(false, bullet.GameObject.activeSelf);
        }

        Assert.AreEqual(1, field.AllMovableObjects.Count);

        foreach (var weapon in player.Weapons)
        {
            weapon.Reload();
        }

        player.Shoot();

        Assert.AreEqual(1+player.Weapons.Count(), field.AllMovableObjects.Count);

        var newBullets = field.AllMovableObjects.Where(x => x is Bullet).ToArray();

        foreach (var bullet in bullets)
        {
            Assert.AreEqual(true, bullet.GameObject.activeSelf);
        }
        for (int i = 0; i < newBullets.Count(); i++)
        {
            var bullet = bullets[i];
            var newBullet = newBullets[i];
            Assert.AreSame(bullet, newBullet);
        }
        
    }
}