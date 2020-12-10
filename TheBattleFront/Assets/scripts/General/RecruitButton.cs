using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour {
    private TurnManager turnManager;
    private GameObject actionPanel;
    private GameObject panel;
    private GameObject cancelPanel;
    private Button cancelButton;
    private Text battleOutput;
    private bool hasBeenClicked;
    private Grid grid;
    private PrefabScript prefab;
    private SoldierManager soldierManager;
    private string whosTurn;
    private MoveAction moveAction;
    private CameraManager cameraManager;

    private void Awake()
    {
        actionPanel = GameObject.Find("actionPanel");
        panel = GameObject.Find("Panel");
        cancelPanel = GameObject.Find("cancelPanel");
        cancelButton = GameObject.Find("cancelButton2").GetComponent<Button>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        prefab = GameObject.Find("prefabInstantiator").GetComponent<PrefabScript>();
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        moveAction = GameObject.Find("actionPanel").GetComponent<MoveAction>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    private void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        battleOutput = GameObject.Find("battleOutput").GetComponent<Text>();
    }
    
	public void recruitAction()
    {
        whosTurn = turnManager.whosTurn.ToString();
        battleOutput.text = "Battle Output: \n";
        createSubmenu();
        cameraManager.showRecruitView();
    }

    private void createSubmenu()
    {
        hasBeenClicked = true;
        actionPanel.SetActive(false);
        panel.SetActive(true);
        cancelButton.onClick.AddListener(listenForCancel);
    }

    public void recTank()
    {
        recruit("tank");
    }

    public void recArt()
    {
        recruit("artillery");
    }

    public void recInf()
    {
        recruit("infantry");
    }

    public void recMark()
    {
        recruit("marksman");
    }

    private void Update()
    {
    }

    private void listenForCancel()
    {
        hasBeenClicked = false;
        panel.SetActive(false);
        actionPanel.SetActive(true);
        grid.clearGrid();
        cancelButton.onClick.RemoveListener(listenForCancel);
        cameraManager.returnCameraToSoldier();
    }

    public void handleTileClick(float xpos, float zpos)
    {
        if(hasBeenClicked)
        {
            Vector3 vectorPosition = new Vector3(xpos, 0f, zpos);
            recruitOnSelection(vectorPosition);
        }
    }

    public bool getHasBeenClicked()
    {
        return hasBeenClicked;
    }

    public void setHasBeenClicked(bool hasBeenClicked)
    {
        this.hasBeenClicked = hasBeenClicked;
    }

    public  void showRecruitOptions()
    {
        grid.clearGrid();
        grid.showRecruitOptions(turnManager.whosTurn.ToString());
    }

    public void recruit(string recruitType)
    { 
        if (soldierManager.getCurrentSoldiers().Count < 6)
        {
            Debug.Log("recruit was selected");
            prefab.setPrefabToMake(recruitType);
            prefab.setActivePlayer(turnManager.whosTurn.ToString());
            showRecruitOptions();
        }
    }

    private void recruitOnSelection(Vector3 recruitPos)
    {
        if (hasBeenClicked && isValidSelection(recruitPos))
        {
            prefab.setRecruitPosition(recruitPos);
            prefab.setIsRecruiting(true);
            prefab.Update();
            GameObject recruit = prefab.getClone();
            if (recruit != null)
            {
                prefab.setPrefabToMake("");
                prefab.setIsRecruiting(false);
                grid.addSoldierToGrid(recruitPos, recruit);
                soldierManager.addSoldier(whosTurn.ToLower(), recruit);
                moveAction.updateSoldier();
            }
            actionPanel.SetActive(true);
            panel.SetActive(false);
            grid.clearGrid();
            hasBeenClicked = false;
            cancelButton.onClick.RemoveListener(listenForCancel);
            cameraManager.returnCameraToSoldier();
        }
    }

    private bool isValidSelection(Vector3 recruitPos)
    {
        Dictionary<string, GridObject> listOfOptions = new Dictionary<string, GridObject>();
        listOfOptions = grid.getCurrentOptions(whosTurn);
        bool isValid = false;
        
        string stringPos = recruitPos.x + "," + recruitPos.z;
        if(listOfOptions.ContainsKey(stringPos))
        {
            isValid = true;
        }
        else
        {
           isValid = false;
        }
        
        return isValid;
    }
}
