﻿using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets
{
    [TestFixture]
    public class MovableTest
    {
        [Test]
        public void SingleTeleportTest()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroid = Substitute.For<IMovable>();
            asteroid.Position.Returns(new Vector3(1, 5, 0));

            field.AddMovable(asteroid);

            var newPosition = field.NewPositionFor(asteroid);

            Assert.AreEqual(new Vector3(1,-5,0), newPosition);
        }

        [Test]
        public void MultipleTeleportTest()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroid = Substitute.For<IMovable>();
            asteroid.Position.Returns(new Vector3(5, 0, 0));
            field.AddMovable(asteroid);

            var ship = Substitute.For<IMovable>();
            ship.Position.Returns(new Vector3(0, 5, 0));
            field.AddMovable(ship);

            var newAsteroidPosition = field.NewPositionFor(asteroid);
            Assert.AreEqual(new Vector3(-5, 0, 0), newAsteroidPosition);

            var newShipPosition = field.NewPositionFor(ship);
            Assert.AreEqual(new Vector3(0, -5, 0), newShipPosition);
        }

        [Test]
        public void SingleTeleportWithSpeed()
        {
            const int width = 10;
            const int height = 10;

            var field = new GameObject("field", typeof(Field)).GetComponent<Field>();
            field.SetSize(width, height);

            var asteroidManager = new AsteroidManager(field);

            var asteroid = asteroidManager.CreateAsteroid(new Vector3(1, -5, 0), new Vector3(0, -1, 0),takeFactoryValues:false);

            var newPosition = field.NewPositionFor(asteroid);
            asteroid.Position = newPosition;
            
            const float timePassed = 0.2f;
            asteroid.Move(timePassed);
            var newPositionAfterSpeedApplied = asteroid.Position;

            Assert.AreEqual(new Vector3(1, 4.8f, 0), newPositionAfterSpeedApplied);
            Assert.AreEqual(1, field.AllMovableObjects.Count);
        }
    }
}