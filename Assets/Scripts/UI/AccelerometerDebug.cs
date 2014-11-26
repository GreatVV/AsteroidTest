using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AccelerometerDebug : MonoBehaviour
    {
        public Text Text;

        private void Update()
        {
            Text.text = string.Format(
                                      "x:{0}\ny:{1}\nz:{2}",
                                      Input.acceleration.x,
                                      Input.acceleration.y,
                                      Input.acceleration.z);
        }
    }
}