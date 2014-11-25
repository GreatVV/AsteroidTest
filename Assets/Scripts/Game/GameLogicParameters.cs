public class GameLogicParameters
{
    private static float _bulletSpeed = 10;
    private static float _playerRotateSpeed = 360;
    private static int _defaultNumberOfNewAsteroids = 4;
    private static float _defaultPlayerSpeed = 3;
    private static int _startNumberOfLifes = 3;
    public static int PointsForAsteroid = 10;

    public static float BulletSpeed
    {
        get
        {
            return _bulletSpeed;
        }
    }

    public static float PlayerRotateSpeed
    {
        get
        {
            return _playerRotateSpeed;
        }
    }

    public static int DefaultNumberOfNewAsteroids
    {
        get
        {
            return _defaultNumberOfNewAsteroids;
        }
    }

    public static float DefaultPlayerSpeed
    {
        get
        {
            return _defaultPlayerSpeed;
        }
    }

    public static int StartNumberOfLifes
    {
        get
        {
            return _startNumberOfLifes;
        }
    }

    public static float UndestructableTime = 3f;
    public static int PointsTillUfo = 100;
}