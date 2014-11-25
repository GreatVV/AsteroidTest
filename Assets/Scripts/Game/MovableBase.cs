using System;
using UnityEngine;

public class MovableBase : MonoBehaviour, IMovable
{
    #region Events

    public event Action<IMovable> Destroyed;

    protected virtual void FireCollided(IMovable arg1, IMovable arg2)
    {
        Action<IMovable, IMovable> handler = Collided;
        if (handler != null)
        {
            handler(arg1, arg2);
        }
    }

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

    #region IMovable Members

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public virtual Vector3 Speed { get; set; }

    public virtual void Move(float timePassed)
    {
        Position += Speed * timePassed;
    }

    public event Action<IMovable, IMovable> Collided;

    public virtual void SelfDestroy()
    {
        if (gameObject)
        {
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            FireDestroyed(this);
        }
    }

    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D other)
    {
        var collidedWith = other.gameObject.GetComponent<MovableBase>();
        FireCollided(this, collidedWith);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collidedWith = other.gameObject.GetComponent<MovableBase>();
        FireCollided(this, collidedWith);
    }
}