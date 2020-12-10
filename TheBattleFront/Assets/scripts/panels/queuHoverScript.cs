using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class queuHoverScript : MonoBehaviour {
    public AbstractSoldier soldierObject;
    public GameObject originalActiveSoldier;
    private SoldierManager soldierManager;
    private CameraManager cameraManager;
    private Button theButton;
    
	void Start () {
		soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        theButton = this.GetComponent<Button>();
    }
	
	void Update () {}
    
    public void acitvateSelectedSoldier()
    {
        Debug.Log(soldierObject.getName());
        originalActiveSoldier.GetComponent<AbstractSoldier>().setCurrentState(AbstractSoldier.TurnState.WAIT);
        soldierObject.beginTurn();
        GameObject.Find("actionPanel").GetComponent<MoveAction>().newTurn();
        GameObject.Find("actionPanel").GetComponent<Attack>().handleActiveSoldierSwitched();
        GameObject.Find("soldierId").GetComponent<idCardManager>().changeImage(soldierObject);
        cameraManager.panToHoveredSoldier(soldierObject);
        originalActiveSoldier = null;
        soldierObject = null;
    }    

    public void setSoldierObject(AbstractSoldier soldierObject)
    {
        this.soldierObject = soldierObject;
    }

    public AbstractSoldier getSoldierObject()
    {
        return soldierObject;
    }
    
}
