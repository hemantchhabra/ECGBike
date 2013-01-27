using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class BlockGenerator : MonoBehaviour {
	
	public GameObject basicCube; 
	public GameObject speedBoost;
	public GameObject player; 
	
	public int numBlocks = 10; 
	
	public float minLength = 3; 
	public float maxLength = 10;
	
	public float straitLength = 20; 
	
	public float minRotation = -60; 
	public float maxRotation = 60; 
	
	public float cubeHeight = 0.5f; 
	
	public float totalHeartbeatTime = 2f; 
	
	private float _heartbeatTime; 
	private bool _doingHeartbeat = false; 
	private bool _endingHeartbeat = false; 
	
	private List<BlockAgent> _currentBlocks;
	
	
	private float _currentX, _currentY, _previousAngle; 
	
	
	// Use this for initialization
	void Start () {
		_currentX = -1; 
		_currentY = -1; 
		_previousAngle = 0; 
		
		_currentBlocks = new List<BlockAgent>(); 
		
		
		generateStrait();
		generateHeartbeat();
		generateStrait();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		// Figure out where the player is and see if we need to generate more blocks. 
		if (player.transform.position.x >= _currentX - 10) { 
			generateHeartbeat(); 
			generateStrait();
		}
	}
	
	
	
	
	
	
	private void startHeartbeat() 
	{
		// Find a target angle for all of the blocks
		foreach (BlockAgent block in _currentBlocks) { 
			block.oldAngle = block.transform.rotation.eulerAngles.z;
			
			block.targetAngle = Random.value * (maxRotation - minRotation) + minRotation; 	
		}
		
		_heartbeatTime = 0; 
		_doingHeartbeat = true; 
		_endingHeartbeat = false;
	}
	
	private void updateHeartbeat() 
	{	
		
		_heartbeatTime += Time.deltaTime; 
		if (_heartbeatTime >= totalHeartbeatTime) { 
			_heartbeatTime = totalHeartbeatTime; 
			_doingHeartbeat = false;
		}	
		
		float currentX = 0; 
		float currentY = 0; 
		float previousAngle = 0; 
		
		for (int i = 0; i < _currentBlocks.Count; i++) { 
			BlockAgent block = _currentBlocks[i];
			// First, update the angle of the block 
			float blockLength = block.transform.localScale.x; 
			float currentAngle = Mathf.LerpAngle(block.oldAngle, block.targetAngle, _heartbeatTime/totalHeartbeatTime); 
			//float currentAngle = block.oldAngle + (_heartbeatTime / totalHeartbeatTime) * (block.targetAngle - block.oldAngle); 
			block.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle)); 
			// If we're not the first block, have to change our x and y coordinates as well 
			if (i != 0) { 
				float cubeX = currentX + blockLength / 2 * Mathf.Cos(currentAngle*Mathf.Deg2Rad); 
				float cubeY = currentY + blockLength / 2 * Mathf.Sin(currentAngle*Mathf.Deg2Rad); 
				
				// figure out how much we have to correct based on the relative angles. 
				
				Vector2 prev = (new Vector2(Mathf.Cos(previousAngle*Mathf.Deg2Rad), Mathf.Sin(previousAngle*Mathf.Deg2Rad)))*cubeHeight; 
				Vector2 cur = (new Vector2(Mathf.Cos(currentAngle*Mathf.Deg2Rad), Mathf.Sin(currentAngle*Mathf.Deg2Rad)))*cubeHeight; 
				
				float extraDistance = Vector2.Distance(prev, cur); 
				cubeX -= extraDistance/2; 
				
				block.transform.position = new Vector3(cubeX, cubeY, 0);
				currentX = cubeX + blockLength/2 * Mathf.Cos(currentAngle*Mathf.Deg2Rad);
				currentY = cubeY + blockLength/2 * Mathf.Sin(currentAngle*Mathf.Deg2Rad);
			}
			else { 
				currentX = block.transform.position.x + blockLength/2 * Mathf.Cos(currentAngle*Mathf.Deg2Rad); 
				currentY = block.transform.position.y + blockLength/2 * Mathf.Sin(currentAngle*Mathf.Deg2Rad);
			}
			previousAngle = currentAngle; 
		}
		
		
		
	}
	
	private void generateStrait() 
	{ 
		// Make a single straight block of constant length 
		GameObject block = GameObject.Instantiate(basicCube)  as GameObject; 
		block.transform.localScale = new Vector3(straitLength, cubeHeight, 1);
		
		float cubeX = _currentX + straitLength/2; 
		float cubeY = _currentY; 
		float currentAngle = 0; 
		Vector2 prev, cur; 
		if (currentAngle < _previousAngle) {
			prev = (new Vector2(-Mathf.Sin(_previousAngle*Mathf.Deg2Rad), Mathf.Cos(_previousAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
			cur = (new Vector2(-Mathf.Sin(currentAngle*Mathf.Deg2Rad), Mathf.Cos(currentAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
		}
		else { 
			prev = (new Vector2(Mathf.Sin(_previousAngle*Mathf.Deg2Rad), Mathf.Cos(_previousAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
			cur = (new Vector2(Mathf.Sin(currentAngle*Mathf.Deg2Rad), Mathf.Cos(currentAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
		}	
		Vector2 dis = (cur - prev); 
		cubeX -= dis.x; 
		cubeY -= dis.y; 
		
		block.transform.position = new Vector3(cubeX, cubeY, 0f); 
		
		_currentX = cubeX + straitLength/2; 
		_currentY = cubeY; 
		_previousAngle = 0; 
	}
	
	private void generateHeartbeat() { 
		for (int i = 0; i < numBlocks; i++) { 
			GameObject cube = GameObject.Instantiate(basicCube) as GameObject; 
			cube.transform.parent = transform; 
			
			//float newMaxAngle = Mathf.Min(maxRotation, _previousAngle + 30); 
			//float newMinAngle = Mathf.Max(minRotation, _previousAngle - 30); 
			float newMaxAngle = maxRotation; 
			float newMinAngle = minRotation;
			
			float blockLength = Random.value * (maxLength - minLength) + minLength; 
			float currentAngle = Random.value * (newMaxAngle - newMinAngle) + newMinAngle;
			float cubeX = _currentX + blockLength / 2 * Mathf.Cos(currentAngle*Mathf.Deg2Rad); 
			float cubeY = _currentY + blockLength / 2 * Mathf.Sin(currentAngle*Mathf.Deg2Rad); 
			
			
			// figure out how much we have to correct based on the relative angles. 
			if (currentAngle < _previousAngle) { 
				Vector2 prev = (new Vector2(-Mathf.Sin(_previousAngle*Mathf.Deg2Rad), Mathf.Cos(_previousAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
				Vector2 cur = (new Vector2(-Mathf.Sin(currentAngle*Mathf.Deg2Rad), Mathf.Cos(currentAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
				Vector2 dis = (cur - prev); 
				cubeX -= dis.x; 
				cubeY -= dis.y; 
			}
			else { 
				Vector2 prev = (new Vector2(Mathf.Sin(_previousAngle*Mathf.Deg2Rad), Mathf.Cos(_previousAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
				Vector2 cur = (new Vector2(Mathf.Sin(currentAngle*Mathf.Deg2Rad), Mathf.Cos(currentAngle*Mathf.Deg2Rad)))*cubeHeight/2; 
				Vector2 dis = (cur - prev); 
				cubeX -= dis.x; 
				cubeY -= dis.y;	
			}
 
			cube.transform.localScale = new Vector3(blockLength, cubeHeight, 1);
			cube.transform.position = new Vector3(cubeX, cubeY, 0); 
			cube.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle)); 
	
			
			_currentX = cubeX + blockLength/2 * Mathf.Cos(currentAngle*Mathf.Deg2Rad);
			_currentY = cubeY + blockLength/2 * Mathf.Sin(currentAngle*Mathf.Deg2Rad);
			
			_previousAngle = currentAngle;
		}
	}
	
	
	private void generateBlocks() 
	{ 
		
		for (int i = 0; i < numBlocks; i++) { 
			GameObject cube = GameObject.Instantiate(basicCube) as GameObject; 
			cube.transform.parent = transform; 

			float cubeLength = Random.value * (maxLength - minLength) + minLength; 
			float cubeX = _currentX + cubeLength / 2; 
			float cubeY = _currentY; 
			
 
			cube.transform.localScale = new Vector3(cubeLength, cubeHeight, 1);
			cube.transform.position = new Vector3(cubeX, cubeY, 0);
			_currentX = cubeX + cubeLength/2;
			_currentY = cubeY;
			
			_currentBlocks.Add(cube.GetComponent<BlockAgent>());
		} 
	}
	
}
