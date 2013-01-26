using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int MaxHealth = 200;
	int currentHealth;
	
	// Use this for initialization
	void Start () {
		currentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter ( Collider c ) {
		currentHealth -= 10;
		Debug.Log(
			string.Format( "Current Health Level is {0}", 
			currentHealth ));
	}
}
