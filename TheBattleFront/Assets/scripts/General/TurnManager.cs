using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public enum Turn{PLAYER, ENEMY};
    public Turn whosTurn;
    public GameObject playerChampion;
    public GameObject enemyChampion;
    public GameObject attackButton;
    public GameObject recruitButton;
    public PlayerChampionStateMachine champStateMachine;
    public EnemyChampionStateMachine eStateMachine;
    public BaseController playerBase;
    public BaseController enemyBase;
    public MoveAction moveAction;
    public PrefabScript prefab;
    public Grid grid;
    public bool isShowingRecruitOptions;
    private Text p1BaseHealthText;
    private Text p2BaseHealthText;
    private GameObject actionPanel;
    private GameObject panel;
    private GameObject cancelPanel;
    private idCardManager idManager;
    private SoldierManager soldierManager;
    private CameraManager cameraManager;

    void Awake()
    {
        actionPanel = GameObject.Find("actionPanel");
        panel = GameObject.Find("Panel");
        cancelPanel = GameObject.Find("cancelPanel");
        moveAction = GameObject.Find("actionPanel").GetComponent<MoveAction>();
        prefab = GameObject.Find("prefabInstantiator").GetComponent<PrefabScript>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        attackButton = GameObject.Find("AttackButton");
        recruitButton = GameObject.Find("Recruit");
        p1BaseHealthText = GameObject.Find("base1Health").GetComponent<Text>();
        p2BaseHealthText = GameObject.Find("base2Health").GetComponent<Text>();
        idManager = GameObject.Find("soldierId").GetComponent<idCardManager>();
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    void Start()
    {
        actionPanel.SetActive(true);
        panel.SetActive(false);
        cancelPanel.SetActive(false);
        whosTurn = Turn.PLAYER;
        initializePlayerField();
        initializeEnemyField();
        moveAction.newTurn();
        GameObject.Find("actionPanel").GetComponent<Attack>().handleActiveSoldierSwitched();
        Debug.Log("Game Start. It is " + whosTurn + " turn");
        cameraManager.startGame();
    }

    void initializePlayerField()
    {
        playerChampion = GameObject.FindGameObjectWithTag("playerChampion");
        if (playerChampion != null)
        {
            Debug.Log("There is 1 player champion on the field");
            champStateMachine = playerChampion.GetComponent<PlayerChampionStateMachine>();
            champStateMachine.init();
            cameraManager.createStartingSoldierCamera(playerChampion);
            soldierManager.addPlayerChamp(playerChampion);
            champStateMachine.beginTurn();
            grid.addSoldierToGrid(playerChampion.transform.position, playerChampion);
            idManager.changeImage(playerChampion.GetComponent<AbstractSoldier>());
        }
        playerBase = GameObject.Find("Base1").GetComponent<BaseController>();
        playerBase.initBase("player", 2);
    }

    public void initializeEnemyField()
    {
        enemyChampion = GameObject.FindGameObjectWithTag("enemyChampion");
        
        if (enemyChampion != null)
        {
            Debug.Log("There is 1 enemy champion on the field");
            eStateMachine = enemyChampion.GetComponent<EnemyChampionStateMachine>();
            eStateMachine.init();
            cameraManager.createStartingSoldierCamera(enemyChampion);
            soldierManager.addEnemyChamp(enemyChampion);
            grid.addSoldierToGrid(enemyChampion.transform.position, enemyChampion);
        }
        enemyBase = GameObject.Find("Base2").GetComponent<BaseController>();
        enemyBase.initBase("enemy", 2);
    }

    public void switchTurns()
    {
        
        switch (whosTurn)
        {
			case (Turn.PLAYER):
				whosTurn = Turn.ENEMY;
				soldierManager.updateCurrentSoliderList ("enemy");
                break;
			case (Turn.ENEMY):
				whosTurn = Turn.PLAYER;
				soldierManager.updateCurrentSoliderList ("player");
                break;
        }
        isShowingRecruitOptions = false;
        if (soldierManager.currentSoldiers.Count > 0)
        {
            attackButton.GetComponent<Button>().interactable = true;
        }
        GameObject.Find("actionPanel").GetComponent<Attack>().resetButton();
        moveAction.newTurn();
    }

    private void endPlayerTurn() { }

    private void beginPlayerTurn() { }

    private void endEnemyTurn()
    {

    }

    private void beginEnemyTurn()
    {

    }

    private void Update()
    {
        updateBaseHealthText();
        activateDeactivateRecruit();
    }

    private void activateDeactivateRecruit()
    {
        if(soldierManager.getCurrentSoldiers().Count == 5)
        {
			recruitButton.GetComponent<Button> ().interactable = false;
        }
        else
        {
			recruitButton.GetComponent<Button> ().interactable = true;
        }
    }

    private void updateBaseHealthText()
    {
        int p1BaseHealth = 0;
        int p2BaseHealth = 0;
        if (GameObject.Find("Base1") != null)
        {
            p1BaseHealth = GameObject.Find("Base1").GetComponent<AbstractSoldier>().getCurrentHealth();
            if(p1BaseHealth < 1)
            {
                SceneManager.LoadScene("gameOverScreen");
            }
        }
        if(GameObject.Find("Base2") != null)
        {
            p2BaseHealth = GameObject.Find("Base2").GetComponent<AbstractSoldier>().getCurrentHealth();
            if (p2BaseHealth < 1)
            {
                SceneManager.LoadScene("gameOverScreen");
            }
        }
        
        p1BaseHealthText.text = "Player 1 Base Health: " + p1BaseHealth;
        p2BaseHealthText.text = "Player 2 Base Health: " + p2BaseHealth;
    }   
}