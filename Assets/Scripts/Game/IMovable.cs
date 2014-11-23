using System;
using UnityEngine;

public interface IMovable
{
    Vector3 Position { get; set; }
    Vector3 Speed { get; set; }
    void Move(float timePassed);

    event Action<IMovable, IMovable> Collided;

    event Action<IMovable> Destroyed;
    void SelfDestroy();

    

    GameObject GameObject { get; }
}

public interface ITeleportable : IMovable
{
    bool WasTeleported { get; set; }
}