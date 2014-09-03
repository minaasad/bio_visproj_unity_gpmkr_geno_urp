using UnityEngine;
using System.Collections;

public class PatientComponent : MonoBehaviour {
	
	public string SelectedPatientID;
	Color initialColor;
	GameObject theText;

	// Use this for initialization
	void Start () 
	{
		theText = new GameObject();
		theText.transform.position = renderer.transform.position;
		theText.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		// mesh render
		theText.AddComponent<MeshRenderer> ();
		MeshRenderer meshRenderer = (MeshRenderer)theText.GetComponent (typeof(MeshRenderer));
		meshRenderer.material = Resources.Load ("OpenSans-Regular", typeof(Material)) as Material;
		meshRenderer.material.color = Color.white;
		
		// text mesh
		theText.AddComponent<TextMesh> ();
		TextMesh tMesh = (TextMesh)theText.GetComponent (typeof(TextMesh));
		Font OpenSansFont = Resources.Load ("OpenSans-Regular", typeof(Font)) as Font;
		tMesh.text = SelectedPatientID;
		tMesh.font = OpenSansFont;
	}
	
	// Update is called once per frame
	void Update () {
		theText.transform.LookAt (2 * theText.transform.position - Camera.main.transform.position);
	}
	
	void OnMouseEnter () {
		initialColor = renderer.material.color;
		renderer.material.color = Color.yellow;//new Color (1, 1, 1, 1);
	}
	
	void OnMouseExit () {
		renderer.material.color = initialColor;
	}
	
}

