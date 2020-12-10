using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveSoldier : MonoBehaviour {
    SoldierManager soldierManager;
    

	// Use this for initialization
	void Start () {
		soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
