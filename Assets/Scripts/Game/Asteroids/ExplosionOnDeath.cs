using UnityEngine;

[RequireComponent(typeof(MovableBase))]
public class ExplosionOnDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    void OnDestroyed(IMovable movable)
    {
        if (_explosionPrefab && Application.isPlaying)
        {
            var go = Instantiate(_explosionPrefab) as GameObject;
            go.transform.position = transform.position;
            Destroy(go, go.particleSystem.duration);
        }
    }

    public void Awake()
    {
        if (!_explosionPrefab || !_explosionPrefab.particleSystem)
        {
            Debug.LogError("Incorrect prefab for explosion = has no particle system");
        }

        var moveable = GetComponent<MovableBase>();
        moveable.Destroyed += OnDestroyed;
    }
}