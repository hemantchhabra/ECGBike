using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	public enum TrickMode {
		None = 0,
		FrontWheelUp = 1,
		BackWheelUp = 2,
		AllWheelsUp = 3,
	}
	
	public tk2dSprite sprite;
	public double CurrentScore = 0;
	public GameObject Player = null;
	private Rigidbody _rb = null;
	private int air_id = 0;
	private int ground_id = 0;
	WheelCollider left = null, right = null;
	BoxCollider body = null;
	TrickMode trick = TrickMode.None;
	bool bodyisgrounded = false;
	float airtime = 0;
	float wheelietime = 0;
	float scoretime = 0;
	
	private AudioSource _audioSource; 
	
	private Health _playerHealth; 
	private Flips bikerflips;
	
	public TrickMode Trick {
		get {
			return trick;
		}
	}
	
	public void Reset () {
		CurrentScore = 0;	
		trick = TrickMode.None;
		airtime = 0;
		wheelietime = 0;
		scoretime = 0;
	}
	
	// Use this for initialization
	void Start () {
		var wheels = Player.GetComponentsInChildren<WheelCollider>();
		left = wheels[0];
		right = wheels[1];
		body = Player.GetComponent<BoxCollider>();
		_rb = Player.GetComponent<Rigidbody>();
		air_id = sprite.GetSpriteIdByName("big_air");
		ground_id = sprite.GetSpriteIdByName("bike_rider");
		_playerHealth = GetComponentInChildren<Health>(); 
		bikerflips = GetComponent<Flips>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (_playerHealth.CurrentHealth <= 0) 
			return;
		
		//Reset ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_playerHealth.CurrentHealth <= 0) 
			return;
		
		float scoreboost = 0;
		if (left != null && right != null) {
			bool leftgrounded = left.isGrounded;
			bool rightgrounded = right.isGrounded;
			trick = !rightgrounded ? (trick | TrickMode.FrontWheelUp) : (trick & ~TrickMode.FrontWheelUp);
			trick = !leftgrounded ? (trick | TrickMode.BackWheelUp) : (trick & ~TrickMode.BackWheelUp);
			if ( trick == TrickMode.AllWheelsUp ) {
				airtime += Time.deltaTime;
				if ( airtime > 0.5f ) {
					scoreboost += airtime * ( bikerflips._currflips + 1 );
				}
				//RaycastHit hit = new RaycastHit();
				//Physics.Raycast(new Ray(transform.position, transform.InverseTransformDirection(new Vector3(0, -1, 0))), out hit);
				if (airtime > 1.5f || sprite.spriteId == air_id) {
					sprite.spriteId = air_id;
					scoreboost += airtime * ( bikerflips._currflips + 1 );
				} 
				//sprite.spriteId = air_id;
			}
			else if ( trick == TrickMode.None ) {
				airtime = 0;
				wheelietime = 0;
				if (sprite.spriteId == air_id) {
					sprite.spriteId = ground_id;
				}
			}
			else {
				wheelietime += Time.deltaTime;
				if (wheelietime > 0.4f) {
					scoreboost += wheelietime;
				}
				if (sprite.spriteId == air_id) {
					sprite.spriteId = ground_id;
				}
			}
		}
		else {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;	
			trick = TrickMode.None;
			sprite.spriteId = ground_id;
		}
		Debug.Log (airtime);
		Debug.Log ( "Score is " + scoreboost.ToString () );
		if ( bodyisgrounded ) {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;
			sprite.spriteId = ground_id;
		}
		else if (Player.rigidbody.velocity.magnitude < 0.1f) {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;
			sprite.spriteId = ground_id;
		}
		scoretime += scoreboost != 0 ? Time.deltaTime : 0;
		CurrentScore += scoreboost;
		Debug.Log ( "Score is " + scoreboost.ToString () );
	}
	
	void OnTriggerEnter ( Collider c ) {
		if (!c.isTrigger)
			bodyisgrounded = true;
	}
	
	void OnTriggerExit ( Collider c ) {
		if (!c.isTrigger)
			bodyisgrounded = false;
	}
	
	void OnGUI () {
		
	}
}
