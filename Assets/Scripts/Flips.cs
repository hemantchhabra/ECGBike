using UnityEngine;
using System.Collections;

public class Flips : MonoBehaviour {
	
	private Rigidbody _body; 
	
	private int _numCollisions; 
	
	// The quadrants we use to tell if we've had a flip
	private bool _firstQuad = false, _secondQuad = false, _thirdQuad = false, _fourthQuad = false; 
	
	public int _flips = 0; 
	public int _currflips = 0;
	public int _flipcombo = 0;
	
	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody>(); 
		_numCollisions = 0; 
		_firstQuad = false; 
		_secondQuad = false; 
		_thirdQuad = false; 
		_fourthQuad = false; 
		_flips = 0;
		_currflips = 0;
		_flipcombo = 0;
	}
	
	void OnCollisionEnter(Collision other) { 
		_numCollisions++; 
	}
	
	void OnCollisionExit(Collision other) { 
		_numCollisions--;	
	}
	
	// Update is called once per frame
	void Update () {
		
		float angle = transform.rotation.eulerAngles.z; 
		if (angle >= 0 && angle < 90) 
			_firstQuad = true; 
		if (angle >= 90 && angle < 180) 
			_secondQuad = true; 
		if (angle >= 180 && angle < 270)
			_thirdQuad = true; 
		if (angle >= 270 && angle < 360) 
			_fourthQuad = true; 	
	
		if (_numCollisions > 0) { 
			_firstQuad = false; 
			_secondQuad = false; 
			_thirdQuad = false; 
			_fourthQuad = false;
			_currflips = 0;
		}
		
		// If we've seen all the quads, it was a flip 
		if (_firstQuad && _secondQuad && _thirdQuad && _fourthQuad) { 
			_flips++; 
			_firstQuad = false; 
			_secondQuad = false; 
			_thirdQuad = false; 
			_fourthQuad = false;
			++_currflips;
			if (_currflips > _flipcombo)
				_flipcombo = _currflips;
		}
		
	} 
}
