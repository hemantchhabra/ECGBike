using UnityEngine;
using System.Collections;

public class Flips : MonoBehaviour {
	
	public AudioClip[] flipSounds; 
	
	private Rigidbody _body; 
	
	private int _numCollisions; 
	
	// The quadrants we use to tell if we've had a flip
	private bool _firstQuad = false, _secondQuad = false, _thirdQuad = false, _fourthQuad = false; 
	
	public int _flips = 0; 
	public int _currflips = 0;
	public int _flipcombo = 0;
	
	private Collider _bodyCol; 
	private Health _bodyHealth; 
	
	private AudioSource _audioSource; 
	
	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody>(); 
		_numCollisions = 0; 
		_firstQuad = false; 
		_secondQuad = false; 
		_thirdQuad = false; 
		_fourthQuad = false; 
		_flips = 0; 
		
		_bodyCol = transform.FindChild("body").GetComponent<Collider>(); 
		_bodyHealth = GetComponentInChildren<Health>(); 
		
		_audioSource = GetComponent<AudioSource>();

		_currflips = 0;
		_flipcombo = 0;
	}
	
	void OnCollisionEnter(Collision col) { 
		_numCollisions++; 
		// See if one of the contacts was our head 
		foreach (ContactPoint contact in col.contacts) { 
			if (contact.thisCollider == _bodyCol) { 
				_bodyHealth.CurrentHealth -= col.impactForceSum.magnitude;
			} 
		} 
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
		
		if (_bodyHealth.CurrentHealth <= 0) { 
			_audioSource.Stop();	
		} 
		
	
		// If we've seen all the quads, it was a flip 
		if (_firstQuad && _secondQuad && _thirdQuad && _fourthQuad) { 
			_flips++; 
			// Play a sound!
			if (flipSounds.Length > 0) { 
				AudioClip clip = flipSounds[Mathf.FloorToInt(Random.value*flipSounds.Length)]; 
				AudioSource.PlayClipAtPoint(clip, Vector3.zero);
			} 
			
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
