using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour {
    private GameObject attackButton;
    private Button cancelButton;
    private GameObject cancelPanel;
    private GameObject actionPanel;
    private CameraManager cameraManager;
    private Text battleOutput;
    private Grid grid;
    private GameObject attackingSoldier;
    private GameObject defendingSoldier;
    public Dictionary<string, GridObject> listOfOptions;
    private bool attackButtonClicked;
    private Vector3 attackingSoldierPosition;
    private Vector3 attackingSoldierRotation;
    private Vector3 defendingSoldierPosition;
    private Vector3 defendingSoldierRotation;
    private List<GridObject> activeGrid = new List<GridObject>();
    private SoldierManager soldierManager;
    private GameObject canvas;
    public GameObject p1AnimObj;
    public GameObject p2AnimObj;
    private Animator player1Animator;
    private Animator player2Animator;

    private void Awake()
    {
        attackButton = GameObject.Find("AttackButton");
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        battleOutput = GameObject.Find("battleOutput").GetComponent<Text>();
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        cancelButton = GameObject.Find("cancelButton").GetComponent<Button>();
        cancelPanel = GameObject.Find("cancelPanel");
        actionPanel = GameObject.Find("actionPanel");
        canvas = GameObject.Find("Canvas");
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    public void attackAction()
    {
        battleOutput.text = "Battle Output: \n";
        cancelPanel.SetActive(true);
        actionPanel.SetActive(false);
        attackButton.GetComponent<Button>().interactable = false;
        cancelButton.onClick.AddListener(handleMoveCancelled);
        activeGrid.Clear();
        attackingSoldier.GetComponent<AbstractSoldier>().setCurrentState(AbstractSoldier.TurnState.ATTACK);

        if (attackingSoldier != null)
        {
            listOfOptions = grid.showAttackOption(attackingSoldier);
            attackButtonClicked = true;
        }
    }
    
    void Start () {
        player1Animator = p1AnimObj.GetComponent<Animator>();
        player2Animator = p2AnimObj.GetComponent<Animator>();
    }
	
	void Update () {}

    public void handleActiveSoldierSwitched()
    {
        attackingSoldier = soldierManager.findSoldier("ACTIVE");
        attackingSoldierPosition = attackingSoldier.transform.position;
        if(attackingSoldier.GetComponent<AbstractSoldier>().getHasAttacked())
        {
            attackButton.GetComponent<Button>().interactable = false;
        } else
        {
            attackButton.GetComponent<Button>().interactable = true;
        }
    }

    private void handleMoveSelected(GameObject recievingSoldier)
    {
        AbstractSoldier defSoldier = recievingSoldier.GetComponent<AbstractSoldier>();
        AbstractSoldier atkSoldier = attackingSoldier.GetComponent<AbstractSoldier>();
        bool isComplete = false;
        if (defSoldier != null)
        {
            if (defSoldier.getCurrentState().Equals(AbstractSoldier.TurnState.BASE))
            {
                defSoldier.takeDamage();
                Debug.Log("base health: " + defSoldier.getCurrentHealth());
                battleOutput.text = battleOutput.text + "\n Base has taken damage!!!";
            }
            else
            {
                int atkRoll = atkSoldier.atkRoll();
                int defRoll = defSoldier.atkRoll();
                
                //atkRoll = 1;
                
                if (defRoll > atkRoll)
                {
                    isComplete = showAtkRollAnimation(atkRoll);
                    //do nothing
                    //defending soldier dodged attack
                    Debug.Log("defending soldier dodged attack");
                    battleOutput.text = battleOutput.text + "Attacking player rolled a : " + atkRoll;
                    battleOutput.text = battleOutput.text + "\n";
                    battleOutput.text = battleOutput.text + "Defending player rolled a :" + defRoll;
                    battleOutput.text = battleOutput.text + "\n";
                    battleOutput.text = battleOutput.text + "Defending player dodged the attack";
                }
                else if (defRoll == atkRoll)
                {
                    Debug.Log("re-roll");
                    handleMoveSelected(recievingSoldier);
                }
                else
                {
                    isComplete = showAtkRollAnimation(atkRoll);
                    //attack will land, calculate damage
                    Debug.Log("defending soldier current health: " + defSoldier.getCurrentHealth());
                    defSoldier.takeDamage(atkSoldier.getAttackPower());
                    if(defSoldier.getCurrentHealth() < 1 )
                    {
                        soldierManager.removeSoldier(recievingSoldier);
                    }
                    battleOutput.text = battleOutput.text + "Attacking player rolled: " + atkRoll;
                    battleOutput.text = battleOutput.text + "\n";
                    battleOutput.text = battleOutput.text + "Defending player rolled: " + defRoll;
                    battleOutput.text = battleOutput.text + "\n";
                    battleOutput.text = battleOutput.text + "Player landed attack";
                    battleOutput.text = battleOutput.text + "\n";
                    battleOutput.text = battleOutput.text + "Defending soldier's new health: " + defSoldier.currentHealth;
                }
            }
        }
        atkSoldier.setCurrentState(AbstractSoldier.TurnState.ACTIVE);
        resetButton();
        cancelPanel.SetActive(false);
        actionPanel.SetActive(true);
        attackButton.GetComponent<Button>().interactable = false;
        attackingSoldier.GetComponent<AbstractSoldier>().setHasAttacked(true);
    }

    private bool showAtkRollAnimation(int atkRoll)
    {
        bool isFinished = false;

        switch (atkRoll)
        {
            case (1):
                player1Animator.SetTrigger("goTo1");
                AnimatorStateInfo a = player1Animator.GetCurrentAnimatorStateInfo(0);
                player1Animator.SetTrigger("goIdle");
                break;
            case (2):
                player1Animator.SetTrigger("goTo1");
                break;
            case (3):
                player1Animator.SetTrigger("goTo1");
                break;
            case (4):
                player1Animator.SetTrigger("goTo1");
                break;
            case (5):
                player1Animator.SetTrigger("goTo1");
                break;
            case (6):
                player1Animator.SetTrigger("goTo1");
                break;
        }

        return isFinished;
    }

    private bool showDefRollAnimation(int defRoll)
    {
        bool isFinished = false;

        switch (defRoll)
        {
            case (1):
                player2Animator.SetTrigger("goTo1");
                AnimatorStateInfo a = player2Animator.GetCurrentAnimatorStateInfo(0);
                Thread.Sleep(4000);
                player2Animator.SetTrigger("goIdle");
                break;
            case (2):
                player2Animator.SetTrigger("goTo1");
                break;
            case (3):
                player2Animator.SetTrigger("goTo1");
                break;
            case (4):
                player2Animator.SetTrigger("goTo1");
                break;
            case (5):
                player2Animator.SetTrigger("goTo1");
                break;
            case (6):
                player2Animator.SetTrigger("goTo1");
                break;
        }

        return isFinished;
    }

    private void setUpAttackScene(GameObject recievingSoldier)
    {
        defendingSoldierPosition = recievingSoldier.transform.position;
        defendingSoldierRotation = recievingSoldier.transform.eulerAngles;
        attackingSoldierPosition = attackingSoldier.transform.position;
        attackingSoldierRotation = attackingSoldier.transform.eulerAngles;

        attackingSoldier.transform.position = new Vector3(6f, 0f, 90f);
        attackingSoldier.transform.eulerAngles = new Vector3(0f, 90f, 0f);
        recievingSoldier.transform.position = new Vector3(10f, 0f, 90f);
        recievingSoldier.transform.eulerAngles = new Vector3(0f, 270f, 0f);

        canvas.SetActive(false);
        
        /*
        cameraManager.disableCamera("overheadCamea");
        Debug.Log("main camera is: " + mainCamera.isActiveAndEnabled);
        battleCamera.enabled = true;
        Debug.Log("battle camera is: " + battleCamera.isActiveAndEnabled);
        */
    }

    private void closeAttackScene()
    {
        /*
        battleCamera.enabled = false;
        Debug.Log("battle camera is: " + battleCamera.isActiveAndEnabled);
        mainCamera.enabled = true;
        Debug.Log("main camera is: " + mainCamera.isActiveAndEnabled);
        */

        attackingSoldier.transform.position = attackingSoldierPosition;
        attackingSoldier.transform.eulerAngles = attackingSoldierRotation;
        if (defendingSoldier != null)
        {
            defendingSoldier.transform.position = defendingSoldierPosition;
            defendingSoldier.transform.eulerAngles = defendingSoldierRotation;
        }
        canvas.SetActive(true);
    }

    public void resetButton()
    {
        grid.clearGrid();
        grid.clearGrid();
        attackButtonClicked = false;
    }

    private void handleMoveCancelled()
    {
        resetButton();
        foreach (GameObject gameObj in soldierManager.getCurrentSoldiers())
        {
            if (gameObj.GetComponent<AbstractSoldier>().getCurrentState().Equals(AbstractSoldier.TurnState.ATTACK))
            {
                gameObj.GetComponent<AbstractSoldier>().setCurrentState(AbstractSoldier.TurnState.ACTIVE);
            }
        }
        cancelPanel.SetActive(false);
        actionPanel.SetActive(true);
        attackButton.GetComponent<Button>().interactable = true;
        attackButtonClicked = false;
        cancelButton.onClick.RemoveListener(handleMoveCancelled);
    }

    public void handleTileClick(float xpos, float zpos)
    {
        if (attackButtonClicked)
        {
            string clickedPos = xpos + "," + zpos;
            if(listOfOptions.ContainsKey(clickedPos))
            {
                defendingSoldier = listOfOptions[clickedPos].getOccupiedSoldier();
                if(defendingSoldier != null)
                {
                    setUpAttackScene(defendingSoldier);
                    handleMoveSelected(defendingSoldier);
                    closeAttackScene();
                    attackButtonClicked = false;
                    cancelButton.onClick.RemoveListener(handleMoveCancelled);
                }
            }
        }
        
    }

    public bool getAttackButtonClicked()
    {
        return attackButtonClicked;
    }

    public void setAttackButtonClicked(bool attackButtonClicked)
    {
        this.attackButtonClicked = attackButtonClicked;
    }
}
