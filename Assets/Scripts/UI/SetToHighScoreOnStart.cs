using System.Globalization;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetToHighScoreOnStart : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GetComponent<Text>().text = string.Format("High score: {0}", PlayerPrefs.GetInt(StringConstants.HighScorePref, 0));
	}
	
}
