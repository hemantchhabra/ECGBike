using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour 
{
	
	private Rigidbody _body = null; 
	private Health bikerhealth = null;
	private Score bikerscore = null;
	public float torqueStrength = 22f; 
	
	// Use this for initialization
	void Start () 
	{
		_body = GetComponent<Rigidbody>(); 
		bikerhealth = transform.Find( "bodyHealthTrigger" ).GetComponent<Health>();
		bikerscore = GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for tilt inputs 
		if (!bikerhealth.IsDead) {
			if (Input.GetKey(KeyCode.D)) { 
				_body.AddTorque(new Vector3(0, 0, -torqueStrength)); 
			}
			else if (Input.GetKey(KeyCode.A)) { 
				_body.AddTorque(new Vector3(0, 0, torqueStrength)); 	
			}
			else { 
				_body.AddTorque(Vector3.zero);	
			}
		}
		else { 
			_body.AddTorque(Vector3.zero);
		}
	}
	
	
	void OnGUI() 
	{
		if (bikerhealth.IsDead) {
			if (GUI.Button(new Rect(10, 10, 96, 48), "Reset")) { 
				Application.LoadLevel(Application.loadedLevel); 	
			}
		}
		else {
			float truewidth = ((float)bikerhealth.CurrentHealth / (float)bikerhealth.MaxHealth) * bikerhealth.healthWidth;
			GUI.DrawTexture( 
				new Rect(bikerhealth.healthMarginLeft, bikerhealth.healthMarginTop,
				(float)truewidth, bikerhealth.healthHeight), 
				bikerhealth.HealthTexture, ScaleMode.ScaleAndCrop, true, 0 );
	
	    	GUI.DrawTexture( 
				new Rect(bikerhealth.frameMarginLeft, bikerhealth.frameMarginTop, 
				bikerhealth.frameWidth, bikerhealth.frameHeight), 
				bikerhealth.FrameTexture, ScaleMode.ScaleToFit, true, 0 );
		}
	}
}
