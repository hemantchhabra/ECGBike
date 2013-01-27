using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {
	
	public float motorTorque = 22f; 
	
	private WheelCollider _wheel = null; 
	private Health bikerhealth = null;
	private Score bikerscore = null;
	
	// Use this for initialization
	void Start () {
		_wheel = GetComponent<WheelCollider>();
		bikerhealth = transform.parent.Find( "bodyHealthTrigger" ).GetComponent<Health>();
		bikerscore = transform.parent.GetComponent<Score>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool touchRight = false; 
		bool touchLeft = false;
		
		if ( !bikerhealth.IsDead ) {
			touchRight = true;
			
			if (touchRight) { 
				_wheel.motorTorque = motorTorque; 
			}
			else if (touchLeft) { 
				_wheel.motorTorque = -motorTorque; 
			}	
			else { 
				_wheel.motorTorque = 0; 	
			}
		}
		else {
			_wheel.motorTorque = 0;
		}
		
	}
}
