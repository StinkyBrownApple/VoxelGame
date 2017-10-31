using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour {

    [SerializeField]
    Texture2D crosshairs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        float xCenter = (Screen.width / 2 - crosshairs.width / 2);
        float yCenter = (Screen.height / 2 - crosshairs.height / 2);

        GUI.DrawTexture(new Rect(xCenter, yCenter, crosshairs.width, crosshairs.height), crosshairs);
    }
}
