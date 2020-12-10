using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class idCardManager : MonoBehaviour {
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
	public Text soldierType;
    public Text health;
    public Text atkDie;
    public Text atkRange;


    private void Start()
    {
		soldierType = GameObject.Find("soldierName").GetComponent<Text>();
        health = GameObject.Find("health").GetComponent<Text>();
        atkDie = GameObject.Find("atkDie").GetComponent<Text>();
        atkRange = GameObject.Find("atkRange").GetComponent<Text>();
    }

    public void changeImage(AbstractSoldier soldier)
    {
        string soldierName = soldier.getName();
		soldierType.text = soldier.getSoldierType ();
        switch (soldierName)
        {
            case ("PLAYERInfantry"):
                this.gameObject.GetComponent<Image>().sprite = playerFoot;
                break;
            case ("PLAYERMarksman"):
                this.gameObject.GetComponent<Image>().sprite = playerSnipe;
                break;
            case ("PLAYERTank"):
                this.gameObject.GetComponent<Image>().sprite = playerTank;
                break;
            case ("PLAYERArtillery"):
                this.gameObject.GetComponent<Image>().sprite = playerArt;
                break;
            case ("ENEMYInfantry"):
                this.gameObject.GetComponent<Image>().sprite = enemyFoot;
                break;
            case ("ENEMYMarksman"):
                this.gameObject.GetComponent<Image>().sprite = enemySnipe;
                break;
            case ("ENEMYTank"):
                this.gameObject.GetComponent<Image>().sprite = enemyTank;
                break;
            case ("ENEMYArtillery"):
                this.gameObject.GetComponent<Image>().sprite = enemyArt;
                break;
            case ("playerChampion"):
                this.gameObject.GetComponent<Image>().sprite = playerChampion;
                break;
            case ("enemyChampion"):
                this.gameObject.GetComponent<Image>().sprite = enemyChampion;
                break;
        }
        health.text = "Health: " + soldier.currentHealth.ToString();
        atkDie.text = "Attack/Defense Die: " +soldier.atkDie.ToString();
        atkRange.text = "Attack Range: " + soldier.atkRange.ToString();
    }
}
