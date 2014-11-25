using System.Linq;
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
        
        var bullet = ufo.Shoot().First();

        Assert.AreSame(ufo, bullet.Owner);

        field.SpawnPlayer();

        Assert.AreEqual(3, field.AllMovableObjects.Count);

        field.OnCollided(bullet, field.Player);


    }
}