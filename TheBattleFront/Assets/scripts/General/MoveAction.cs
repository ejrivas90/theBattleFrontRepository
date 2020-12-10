using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoveAction : MonoBehaviour {
    private Grid grid;
    private bool hasButtonBeenClicked;
    private bool moveMade;
    private GameObject currentActiveSoldier;
    private GameObject actionPanel;
    private GameObject cancelPanel;
    private GameObject panel;
    private GameObject moveButton;
    public Dictionary<string, GridObject> listOfOptions = new Dictionary<string, GridObject>();
    private Vector3 currentSoldierPosition;
    private Text battleOutput;
    private SoldierManager soldierManager;
    private Button cancelButton;

    private void Awake()
    {
        actionPanel = GameObject.Find("actionPanel");
        panel = GameObject.Find("Panel");
        cancelPanel = GameObject.Find("cancelPanel");
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        moveButton = GameObject.Find("Move");
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        cancelButton = GameObject.Find("cancelButton").GetComponent<Button>();
    }

    void Start()
    {
        Debug.Log("end turn button script started");
        hasButtonBeenClicked = false;
        battleOutput = GameObject.Find("battleOutput").GetComponent<Text>();
    }

    public void newTurn()
    {
        grid.clearGrid();
        updateSoldier();
        if (currentActiveSoldier.GetComponent<AbstractSoldier>().getHasMoved() == true)
        {
            buttonDisable(true);
            moveMade = true;
        } else
        {
            buttonDisable(false);
            moveMade = false;
        }
    }

    public void moveButtonClicked()
    {
        battleOutput.text = "Battle Output: \n";
        Debug.Log("Move action button has been clicked");
        cancelPanel.SetActive(true);
        actionPanel.SetActive(false);
        cancelButton.onClick.AddListener(handleMoveCancelled);
        if (!moveMade)
        {
            AbstractSoldier soldier = currentActiveSoldier.GetComponent<AbstractSoldier>();
            getMovementPoints(soldier);
            grid.liftSoldier(soldier);
            hasButtonBeenClicked = true;
        }
    }
    
    private void getMovementPoints(AbstractSoldier soldier)
    {
        soldier.rollDie();
        listOfOptions = grid.showMoveOption(soldier.getSoldierVector(), soldier.getCurrentStamina());
        battleOutput.text = battleOutput.text + "You rolled a: " + soldier.getCurrentStamina();
    }

    private void Update() {}

    public void handleMoveCancelled()
    {
        currentActiveSoldier.GetComponent<AbstractSoldier>().setCurrentState(AbstractSoldier.TurnState.ACTIVE);
        currentActiveSoldier.transform.position = currentSoldierPosition;
        grid.addSoldierToGrid(currentActiveSoldier.transform.position, currentActiveSoldier);
        grid.clearGrid();
        hasButtonBeenClicked = false;
        cancelPanel.SetActive(false);
        actionPanel.SetActive(true);
        cancelButton.onClick.RemoveListener(handleMoveCancelled);
    }

    public void handleTileClicked(float xpos, float zpos)
    {
        if (hasButtonBeenClicked)
        {
            string posToString = xpos + "," + zpos;
            if (listOfOptions.ContainsKey(posToString) && (listOfOptions[posToString].getOccupiedSoldier() == null))
            {
                currentActiveSoldier.transform.position = new Vector3(xpos, currentActiveSoldier.transform.position.y, zpos);
                panel.SetActive(false);
                actionPanel.SetActive(true);

                currentSoldierPosition = currentActiveSoldier.transform.position;
                grid.addSoldierToGrid(currentSoldierPosition, currentActiveSoldier);
                grid.clearGrid();
                moveMade = true;
                currentActiveSoldier.GetComponent<AbstractSoldier>().setHasMoved(true);
                hasButtonBeenClicked = false;
                currentActiveSoldier.GetComponent<AbstractSoldier>().setCurrentState(AbstractSoldier.TurnState.ACTIVE);
                cancelPanel.SetActive(false);
                actionPanel.SetActive(true);
                moveButton.GetComponent<Button>().interactable = false;
                cancelButton.onClick.RemoveListener(handleMoveCancelled);
            }
        }
    }

    public bool getHasButtonBeenClicked()
    {
        return hasButtonBeenClicked;
    }

    public void setHasButtonBeenClicked(bool hasButtonBeenClicked)
    {
        this.hasButtonBeenClicked = hasButtonBeenClicked;
    }

    public void buttonDisable(bool shouldDisabe)
    {
        moveButton.GetComponent<Button>().interactable = !shouldDisabe;
    }

    public void updateSoldier()
    {
        currentActiveSoldier = soldierManager.findSoldier("ACTIVE");
        currentSoldierPosition = currentActiveSoldier.transform.position;
    }
}
