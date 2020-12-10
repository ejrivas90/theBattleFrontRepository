using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndTurnButton : MonoBehaviour {
    private TurnManager turnManager;
    private GameObject moveButton;
    private GameObject recruitButton;
    private Text battleOutput;
    private idCardManager idManager;
    private SoldierManager soldierManager;

    private void Awake()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        moveButton = GameObject.Find("Move");
        recruitButton = GameObject.Find("Recruit");
        idManager = GameObject.Find("soldierId").GetComponent<idCardManager>();
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
    }

    void Start() {
        Debug.Log("end turn button script started");
        battleOutput = GameObject.Find("battleOutput").GetComponent<Text>();
    }

    public void endTurnButtonClicked() {
        battleOutput.text = "Battle Output: \n";
        Debug.Log("End Turn button has been clicked");
        Debug.Log("It is currently " + turnManager.whosTurn + " turn");
        turnManager.switchTurns();
		if (soldierManager.getCurrentSoldiers ().Count > 0) {
			moveButton.GetComponent<Button> ().interactable = true;
			idManager.changeImage (soldierManager.getCurrentSoldiers () [0].GetComponent<AbstractSoldier> ());
		} else {
			moveButton.GetComponent<Button> ().interactable = false;
		}
        recruitButton.GetComponent<Button>().interactable = true;
        Debug.Log("After switch, it is " + turnManager.whosTurn + " turn");
    }
    
}
