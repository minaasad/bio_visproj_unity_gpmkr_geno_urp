using UnityEngine;
using System.Collections;

public class Cam2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.color = Color.yellow;
		GUI.Label (new Rect (1165, 10, 40, 30), "Cam 2");
	}
}
