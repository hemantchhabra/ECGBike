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
	public long CurrentScore = 0;
	public GameObject Player = null;
	private int air_id = 0;
	private int ground_id = 0;
	WheelCollider left = null, right = null;
	BoxCollider body = null;
	TrickMode trick = TrickMode.None;
	bool bodyisgrounded = false;
	int airtime = 0;
	int wheelietime = 0;
	int scoretime = 0;
	int lastscoretime = 0;
	
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
	}
	
	// Use this for initialization
	void Start () {
		var wheels = Player.GetComponentsInChildren<WheelCollider>();
		left = wheels[0];
		right = wheels[1];
		body = Player.GetComponent<BoxCollider>();
		air_id = sprite.GetSpriteIdByName("big_air");
		ground_id = sprite.GetSpriteIdByName("bike_rider");
	}
	
	// Update is called once per frame
	void Update () {
		long scoreboost = 0;
		if (left != null && right != null) {
			bool leftgrounded = left.isGrounded;
			bool rightgrounded = right.isGrounded;
			trick = !rightgrounded ? (trick | TrickMode.FrontWheelUp) : (trick & ~TrickMode.FrontWheelUp);
			trick = !leftgrounded ? (trick | TrickMode.BackWheelUp) : (trick & ~TrickMode.BackWheelUp);
			if ( trick == TrickMode.AllWheelsUp ) {
				airtime++;
				if ( airtime > 10 ) {
					scoreboost += airtime;
				}
				sprite.spriteId = air_id;
			}
			else if ( trick == TrickMode.None ) {
				airtime = 0;
				wheelietime = 0;
				sprite.spriteId = ground_id;
			}
			else {
				++wheelietime;
				if (wheelietime > 10) {
					scoreboost += wheelietime / 3;
				}
				sprite.spriteId = ground_id;
			}
		}
		else {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;	
			trick = TrickMode.None;
			sprite.spriteId = ground_id;
		}
		if ( bodyisgrounded ) {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;
			sprite.spriteId = ground_id;
		}
		else if (Player.rigidbody.velocity.magnitude < 5) {
			airtime = 0;
			wheelietime = 0;
			scoreboost = 0;	
			sprite.spriteId = ground_id;
		}
		scoretime += scoreboost != 0 ? 1 : 0;
		CurrentScore += scoreboost;
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
		GUI.Label( new Rect(Screen.width - 100, 10, 100, 20), CurrentScore.ToString () );
		lastscoretime = scoretime;
	}
}
