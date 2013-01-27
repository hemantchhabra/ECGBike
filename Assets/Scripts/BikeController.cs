using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour 
{
	
	private Rigidbody _body = null; 
	private Health bikerhealth = null;
	private Score bikerscore = null;
	private int timer = 0;
	private int forward_id = 0;
	private int back_id = 0;
	private int air_id = 0;
	private int ground_id = 0;
	public float torqueStrength = 22f; 
	public tk2dSprite sprite;
	
	// Use this for initialization
	void Start () 
	{
		_body = GetComponent<Rigidbody>(); 
		//bikerhealth = transform.Find( "bodyHealthTrigger" ).GetComponent<Health>();
		bikerhealth = GetComponentInChildren<Health>(); 
		bikerscore = GetComponent<Score>();
		air_id = sprite.GetSpriteIdByName("big_air");
		ground_id = sprite.GetSpriteIdByName("bike_rider");
		forward_id = sprite.GetSpriteIdByName("lean_forward_bike");
		back_id = sprite.GetSpriteIdByName("lean_back_bike");
		timer = -5;
#if UNITY_IPHONE
		torqueStrength *= 1.5f; 	
#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (bikerhealth.IsDead) { 
			Time.timeScale = 0; 
		} 	
		else { 
			Time.timeScale = 1; 	
		}
		
		bool touchLeft = false, touchRight = false; 
#if UNITY_IPHONE
		foreach (Touch touch in Input.touches) { 
			if (touch.position.x <= Screen.width/3) 
				touchLeft = true; 
			if (touch.position.x >= 2*Screen.width/3)
				touchRight = true; 
		}
		
#endif

		
		// Check for tilt inputs 
		if (!bikerhealth.IsDead) {
			if (Input.GetKey(KeyCode.D) || touchRight) { 
				_body.AddTorque(new Vector3(0, 0, -torqueStrength)); 
				if(sprite.spriteId != air_id) {
					sprite.spriteId = forward_id;
					timer = 45;
				}
			}

			else if (Input.GetKey(KeyCode.A) || touchLeft) { 
				_body.AddTorque(new Vector3(0, 0, torqueStrength)); 
				if(sprite.spriteId != air_id) {
					sprite.spriteId = back_id;
					timer = 45;
				}
			}
			else { 
				_body.AddTorque(Vector3.zero);	
				if(sprite.spriteId != air_id ) {
					sprite.spriteId = ground_id;
					timer = -5;
				}
			}
		}
		else { 
			_body.AddTorque(Vector3.zero);
			if(sprite.spriteId != air_id) {
				sprite.spriteId = ground_id;
			}
		}
		timer -= 1;
	}
	
	
	void OnGUI() 
	{
		if (bikerhealth.IsDead) {
			if (GUI.Button(new Rect(10, 10, 96, 48), "Reset")) { 
				Application.LoadLevel(Application.loadedLevel); 	
			}
		}
		else {
			float truewidth = ((float)bikerhealth.CurrentHealth / (float)bikerhealth.MaxHealth) * bikerhealth.healthWidth;
			GUI.DrawTexture( 
				new Rect(bikerhealth.healthMarginLeft, bikerhealth.healthMarginTop,
				(float)truewidth, bikerhealth.healthHeight), 
				bikerhealth.HealthTexture, ScaleMode.ScaleAndCrop, true, 0 );
	
	    	GUI.DrawTexture( 
				new Rect(bikerhealth.frameMarginLeft, bikerhealth.frameMarginTop, 
				bikerhealth.frameWidth, bikerhealth.frameHeight), 
				bikerhealth.FrameTexture, ScaleMode.ScaleToFit, true, 0 );
		}
	}
}
