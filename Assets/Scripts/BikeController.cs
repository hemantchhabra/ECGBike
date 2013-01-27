using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour 
{
	
	private bool exploded = false;
	private Rigidbody _body = null; 
	private Health bikerhealth = null;
	private Score bikerscore = null;
	private Flips bikerflips = null;
	private float boostcooldown = 0.0f;
	private int timer = 0;
	private int forward_id = 0;
	private int back_id = 0;
	private int air_id = 0;
	private int ground_id = 0;
	public float torqueStrength = 22f; 
	public tk2dSprite sprite = null;
	public ParticleSystem Explode = null;
	public GUISkin Skin = null;
	public int SpeedBoosts = 3;
	public int MaxSpeedBoosts = 5;
	public Vector3 BoostForce = new Vector3( 5, 0, 0 );
	public float BoostCooldownSeconds = 5.0f;
	
	public void GrabSpeedBoost () {
		++SpeedBoosts;
		if ( SpeedBoosts > MaxSpeedBoosts ) {
			SpeedBoosts = MaxSpeedBoosts;
		}
	}
	
	public bool UseSpeedBoost () {
		if (SpeedBoosts > 0) {
			--SpeedBoosts;
			if ( _body != null )
				_body.AddForce( BoostForce, ForceMode.Impulse );
			return true;
		}
		return false;
	}
	
	// Use this for initialization
	void Start () 
	{
		_body = GetComponent<Rigidbody>(); 
		//bikerhealth = transform.Find( "bodyHealthTrigger" ).GetComponent<Health>();
		bikerhealth = GetComponentInChildren<Health>(); 
		bikerscore = GetComponent<Score>();
		bikerflips = GetComponent<Flips>();
		air_id = sprite.GetSpriteIdByName("big_air");
		ground_id = sprite.GetSpriteIdByName("bike_rider");
		forward_id = sprite.GetSpriteIdByName("lean_forward_bike");
		back_id = sprite.GetSpriteIdByName("lean_back_bike");
		timer = -5;
		exploded = false;
#if UNITY_IPHONE
		torqueStrength *= 1.5f; 	
#endif
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		/*if (bikerhealth.IsDead) { 
			Time.timeScale = 0; 
		} 	
		else { 
			Time.timeScale = 1; 	
		}*/
		
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
			
			if (boostcooldown > 0.0f) {
				boostcooldown -= Time.deltaTime;	
			}
			else if (boostcooldown <= 0.0f) {
				if (Input.GetKey (KeyCode.Space) && UseSpeedBoost()) {
					boostcooldown = BoostCooldownSeconds;		
				}
			}
		}
		else { 
			_body.AddTorque(Vector3.zero);
			if ( sprite.spriteId != air_id ) {
				sprite.spriteId = ground_id;
			}
		}
		timer -= 1;
	}
	
	
	void OnGUI() 
	{
		GUI.skin = Skin;
		if (bikerhealth.IsDead) {
			if ( !exploded && _body.velocity.magnitude < 0.1f ) {
				exploded = true;
				Instantiate( Explode, transform.position, transform.rotation );
			}
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
    		GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

    		GUILayout.Box( string.Format("You completed {0} {1} flips and scored {2} points!", 
				bikerflips._flips,
				bikerflips._flips > 60 ? "FLIPPALICIOUS " : 
				bikerflips._flips > 43 ? "RADICAL " : 
				bikerflips._flips > 30 ? "gnarly " : 
				bikerflips._flips > 20 ? "boss " : 
				bikerflips._flips > 17 ? "crazy " : 
				bikerflips._flips > 15 ? "awesome " : 
				bikerflips._flips > 12 ? "amazing " : 
				bikerflips._flips > 5 ? "sweet " :
				bikerflips._flips > 2 ? "nice " : "",
				bikerscore.CurrentScore.ToString ("F1") ) );
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			//if (GUI.Button(new Rect(Screen.width * 0.5f - 230, Screen.height * 0.5f - 80, 96, 48), "Reset")) {
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Try again?")) {
				Application.LoadLevel(Application.loadedLevel); 	
			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
    		GUILayout.FlexibleSpace();
    		GUILayout.EndArea();
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
		
		GUI.Label( new Rect(Screen.width - 300, 10, 230, 80),
			string.Format(
			"{0} Points\n{1} {2}Flip{3}\nBest Jump: {4} {5}Flip{6}", 
			bikerscore.CurrentScore.ToString ("F1"),
			bikerflips._flips, 
			bikerflips._flips > 60 ? "FLIPPALICIOUS " : 
			bikerflips._flips > 43 ? "RADICAL " : 
			bikerflips._flips > 30 ? "Gnarly " : 
			bikerflips._flips > 20 ? "Boss " : 
			bikerflips._flips > 17 ? "Crazy " : 
			bikerflips._flips > 15 ? "Awesome " : 
			bikerflips._flips > 12 ? "Amazing " : 
			bikerflips._flips > 5 ? "Sweet " :
			bikerflips._flips > 2 ? "Nice " : "",
			bikerflips._flips == 0 || bikerflips._flips > 1 ? "s" : "",
			
			bikerflips._flipcombo, 
			bikerflips._flipcombo > 60 ? "FLIPPALICIOUS " : 
			bikerflips._flipcombo > 43 ? "RADICAL " : 
			bikerflips._flipcombo > 30 ? "Gnarly " : 
			bikerflips._flipcombo > 20 ? "Boss " : 
			bikerflips._flipcombo > 17 ? "Crazy " : 
			bikerflips._flipcombo > 15 ? "Awesome " : 
			bikerflips._flipcombo > 12 ? "Amazing " : 
			bikerflips._flipcombo > 5 ? "Sweet " :
			bikerflips._flipcombo > 2 ? "Nice " : "",
			bikerflips._flipcombo == 0 || bikerflips._flipcombo > 1 ? "s" : "" )
		);
	}
}
