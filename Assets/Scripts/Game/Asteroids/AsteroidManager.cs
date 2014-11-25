using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AsteroidManager
{
    public List<Asteroid> CheckCollisions(Bullet bullet, Asteroid asteroid)
    {
        if (!asteroid)
        {
            return new List<Asteroid>();
        }

        var newAsteroids = new List<Asteroid>();

        var timeToDivide = asteroid.TimeToDivide - 1;
        var position = asteroid.Position;

        Field.Remove(bullet);
        Field.Remove(asteroid);

        if (timeToDivide > 0)
        {
            var newAsteroid1 = CreateAsteroid(position, timeToDivide);
            var newAsteroid2 = CreateAsteroid(position, timeToDivide);
            
            newAsteroids.Add(newAsteroid1);
            newAsteroids.Add(newAsteroid2);
        }

        return newAsteroids;
    }

    public static Vector3 RotateSpeed(Vector3 speed, float angleInDegrees)
    {
        return new Vector3(
            speed.x * Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),
            speed.y * Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)
            );

    }

    public AsteroidManager(Field field)
    {
        Field = field;
    }

    Field Field { get; set; }

    public Asteroid CreateAsteroid(Vector3 position, int timeToDivide = 3)
    {
        Asteroid asteroid = EnemyFactory.Instance.CreateAsteroid(position, timeToDivide);
        Field.AddMovable(asteroid);
        return asteroid;
    }

    public Asteroid CreateAsteroid(Vector3 position, Vector3 speed, int timeToDivide = 3)
    {
        Asteroid asteroid = EnemyFactory.Instance.CreateAsteroid(position, speed, timeToDivide);
        Field.AddMovable(asteroid);
        return asteroid;
    }
}