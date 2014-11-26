using UnityEngine;

namespace Game.Shared
{
    public class AutoRotate : MonoBehaviour
    {
        public Vector3 Speed = new Vector3(0,0,10);

        public void Rotate(float timePassed)
        {
            transform.Rotate(Speed * timePassed);
        }

        void Update()
        {
            Rotate(Time.deltaTime);
        }
    }
}