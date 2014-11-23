using UnityEngine;

public class FactoryInitializer : MonoBehaviour
{
    [SerializeField]
    private AsteroidFactory asteroidFactory = null;

    [SerializeField]
    private PlayerFactory playerFactory = null; 

    void Awake()
    {
        AsteroidFactory.Instance = asteroidFactory;
        PlayerFactory.Instance = playerFactory;
    }
}