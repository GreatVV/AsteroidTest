public class PointManager
{
    private static PointManager _instance;

    public static PointManager Instance
    {
        get
        {
            return _instance ?? (_instance = new PointManager());
        }
    }

    public int GetPointsForAsteroid(Asteroid asteroid)
    {
        switch (asteroid.TimeToDivide)
        {
            case (1):
                return GameLogicParameters.PointsForAsteroid * 3;
            case (2):
                return GameLogicParameters.PointsForAsteroid * 2;
            case (3):
                return GameLogicParameters.PointsForAsteroid;
        }
        return GameLogicParameters.PointsForAsteroid * asteroid.TimeToDivide;
    }
}