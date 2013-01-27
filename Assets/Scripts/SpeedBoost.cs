using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Player collides with this powerup
	void OnTriggerEnter(Collider other) {
		BikeController bike = other.attachedRigidbody.gameObject.GetComponent<BikeController>( );
		if ( bike != null ) {
			bike.GrabSpeedBoost();
		}
		Destroy(gameObject);
	}
}
