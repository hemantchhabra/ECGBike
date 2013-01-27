using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width*0.5f - 48, Screen.height * 0.5f + 96, 96, 48), "Start!")) { 
			Application.LoadLevel(1); 	
		}
	}
}
