using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour {
	
	public Vector3 boostForce = new Vector3(10.0f, 0.0f, 0.0f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Player collides with this powerup
	void OnTriggerEnter(Collider other) {
		other.attachedRigidbody.AddForce(boostForce, ForceMode.Impulse);
		Destroy(gameObject);
	}
}
