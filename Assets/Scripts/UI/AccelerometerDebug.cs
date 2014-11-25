using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccelerometerDebug : MonoBehaviour
{

    public Text text;
	
	// Update is called once per frame
	void Update ()
	{
	    text.text = string.Format("x:{0}\ny:{1}\nz:{2}",Input.acceleration.x,Input.acceleration.y,Input.acceleration.z);
	}
}
