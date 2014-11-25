using System.Linq;
using UnityEngine;

public class EnemyFactory : ScriptableObject
{
    public static EnemyFactory Instance { get; set; }

    public GameObject[] AsteroidPrefabs = new GameObject[0];

    public GameObject[] EnemyPrefabs = new GameObject[0];

    public Vector3 MinSpeed = new Vector3(-100,-100);

    public Vector3 MaxSpeed = new Vector3(100,100);

    public Asteroid CreateAsteroid(Vector3 position, int timeToDivide = 3)
    {
        return CreateAsteroid(
                              position,
                              new Vector3(Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y), 0),
                              timeToDivide);
    }

    public Asteroid CreateAsteroid(Vector3 position, Vector3 speed, int timeToDivide = 3)
    {
        Asteroid asteroid;
        if (AsteroidPrefabs.Any())
        {
            asteroid = ((GameObject)Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Count())])).GetComponent<Asteroid>();
        } 
        else 
        {
            asteroid = new GameObject("asteroid", typeof(Asteroid)).GetComponent<Asteroid>();
        }
        asteroid.Position = position;
        asteroid.Speed = speed;
        asteroid.TimeToDivide = timeToDivide;
        asteroid.transform.localScale *= timeToDivide;
        return asteroid;
    }

    public Ufo CreateEnemy(Vector3 position)
    {
        return CreateEnemy(
                           position,
                           new Vector3(Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y), 0));
    }

    public Ufo CreateEnemy(Vector3 position, Vector3 speed)
    {
        Ufo ufo;
        if (EnemyPrefabs.Any())
        {
            ufo = ((GameObject)Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count())])).GetComponent<Ufo>();
        }
        else
        {
            ufo = new GameObject("ufo", typeof(Ufo)).GetComponent<Ufo>();
        }
        ufo.Position = position;
        ufo.Speed = speed;
        return ufo;
    }
}