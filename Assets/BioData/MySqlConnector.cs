using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using AssemblyCSharp;
using System.Collections.Generic;
using System;

public class MySqlConnector : MonoBehaviour {
	//Attributess
	MySqlConnection dbConnection;
	List<Patient> extractedPatientDataSet = new List<Patient>();
	Hashtable firstPatientSelection, secondPatientSelection;
	Color guiColor = Color.green, guiBeingViewed = Color.yellow;
	float camFieldOfView = 86;
	float speed = 0.0001f;
	float rotationSpeed = 2f;

	void AssignPatientSelection(int mID)
	{
		RaycastHit hitInfo = new RaycastHit();
		bool hit = Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
		
		if (hit) 
		{
			foreach (Patient patient in extractedPatientDataSet) 
			{
				if (patient.ID.Equals(hitInfo.transform.gameObject.name)) 
				{
					if (mID==0) 
					{
						firstPatientSelection["ID"] = patient.ID;
						firstPatientSelection["PositionX"] = hitInfo.transform.position.x;
						firstPatientSelection["PositionY"] = hitInfo.transform.position.y;
						firstPatientSelection["PositionZ"] = hitInfo.transform.position.z;
					}
					else if (mID==1) 
					{
						secondPatientSelection["ID"] = patient.ID;
						secondPatientSelection["PositionX"] = hitInfo.transform.position.x;
						secondPatientSelection["PositionY"] = hitInfo.transform.position.y;
						secondPatientSelection["PositionZ"] = hitInfo.transform.position.z;
					}
				}
			}
		}
	}

	//Retrieve list of processed patients
	List<Patient> GetPatientsListFromData(MySqlDataReader dataReader)
	{
		while (dataReader.Read()) 
		{
			Patient newPatient = new Patient
			(
					/*Position X*/                      float.Parse(dataReader["Position_X"].ToString())/50.0f,
					/*Position Y*/                      float.Parse(dataReader["Position_Y"].ToString())/50.0f,
					/*Position Z*/                      float.Parse(dataReader["Position_Y"].ToString())/50.0f,
					/*Patient ID*/                      dataReader["Patient_ID"].ToString(),
					/*Patient Sex*/                     dataReader["Gender"].ToString().Equals("Male")?GenderType.male:GenderType.female,
					/*Patient Survived*/                (dataReader["Date_death"].ToString().Trim().Equals("NULL") || dataReader["Date_death"].Equals(DBNull.Value))?true:false,

					/*Cluster*/                         int.Parse(dataReader["Cluster"].ToString()),
					/*Ethnicity*/                       dataReader["Ethnicity"].ToString(),
					/*HasRelapsed*/                     dataReader["Relapse"].ToString().Equals("Yes")?true:false,
					/*Chrom_Structure*/                 dataReader["Chrom_structure"].ToString(),
					/*Cytogenics*/                      dataReader["Cytogenetics"].ToString(),
					/*FISH*/                            dataReader["FISH"].ToString(),
					/*PH_rear*/                         dataReader["PH_rearr"].ToString().Equals("Positive")?true:false,
					/*TEL_AML1*/                        dataReader["TEL_AML1"].ToString().Equals("Positive")?true:false,
					/*MLL_AF4*/                         dataReader["MLL_AF4"].ToString().Equals("Positive")?true:false,
					/*E2A_PBX1*/                        dataReader["E2A_PBX1"].ToString().Equals("Positive")?true:false,
					/*Initial_WBCx109*/                 dataReader["Initial_WBCx109"].ToString(),
					/*Initial_Plateletx109*/            dataReader["Initial_plateletx109"].ToString(),
					/*Initial_Blastsx109*/              dataReader["Initial_Blastsx109"].ToString(),
					/*Blasts_Percent*/                  dataReader["Blasts_percent"].ToString(),
					/*Splenomegaly*/                    dataReader["Splenomegaly"].ToString(),
					/*Phenotype*/                       dataReader["Phenotype"].ToString(),
					/*Immunophenotype*/                 dataReader["Immunophenotype"].ToString(),
					/*CNS_Involvement*/                 dataReader["CNS_involvement"].ToString().Equals("Yes")?true:false,
					/*Mediastinal_Involvement*/         dataReader["Mediastinal_involvement"].ToString().Equals("Yes")?true:false,
					/*DOB*/                             dataReader["Date_birth"].ToString(),
					/*DOG*/                             dataReader["Date_diagnosis"].ToString()

			);

			extractedPatientDataSet.Add(newPatient);
		}
		
		return extractedPatientDataSet;
	}

//	IEnumerator ViewSinglePatientDetails()
//	{
//		currentlyViewingDouble = false;
//		currentlyViewingSingle = true;
//		yield return new WaitForSeconds (10);
//		currentlyViewingSingle = false;
//	}

	void OnGUI () {
		GUIUtility.ScreenToGUIPoint (new Vector2 (33, 2));

		//On-Screen Movement Controls for Touch Screens
		if(GUI.RepeatButton(new Rect(220,10,30,20), "U"))
		{
			MoveUp();
		}
		if(GUI.RepeatButton(new Rect(220,30,30,20), "L"))
		{
			MoveLeft();
		}
		if(GUI.RepeatButton(new Rect(220,50,30,20), "R"))
		{
			MoveRight();
		}
		if(GUI.RepeatButton(new Rect(220,70,30,20), "D"))
		{
			MoveDown();
		}

		// Preferences Box
		GUI.Box(new Rect(10,10,200,100), extractedPatientDataSet.Count + " patients extracted");
		//camFieldOfView = GUI.HorizontalSlider (new Rect (10, 10, 100, 100), camFieldOfView, 10, 170);
		//UnityEngine.Camera.main.fieldOfView = camFieldOfView;

		//Toggle First Selection
		String labelTextFirst = "Select 1st";
		if(firstPatientSelection["ID"].ToString()!="") 
		{
			labelTextFirst = firstPatientSelection["ID"].ToString();
			GUI.color = guiColor;
		}
		else GUI.color = Color.clear;
		if(GUI.Button(new Rect(20,40,80,20), labelTextFirst)) 
		{
			if (bool.Parse(firstPatientSelection["CurrentlyBeingViewed"].ToString())) 
				firstPatientSelection["CurrentlyBeingViewed"]=false;
			else 
			{
				if (firstPatientSelection["ID"].ToString()!="") 
				{
					firstPatientSelection["CurrentlyBeingViewed"]=true; //StartCoroutine("ViewSinglePatientDetails()");
				}
			}
		}

		if (bool.Parse(firstPatientSelection["CurrentlyBeingViewed"].ToString())) 
		{
			GUI.color = guiBeingViewed;
			foreach (Patient patient in extractedPatientDataSet) 
			{
				if (patient.ID.Equals(firstPatientSelection["ID"].ToString())) 
				{
					GUI.Label (new Rect(20,140,260,20), "ID# "+patient.ID+", a(n) "+patient.Ethnicity.ToString()+" "+patient.Gender.ToString());
					GUI.Label (new Rect(20,160,120,20), patient.Initial_WBCx109+" WBCx109");
					GUI.Label (new Rect(20,180,120,20), patient.HasSurvived?"survived":"did not survive");
					GUI.Label (new Rect(20,200,120,20), patient.HasRelapsed?"relapsed":"did not relapse");
					GUI.Label (new Rect(20,220,260,20), "chrom "+patient.Chrom_Structure);
					GUI.Label (new Rect(20,240,120,20), "cluster "+patient.Cluster+", E2A "+patient.E2A_PBX1);
					GUI.Label (new Rect(20,260,800,20), "cyto "+patient.Cytogenics);
					GUI.Label (new Rect(20,280,240,20), "born "+patient.Date_of_Birth+", diagnosed "+patient.Date_of_Diagnosis);
					GUI.Label (new Rect(20,300,260,20), "splenomegaly "+patient.Splenomegaly);
					GUI.Button(new Rect(20,320,30,20), "..");
					if(GUI.Button(new Rect(60,320,50,20), "goto"))
					{
						//Move Camera to Patient GameObject
						Vector3 translateTo = new Vector3(float.Parse(firstPatientSelection["PositionX"].ToString()) - UnityEngine.Camera.main.transform.position.x, 
						                                  float.Parse(firstPatientSelection["PositionY"].ToString()) - UnityEngine.Camera.main.transform.position.y,
						                                  (float.Parse(firstPatientSelection["PositionZ"].ToString()) - UnityEngine.Camera.main.transform.position.z/2)+0.90f);
						UnityEngine.Camera.main.transform.Translate(translateTo);
					}
				}
			}
		}

		//Toggle Second Selection
		String labelTextSecond = "Select 2nd"; 
		if(secondPatientSelection["ID"].ToString()!="")
		{
			labelTextSecond = secondPatientSelection["ID"].ToString();
			GUI.color = guiColor;
		}
		else GUI.color = Color.clear;
		if(GUI.Button(new Rect(20,70,80,20), labelTextSecond)) 
		{
			if (bool.Parse(secondPatientSelection["CurrentlyBeingViewed"].ToString())) 
				secondPatientSelection["CurrentlyBeingViewed"]=false;
			else 
			{
				if (secondPatientSelection["ID"].ToString()!="") 
				{
					secondPatientSelection["CurrentlyBeingViewed"]=true;
				}
			}
		}

		if (bool.Parse(secondPatientSelection["CurrentlyBeingViewed"].ToString())) 
		{
			GUI.color = guiBeingViewed;
			foreach (Patient patient in extractedPatientDataSet) 
			{
				if (patient.ID.Equals(secondPatientSelection["ID"].ToString())) 
				{
					GUI.Label (new Rect(20,350,260,20), "ID# "+patient.ID+", a(n) "+patient.Ethnicity.ToString()+" "+patient.Gender.ToString());
					GUI.Label (new Rect(20,370,120,20), patient.Initial_WBCx109+" WBCx109");
					GUI.Label (new Rect(20,390,120,20), patient.HasSurvived?"survived":"did not survive");
					GUI.Label (new Rect(20,410,120,20), patient.HasRelapsed?"relapsed":"did not relapse");
					GUI.Label (new Rect(20,430,260,20), "chrom "+patient.Chrom_Structure);
					GUI.Label (new Rect(20,450,120,20), "cluster "+patient.Cluster+", E2A "+patient.E2A_PBX1);
					GUI.Label (new Rect(20,470,800,20), "cyto "+patient.Cytogenics);
					GUI.Label (new Rect(20,490,240,20), "born "+patient.Date_of_Birth+", diagnosed "+patient.Date_of_Diagnosis);
					GUI.Label (new Rect(20,510,260,20), "splenomegaly "+patient.Splenomegaly);
					GUI.Button(new Rect(20,530,30,20), "..");
					if(GUI.Button(new Rect(60,530,50,20), "goto"))
					{
						//Move Camera to Patient GameObject
						Vector3 translateTo = new Vector3(float.Parse(secondPatientSelection["PositionX"].ToString()) - UnityEngine.Camera.main.transform.position.x, 
						                                  float.Parse(secondPatientSelection["PositionY"].ToString()) - UnityEngine.Camera.main.transform.position.y,
						                                  (float.Parse(secondPatientSelection["PositionZ"].ToString()) - UnityEngine.Camera.main.transform.position.z/2)+0.90f);
						UnityEngine.Camera.main.transform.Translate(translateTo);
					}
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		//Set Initial Hashtable Values and Keys
		firstPatientSelection = new Hashtable();
		firstPatientSelection.Add("CurrentlyBeingViewed", false);
		firstPatientSelection.Add("ID", "");
		firstPatientSelection.Add("PositionX", "");
		firstPatientSelection.Add("PositionY", "");
		firstPatientSelection.Add("PositionZ", "");

		secondPatientSelection = new Hashtable();
		secondPatientSelection.Add("CurrentlyBeingViewed", false);
		secondPatientSelection.Add("ID", "");
		secondPatientSelection.Add("PositionX", "");
		secondPatientSelection.Add("PositionY", "");
		secondPatientSelection.Add("PositionZ", "");

		//Declare connection
		dbConnection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=patientsBioData;UID=root;");

		//Start the connection
		dbConnection.Open ();

		//Pull up the command to get all patients
		MySqlCommand dbCommand = new MySqlCommand("SELECT * FROM processedPatientInfo", dbConnection);

		//Execute the command
		MySqlDataReader dataReader = dbCommand.ExecuteReader();
		extractedPatientDataSet = GetPatientsListFromData (dataReader);

		foreach (Patient patient in extractedPatientDataSet) 
		{	
			Vector3 coordinateLocation = new Vector3 (patient.Position_X, patient.Position_Y, patient.Position_Z);
			GameObject coordinateCubeObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			coordinateCubeObject.transform.position = coordinateLocation;
			
			if ( patient.Gender==GenderType.male )
				coordinateCubeObject.renderer.material.color = Color.red; //light (0, 255, 0, 0.9F);
			else
				coordinateCubeObject.renderer.material.color = Color.green; //light (255, 0, 0, 0.9F);

			//set the GameObject name to patient ID
			coordinateCubeObject.name = patient.ID;
			// half size
			coordinateCubeObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			//coordinateCubeObject.AddComponent("PatientComponent");
			PatientComponent pC = coordinateCubeObject.AddComponent<PatientComponent>();
			pC.SelectedPatientID = patient.ID;
		}

		//Close the connection
		dbConnection.Close ();
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
	void Update () 
	{
		// selecting first patient via left click
		if (Input.GetMouseButton(0))
		{
			AssignPatientSelection(0);
		}

		// selecting second patient via right click
		if (Input.GetMouseButton(1))
		{
			AssignPatientSelection(1);
		}
	}
}