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
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)) { 
			_wheel.motorTorque = motorTorque; 
		}
		else if (Input.GetKey(KeyCode.LeftArrow)) { 
			_wheel.motorTorque = -motorTorque; 
		}	
		else { 
			_wheel.motorTorque = 0; 	
		}
		
		
	}
}
