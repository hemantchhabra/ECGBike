using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float MaxHealth = 200;
	public Texture FrameTexture = null;
	public Texture HealthTexture = null;
	public int healthMarginLeft = 41;
	public int healthMarginTop = 38;
	public int healthWidth = 200;
	public int healthHeight = 30;
	
	public int frameWidth = 266;
	public int frameHeight = 65;
	public int frameMarginLeft  = 10;
	public int frameMarginTop = 10;
	
	float currentHealth;
	
	public float CurrentHealth {
		get {
			return currentHealth;
		}
		set { 
			currentHealth = value; 	
		}
	}
	
	public bool IsDead {
		get {
			return currentHealth <= 0;
		}
	}
	
	// Use this for initialization
	void Start () {
		currentHealth = MaxHealth;
	}
	
	void Reset () {
		currentHealth = MaxHealth;	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter ( Collider c ) {
		float healthkill = 0;
		if ( c != null ) {
			healthkill = -3;
			if (!c.isTrigger)
				healthkill *= transform.parent.rigidbody.velocity.magnitude;
			else
				healthkill = 0;
		}
		currentHealth += healthkill;
		if ( currentHealth < 0 ) {
			currentHealth = 0;
			
			
		}
	}
}
