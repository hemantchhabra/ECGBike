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
	
	// Use this for initialization
	void Start () {
		currentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter ( Collider c ) {
		float healthkill = 10;
		if ( c != null ) {
			Debug.Log ( transform.parent.rigidbody.velocity );
			healthkill *= transform.parent.rigidbody.velocity.magnitude;
		}
		currentHealth -= healthkill;
		Debug.Log(
			string.Format( "Current Health Level is {0}", 
			currentHealth ));
	}
	
	void OnGUI () {
		float truewidth = ((float)currentHealth / (float)MaxHealth) * healthWidth;
		GUI.DrawTexture( 
			new Rect(healthMarginLeft,healthMarginTop,
			healthMarginLeft + (float)truewidth, healthHeight), 
			HealthTexture, ScaleMode.ScaleAndCrop, true, 0 );

    	GUI.DrawTexture( 
			new Rect(frameMarginLeft,frameMarginTop, 
			frameMarginLeft + frameWidth,frameMarginTop + frameHeight), 
			FrameTexture, ScaleMode.ScaleToFit, true, 0 );
	}
}
