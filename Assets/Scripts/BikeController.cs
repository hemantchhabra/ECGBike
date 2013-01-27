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
			_body.AddTorque(new Vector3(0, 0, -2*torqueStrength*Input.acceleration.x));
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
}
