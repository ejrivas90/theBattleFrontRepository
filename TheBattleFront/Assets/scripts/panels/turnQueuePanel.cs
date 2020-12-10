using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnQueuePanel : MonoBehaviour {
	public Sprite playerChampion;
	public Sprite playerFoot;
	public Sprite playerSnipe;
	public Sprite playerTank;
	public Sprite playerArt;
	public Sprite enemyChampion;
	public Sprite enemyFoot;
	public Sprite enemySnipe;
	public Sprite enemyTank;
	public Sprite enemyArt;
	private GameObject turnListPanel;
	private List<GameObject> queueImages;
	private SoldierManager soldierManager;

	// Use this for initialization
	void Start () {
		turnListPanel = GameObject.Find ("turnListPanel");
		soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
		queueImages = new List<GameObject> ();
		for (int i = 0; i < turnListPanel.transform.childCount; i++) {
			queueImages.Add (turnListPanel.transform.GetChild (i).gameObject);
			queueImages [i].GetComponent<Image> ().color = Color.clear;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateQueueImages ();
	}

	public void updateQueueImages()
	{
		clearSprites ();
		List<GameObject> listOfSoldiers = soldierManager.getCurrentSoldiers ();
		turnListPanel.SetActive (true);
		int imageIndex = 0;
		for (int i = 0; i < listOfSoldiers.Count; i++) 
		{
			AbstractSoldier soldier = listOfSoldiers [i].GetComponent<AbstractSoldier> ();
			string soldierName = soldier.getName();
            if (soldier.getCurrentState ().Equals (AbstractSoldier.TurnState.ACTIVE)
				|| soldier.getCurrentState ().Equals (AbstractSoldier.TurnState.MOVE)
				|| soldier.getCurrentState ().Equals (AbstractSoldier.TurnState.ATTACK)) {
                if(imageIndex > 3)
                {
                    queueImages[imageIndex].GetComponent<Image>().sprite = null;
                }
            } else if (soldier.currentState.Equals (AbstractSoldier.TurnState.WAIT)) {
				queueImages [imageIndex].GetComponent<Image> ().color = Color.white;
                queueImages[imageIndex].GetComponent<queuHoverScript>().originalActiveSoldier = soldierManager.findSoldier("ACTIVE");
                queueImages[imageIndex].GetComponent<queuHoverScript>().setSoldierObject(soldier);
                switch (soldierName) {
				case ("PLAYERInfantry"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = playerFoot;
					break;
				case ("PLAYERMarksman"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = playerSnipe;
					break;
				case ("PLAYERTank"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = playerTank;
					break;
				case ("PLAYERArtillery"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = playerArt;
					break;
				case ("ENEMYInfantry"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = enemyFoot;
					break;
				case ("ENEMYMarksman"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = enemySnipe;
					break;
				case ("ENEMYTank"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = enemyTank;
					break;
				case ("ENEMYArtillery"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = enemyArt;
					break;
				case ("playerChampion"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = playerChampion;
					break;
				case ("enemyChampion"):
					queueImages [imageIndex].GetComponent<Image> ().sprite = enemyChampion;
					break;
				}
			} else {
				queueImages [imageIndex].GetComponent<Image> ().sprite = null;
			}
			if (queueImages [imageIndex].GetComponent<Image> ().sprite != null) {
				imageIndex++;
			}
		}
	}

	public void clearSprites() {
		int i = 0;
		foreach (GameObject obj in queueImages) {
			
			Sprite a = obj.GetComponent<Image> ().sprite;
			if (a != null) {
				string b = a.name;
				obj.GetComponent<Image> ().sprite = null;
				obj.GetComponent<Image> ().color = Color.clear;
			}
			i++;
		}
	}
}
