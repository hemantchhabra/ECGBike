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
		// Check for tilt inputs 
#if UNITY_IPHONE
		_body.AddTorque(new Vector3(0, 0, -2*torqueStrength*Input.acceleration.x));
#endif
		
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
}
