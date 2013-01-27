using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {

	int instrHeight = 24;
	int instrWidth1 = 260;
	int instrWidth2 = 330;
	string clockwiseControl;
	string counterClockwiseControl;
#if UNITY_IPHONE
	clockwiseControl = "HERE";
	counterClockwiseControl = "HERE";
#else
	clockwiseControl = "A";
	counterClockwiseControl = "D";
#endif
		
	GUI.Box(new Rect(0,Screen.height-instrHeight,instrWidth1,instrHeight),"PRESS "+clockwiseControl+" TO ROTATE CLOCKWISE");
	GUI.Box(new Rect(Screen.width-instrWidth2,Screen.height-instrHeight,instrWidth2,instrHeight),"PRESS "+counterClockwiseControl+" TO ROTATE COUNTER-CLOCKWISE");
		
		if (GUI.Button(new Rect(Screen.width*0.5f - 48, Screen.height * 0.5f + 96, 96, 48), "Start!")) { 
			Application.LoadLevel(1); 	
		}
	}
}
