using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {
	
	public float motorTorque = 22f; 
	
	private WheelCollider _wheel; 
	
	// Use this for initialization
	void Start () {
		_wheel = GetComponent<WheelCollider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool touchRight = false; 
		bool touchLeft = false;
		
		touchRight = Input.GetKey(KeyCode.RightArrow); 
		touchLeft = Input.GetKey(KeyCode.LeftArrow); 
		
		
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
}
