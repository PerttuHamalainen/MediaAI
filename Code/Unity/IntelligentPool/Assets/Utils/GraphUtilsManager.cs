using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AaltoGames;

public class GraphUtilsManager : MonoBehaviour {
    public Material materialZTestOff;
    public Material materialZTestOn;

	// Use this for initialization
	void Start () {
        GraphUtils.setMaterials(materialZTestOff, materialZTestOn);
	}
	
	// Update is called once per frame
	void OnGUI () {
        GraphUtils.DrawPendingLines();		
	}
}
