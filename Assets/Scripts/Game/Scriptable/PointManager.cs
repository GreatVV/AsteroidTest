using UnityEngine;

public class PointManager : ScriptableObject
{
    private static PointManager _instance;

    public int PointsForAsteroid = 10;
    public int PointsForUfo = 100;

    public static PointManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            Debug.LogWarning("Point Manager is null - create new one");
            return _instance = CreateInstance<PointManager>();
        }
        set
        {
            _instance = value;
        }
    }

    public int GetPointsForAsteroid(Asteroid asteroid)
    {
        switch (asteroid.TimeToDivide)
        {
            case (1):
                return PointsForAsteroid * 3;
            case (2):
                return PointsForAsteroid * 2;
            case (3):
                return PointsForAsteroid;
        }
        return PointsForAsteroid * asteroid.TimeToDivide;
    }

    public int GetPointsForMovable(IMovable movable)
    {
        var asteroid = movable as Asteroid;
        if (asteroid)
        {
            return GetPointsForAsteroid(asteroid);
        }

        var ufo = movable as Ufo;
        if (ufo)
        {
            return PointsForUfo;
        }

        var bullet = movable as Bullet;
        if (bullet)
        {
            return 0;
        }

        Debug.Log("Try get points for "+movable.GetType().Name);
        return 0;
    }
}