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
	
	public bool IsDead {
		get {
			return currentHealth <= 0;
		}
	}
	
	float currentHealth;
	
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
		float healthkill = 6;
		if ( c != null ) {
			healthkill *= transform.parent.rigidbody.velocity.magnitude;
		}
		currentHealth -= healthkill;
		if ( currentHealth < 0 ) {
			currentHealth = 0;
		}
	}
	
	void OnGUI () {
		float truewidth = ((float)currentHealth / (float)MaxHealth) * healthWidth;
		GUI.DrawTexture( 
			new Rect(healthMarginLeft, healthMarginTop,
			(float)truewidth, healthHeight), 
			HealthTexture, ScaleMode.ScaleAndCrop, true, 0 );

    	GUI.DrawTexture( 
			new Rect(frameMarginLeft, frameMarginTop, 
			frameWidth, frameHeight), 
			FrameTexture, ScaleMode.ScaleToFit, true, 0 );
	}
}
