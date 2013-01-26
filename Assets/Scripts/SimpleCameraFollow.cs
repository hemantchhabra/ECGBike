using UnityEngine;
using System.Collections;

public class SimpleCameraFollow : MonoBehaviour {
	
	public Transform playerTarget; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z);
	}
}
