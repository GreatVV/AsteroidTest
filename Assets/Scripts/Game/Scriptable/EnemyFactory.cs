using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Game.Asteroids;
using Game.Shared;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Scriptable
{
    public class EnemyFactory : ScriptableObject
    {
        private static EnemyFactory _instance;
        public GameObject[] AsteroidPrefabs = new GameObject[0];

        public GameObject[] EnemyPrefabs = new GameObject[0];

        public Vector3 MaxSpeed = new Vector3(100, 100);
        public Vector3 MinSpeed = new Vector3(-100, -100);

        public Vector3 DefaultAsteroidSize = new Vector3(0.3f, 0.3f, 0.3f);
        public float DefaultAsteroidMass = 5;

        public static EnemyFactory Instance
        {
            get
            {
                if (!_instance)
                {
                    return CreateInstance<EnemyFactory>();
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public Asteroid CreateAsteroid(Vector3 position, int timeToDivide = 3)
        {
            return CreateAsteroid(
                                  position,
                                  new Vector3(Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y), 0),
                                  timeToDivide);
        }

        public Asteroid CreateAsteroid(Vector3 position, Vector3 speed, int timeToDivide = 3)
        {
            var asteroid = ObjectPool.Instance.GetObject<Asteroid>();
            if (asteroid == null)
            {
                if (AsteroidPrefabs.Any())
                {
                    asteroid =
                        ((GameObject) Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Count())]))
                            .GetComponent<Asteroid>();
                }
                else
                {
                    asteroid = new GameObject("asteroid", typeof (Asteroid)).GetComponent<Asteroid>();
                    asteroid.gameObject.AddComponent<Rigidbody2D>();
                }
                asteroid.Destroyed += ObjectPool.Instance.OnDestroyed;
            }
            asteroid.Position = position;
            asteroid.Speed = speed;
            asteroid.TimeToDivide = timeToDivide;
            asteroid.transform.localScale = DefaultAsteroidSize * timeToDivide;
            asteroid.rigidbody2D.mass = DefaultAsteroidMass * timeToDivide;
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
            var ufo = ObjectPool.Instance.GetObject<Ufo>();
            if (ufo == null)
            {
                if (EnemyPrefabs.Any())
                {
                    ufo =
                        ((GameObject) Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count())]))
                            .GetComponent<Ufo>();
                }
                else
                {
                    ufo = new GameObject("ufo", typeof (Ufo)).GetComponent<Ufo>();
                    ufo.gameObject.layer = LayerMask.NameToLayer(StringConstants.AsteroidLayerName);
                }
                ufo.Destroyed += ObjectPool.Instance.OnDestroyed;
            }
            ufo.Position = position;
            ufo.Speed = speed;
            return ufo;
        }
    }
}