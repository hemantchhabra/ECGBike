using UnityEngine;
using System.Collections;

public class BlockAgent : MonoBehaviour {
	
	public float targetAngle; 
	public float oldAngle; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float screenOffset = Camera.main.transform.position.x - transform.position.x; 
		if (screenOffset > 20) 
			Destroy(gameObject);
	}
}
