using Game;
using Game.Asteroids;
using Game.Players;
using NUnit.Framework;
using UnityEngine;

namespace Assets
{
    [TestFixture]
    public class MovableTest
    {
        [Test]
        public void MultipleTeleportTest()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroidManager = new AsteroidManager(field);

            Asteroid asteroid = asteroidManager.CreateAsteroid(new Vector3(5, 0, 0), 3);

            field.SpawnPlayer();
            Player ship = field.Player;

            Vector3 newAsteroidPosition = field.NewPositionFor(asteroid);
            Assert.AreEqual(new Vector3(-5, 0, 0), newAsteroidPosition);

            Vector3 newShipPosition = field.NewPositionFor(ship);
            Assert.AreEqual(new Vector3(0, 0, 0), newShipPosition);
        }

        [Test]
        public void SingleTeleportTest()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroidManager = new AsteroidManager(field);

            Asteroid asteroid = asteroidManager.CreateAsteroid(new Vector3(1, 5, 0), 3);

            field.AddMovable(asteroid);

            Vector3 newPosition = field.NewPositionFor(asteroid);

            Assert.AreEqual(new Vector3(1, -5, 0), newPosition);
        }

        [Test]
        public void SingleTeleportWithSpeed()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof (Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroidManager = new AsteroidManager(field);

            Asteroid asteroid = asteroidManager.CreateAsteroid(new Vector3(1, -5, 0), new Vector3(0, -1, 0), 3);

            Vector3 newPosition = field.NewPositionFor(asteroid);
            asteroid.Position = newPosition;

            const float timePassed = 0.2f;
            asteroid.Move(timePassed);
            Vector3 newPositionAfterSpeedApplied = asteroid.Position;

            Assert.AreEqual(new Vector3(1, 4.8f, 0), newPositionAfterSpeedApplied);
            Assert.AreEqual(1, field.AllMovableObjects.Count);
        }
    }
}