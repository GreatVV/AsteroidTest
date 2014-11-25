using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (Camera))]
public class Field : MonoBehaviour
{
    public List<IMovable> AllMovableObjects = new List<IMovable>();
    public float Height;

    public bool SetToCameraSize = true;
    public float Width;
    private AsteroidManager _asteroidManager;
    private Player _player;

  

    #region Events

    public event Action<IMovable> Destroyed;

    private void FireDestroyed(IMovable movable)
    {
        if (movable != null)
        {
            if (Destroyed != null)
            {
                Destroyed(movable);
            }
        }
    }

    #endregion

    public Player Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    public bool AreThereAnyAsteroids
    {
        get
        {
            return AllMovableObjects.Any(x => x is Asteroid);
        }
    }

    public Vector3 RandomFreePosition
    {
        get
        {
            var position = new Vector3();

            position.x = Random.Range(-Width / 2f, Width / 2f);
            position.y = Random.Range(-Height / 2f, Height / 2f);

            if (Player)
            {
                Vector3 playerPosition = Player.Position;
                if (Vector3.Distance(playerPosition, position) < 2)
                {
                    position += (position - playerPosition + new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1))) * Random.Range(2, 4);
                }
            }

            return position;
        }
    }

    public void SetSize(float width, float height)
    {
        Height = height;
        Width = width;
        SetToCameraSize = false;
    }

    private void Awake()
    {
        CreateAsteroidManager();
    }

    // Use this for initialization
    private void Start()
    {
        if (SetToCameraSize)
        {
            SetSizeWithAspect(camera.orthographicSize * 2f, camera.aspect);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        CheckOutOfBounds();
        UpdateMove(Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        for (int index = 0; index < AllMovableObjects.Count; index++)
        {
            IMovable allMovableObject = AllMovableObjects[index];
            if (IsOutOfBounds(allMovableObject))
            {
                if (allMovableObject is ITeleportable)
                {
                    allMovableObject.Position = NewPositionFor(allMovableObject);
                }
                else
                {
                    Remove(allMovableObject);
                    index--;
                }
            }
        }
    }

    public void Remove(IMovable movable)
    {
        if (movable == null)
        {
            return;
        }

        if (AllMovableObjects.Contains(movable))
        {
            AllMovableObjects.Remove(movable);
        }

        movable.Collided -= OnCollided;

        movable.SelfDestroy();
    }

    public void AddMovable(IMovable movable)
    {
        if (AllMovableObjects.Contains(movable))
        {
            return;
        }
        movable.Collided += OnCollided;
        movable.Destroyed += FireDestroyed; 
        AllMovableObjects.Add(movable);

        movable.GameObject.transform.parent = transform;
    }

    public void OnCollided(IMovable collideObject, IMovable collidedWith)
    {
        if (collideObject is Bullet || collidedWith is Bullet)
        {
         //   Debug.Log("Colided "+collidedWith.GameObject + " : "+collideObject);
        }

        var bullet = collideObject as Bullet ?? collidedWith as Bullet;
        var player = collideObject as Player ?? collidedWith as Player;
        var asteroid = collideObject as Asteroid ?? collidedWith as Asteroid;
        var ufo = collideObject as Ufo ?? collidedWith as Ufo;

        if (asteroid && bullet)
        {
            if (bullet.Owner is Player)
            {
                _asteroidManager.CheckCollisions(bullet, asteroid);

                if (!AreThereAnyAsteroids)
                {
                    CreateRandomAsteroids(GameLogicParameters.DefaultNumberOfNewAsteroids);
                }
            }
        }

        if (asteroid && player)
        {
            TryKillPlayer();
        }

        if (bullet && player)
        {
            if (!(bullet.Owner is Player))
            {
                TryKillPlayer();
            }
        }

        if (bullet && ufo)
        {
            if (bullet.Owner is Player)
            {
                Remove(ufo);
            }
        }
    }

    private void TryKillPlayer()
    {
        if (!Player.IsUndestructable)
        {
            Remove(Player);
        }
    }

    public bool IsOutOfBounds(IMovable movable)
    {
        Vector3 newPosition = movable.Position;

        newPosition.x = Mathf.Clamp(newPosition.x, -Width / 2f, Width / 2f);
        newPosition.y = Mathf.Clamp(newPosition.y, -Height / 2f, Height / 2f);

        return Mathf.Abs(newPosition.x) >= Width / 2f || Mathf.Abs(newPosition.y) >= Height / 2f;
    }

    public Vector3 NewPositionFor(IMovable movable)
    {
        Vector3 newPosition = movable.Position;

        newPosition.x = Mathf.Clamp(newPosition.x, -Width / 2f, Width / 2f);
        newPosition.y = Mathf.Clamp(newPosition.y, -Height / 2f, Height / 2f);

        if (Mathf.Abs(newPosition.y) >= Height / 2f)
        {
            newPosition.y *= -1;
        }

        if (Mathf.Abs(newPosition.x) >= Width / 2f)
        {
            newPosition.x *= -1;
        }

        return newPosition;
    }

    public void SpawnPlayer(Player player, bool putToCenter = false)
    {
        if (Player)
        {
            Debug.LogError("Player exists");
        }

        Player = player;
        _player.Field = this;
        AddMovable(player);

        if (putToCenter)
        {
            _player.Position = Vector3.zero;
        }
    }

    public void UpdateMove(float timePassed)
    {
        foreach (IMovable allMovableObject in AllMovableObjects)
        {
            allMovableObject.Move(timePassed);
        }
    }

    public void SetSizeWithAspect(float height, float aspect)
    {
        Height = height;
        Width = Height * aspect;
    }

    public void StartGame()
    {
        DeleteAllMovableObjects();

        SpawnPlayer();
        CreateRandomAsteroids(GameLogicParameters.DefaultNumberOfNewAsteroids);
    }

    public void SpawnPlayer()
    {
        SpawnPlayer(PlayerFactory.Instance.CreatePlayer(), true);
    }

    public void DeleteAllMovableObjects()
    {
        foreach (var allMovableObject in AllMovableObjects)
        {
            DestroyImmediate(allMovableObject.GameObject);
        }

        AllMovableObjects.Clear();
    }

    private void CreateAsteroidManager()
    {
        _asteroidManager = new AsteroidManager(this);
    }

    public void CreateUfo()
    {
        var enemy = EnemyFactory.Instance.CreateEnemy(RandomFreePosition);
        enemy.Field = this;
        AddMovable(enemy);
    }

    public void CreateRandomAsteroids(int amount)
    {
        if (_asteroidManager == null)
        {
            CreateAsteroidManager();
        }

        for (int i = 0; i < amount; i++)
        {
            _asteroidManager.CreateAsteroid(RandomFreePosition, 3);
        }
    }
}