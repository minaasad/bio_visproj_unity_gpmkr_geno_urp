using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	
	float speed = 0.0001f;
	float rotationSpeed = 2f;
	
	// Use this for initialization
	void Start () {
		
	}

	void MoveLeft() {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(UnityEngine.Camera.main != null)
		{
			UnityEngine.Camera.main.transform.Translate(new Vector3(xAxisValue-=speed, 0.0f, zAxisValue));
		}
	}

	void MoveUp() {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(UnityEngine.Camera.main != null)
		{
			UnityEngine.Camera.main.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue+=speed));
		}
	}

	void MoveRight() {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(UnityEngine.Camera.main != null)
		{
			UnityEngine.Camera.main.transform.Translate(new Vector3(xAxisValue+=speed, 0.0f, zAxisValue));
		}
	}

	void MoveDown() {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(UnityEngine.Camera.main != null)
		{
			UnityEngine.Camera.main.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue-=speed));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(KeyCode.D)) {
			MoveRight();
		}
		if (Input.GetKey(KeyCode.A)) {
			MoveLeft();
		}
		if (Input.GetKey(KeyCode.W)) {
			MoveUp();
		}
		if (Input.GetKey(KeyCode.S)) {
			MoveDown();
		}
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.up, rotationSpeed);
			}
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.up, -rotationSpeed);
			}
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.right, rotationSpeed);
			}
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.right, -rotationSpeed);
			}
		}
		if (Input.GetKey(KeyCode.E)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.forward, rotationSpeed);
			}
		}
		if (Input.GetKey(KeyCode.F)) {
			if(UnityEngine.Camera.main != null)
			{
				transform.RotateAround(transform.position, transform.forward, -rotationSpeed);
			}
		}
		
		
	}
}
