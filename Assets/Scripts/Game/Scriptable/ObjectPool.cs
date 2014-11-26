using System;
using System.Collections.Generic;
using System.Linq;
using Game.Shared;

namespace Game.Scriptable
{
    public class ObjectPool
    {
        private static ObjectPool _instance;

        public static ObjectPool Instance
        {
            get
            {
                return _instance ?? (_instance = new ObjectPool());
            }
        }

        public int Count
        {
            get
            {
                return _movables.Count;
            }
        }

        private ObjectPool()
        {
            
        }

        [NonSerialized]
        private List<IMovable> _movables = new List<IMovable>();

        public T GetObject<T>() where T:IMovable
        {
            var any = _movables.FirstOrDefault(x => x is T);
            if (any != null)
            {
                _movables.Remove(any);
                any.GameObject.SetActive(true);
            }
            return (T)any;
        }

        public void OnDestroyed(IMovable movable)
        {
            _movables.Add(movable);
        }

        public void Clear()
        {
            _movables.Clear();
        }
    }
}