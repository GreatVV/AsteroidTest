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

    private void OnCollided(IMovable collideObject, IMovable collidedWith)
    {
       // Debug.Log("Colided "+collidedWith.GameObject + " : "+collideObject);
        if (collideObject is Asteroid && collidedWith is Bullet)
        {
            _asteroidManager.CheckCollisions(collidedWith as Bullet, collideObject as Asteroid);

            if (!AreThereAnyAsteroids)
            {
                CreateRandomAsteroids(GameLogicParameters.DefaultNumberOfNewAsteroids);
            }
        }

        if (collideObject is Asteroid && collidedWith is Player)
        {
            var player = collidedWith as Player;
            if (!player.IsUndestructable)
            {
                Remove(player);
            }
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
        while (AllMovableObjects.Any())
        {
            Remove(AllMovableObjects.First());
        }

        AllMovableObjects.Clear();
    }

    private void CreateAsteroidManager()
    {
        _asteroidManager = new AsteroidManager(this);
    }

    public void CreateRandomAsteroids(int amount)
    {
        if (_asteroidManager == null)
        {
            CreateAsteroidManager();
        }

        for (int i = 0; i < amount; i++)
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

            _asteroidManager.CreateAsteroid(position, Vector3.zero);
        }
    }
}