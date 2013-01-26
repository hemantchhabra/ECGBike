using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour 
{
	
	private Rigidbody _body; 
	
	public float torqueStrength = 22f; 
	
	// Use this for initialization
	void Start () 
	{
		_body = GetComponent<Rigidbody>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool touchRight = false, touchLeft = false; 
		// Check for tilt inputs 
#if UNITY_IPHONE
		/*if (Input.acceleration.x >= 0.5f) 
			_body.AddTorque(new Vector3(0, 0, -torqueStrength)); 
		else if (Input.acceleration.x <= -0.5f) 
			_body.AddTorque(new Vector3(0, 0, torqueStrength));*/
		foreach (Touch touch in Input.touches) { 
			if (touch.position.x >= 2*Screen.width/3)
				touchRight = true; 
			if (touch.position.x < Screen.width/3)
				touchLeft = true;
		}
#endif
		
		if (Input.GetKey(KeyCode.D) || touchRight) { 
			_body.AddTorque(new Vector3(0, 0, -torqueStrength)); 
		}
		else if (Input.GetKey(KeyCode.A) || touchLeft) { 
			_body.AddTorque(new Vector3(0, 0, torqueStrength)); 	
		}
		else { 
			_body.AddTorque(Vector3.zero);	
		}
	}
}
