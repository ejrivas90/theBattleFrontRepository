using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldierCamera : MonoBehaviour {

    public GameObject activeSoldier;
    private float cameraXpos;
    private float cameraYpos;
    private float cameraZpos;

    private void setActiveSoldier(GameObject activeSoldier)
    {
        this.activeSoldier = activeSoldier;
    }

    private GameObject getActiveSoldier()
    {
        return activeSoldier;
    }

    private void changeView(GameObject newSoldier, string whosTurn)
    {
        float xpos = newSoldier.transform.position.x;
        float ypos = newSoldier.transform.position.y;
        float zpos = newSoldier.transform.position.z;


    }
	
	void Update () {
		
	}
}
