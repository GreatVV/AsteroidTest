using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Linq;

[TestFixture]
public class FieldCreationTest 
{
    [Test]
    public void SetToCameraSize()
    {
        var cameraWidth = 16f;
        var cameraHeight = 9f;

        var aspect = cameraWidth / cameraHeight;

        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetToCameraSize = true;

        field.SetSizeWithAspect(cameraHeight, aspect);

        Assert.AreEqual(16f, field.Width);
        Assert.AreEqual(9f, field.Height);
    }

    [Test]
    public void CreatePlayer()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10,10);
        field.StartGame();

        Assert.AreEqual(5, field.AllMovableObjects.Count);
        Assert.AreEqual(4, field.AllMovableObjects.Count(x=>x is Asteroid));
        Assert.AreEqual(1, field.AllMovableObjects.Count(x=>x is Player));
        Assert.AreEqual(new Vector3(0,0,0), field.Player.Position);

    }

    [Test]
    public void PlayerShootCreationTest()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10, 10);
        field.StartGame();

        field.Player.Shoot();

        Assert.AreEqual(1, field.Player.ShootedBullets.Count);
        Assert.AreEqual(6, field.AllMovableObjects.Count);
    }

    [Test]
    public void BulletCreationTest()
    {
        var bulletFactory = ScriptableObject.CreateInstance<BulletFactory>();
        var bullet = bulletFactory.CreateBullet(Vector3.zero, new Vector3(1,0), 10);

        Assert.AreEqual(Vector3.zero, bullet.Position);
        Assert.AreEqual(new Vector3(10,0), bullet.Speed);
    }

    [Test]
    public void CreateRandomAsteroids()
    {
        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10, 10);

        field.CreateRandomAsteroids(4);

        Assert.AreEqual(4, field.AllMovableObjects.Count);
    }

    [Test]
    public void PlayerDiedTest()
    {
        var session = new GameObject("session", typeof(Session)).GetComponent<Session>();
        

        var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
        field.SetSize(10, 10);
        
        session.Field = field;
        session.Restart();

        Assert.AreEqual(GameLogicParameters.StartNumberOfLifes, session.Lifes);

        field.Remove(field.Player);

        Assert.AreEqual(GameLogicParameters.StartNumberOfLifes - 1, session.Lifes);

        Assert.AreNotSame(null, field.Player);
    }
}
