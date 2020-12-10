using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public GameObject plane;
    public float width = 14;
    public float height = 16;
    private GridObject[,] grid = new GridObject[14, 16];
    private Dictionary<string, GridObject> gridDictionary = new Dictionary<string, GridObject>();   
    private Dictionary<string, GridObject> playerRecruitDictionary = new Dictionary<string, GridObject>();
    private Dictionary<string, GridObject> enemyRecruitDictionary = new Dictionary<string, GridObject>();
    public Material[] materials;
    public Material oneMat;
    private Dictionary<string, GridObject> playersAvailableRecruitOptions = new Dictionary<string, GridObject>();
    private Dictionary<string, GridObject> enemyAvailableRecruitOptions = new Dictionary<string, GridObject>();

    void Awake()
    {
        
        Debug.Log("Grid initialized");
        for (int i = 0; i < width; i++)
        {
            for (int h = 0; h < height; h++)
            {

                GameObject gridPlane = (GameObject)Instantiate(plane);
                gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + i,
                    gridPlane.transform.position.y, gridPlane.transform.position.z + h);
                gridPlane.name = "square: " + i + "," + h;
                TileClicked tileClicked = gridPlane.AddComponent<TileClicked>() as TileClicked;
                tileClicked.setX(gridPlane.transform.position.x);
                tileClicked.setZ(gridPlane.transform.position.z);
                GridObject planeObject = new GridObject();
                planeObject.setPlane(gridPlane);
                grid[i, h] = planeObject;
                string coords = grid[i, h].getSquarePosition().x + "," + grid[i, h].getSquarePosition().z;
                gridDictionary.Add(coords, grid[i, h]);
            }
        }
        addBasesToBoard();
        addToPlayerRecruitList();
        addToEnemyRecruitList();
        clearGrid();
    }

    private void addBasesToBoard()
    {
        GameObject playerBase = GameObject.Find("Base1");
        GameObject enemyBase = GameObject.Find("Base2");
    
        gridDictionary["1.5,7.5"].setOccupiedSoldier(playerBase);
        gridDictionary["0.5,7.5"].setOccupiedSoldier(playerBase);
        gridDictionary["-0.5,7.5"].setOccupiedSoldier(playerBase);

        gridDictionary["-1.5,-7.5"].setOccupiedSoldier(enemyBase);
        gridDictionary["0.5,-7.5"].setOccupiedSoldier(enemyBase);
        gridDictionary["-0.5,-7.5"].setOccupiedSoldier(enemyBase);
    }

    private bool checkBasePosition(Vector3 positionToCheck)
    {
        bool isValid = true;
        Vector3 pBase1 = new Vector3(1.5f, 0f, 7.5f);
        Vector3 pBase2 = new Vector3(.5f, 0f, 7.5f);
        Vector3 pBase3 = new Vector3(-.5f, 0f, 7.5f);

        Vector3 eBase1 = new Vector3(-1.5f, 0f, -7.5f);
        Vector3 eBase2 = new Vector3(.5f, 0f, -7.5f);
        Vector3 eBase3 = new Vector3(-.5f, 0f, -7.5f);

        if(positionToCheck.Equals(pBase1) || positionToCheck.Equals(pBase2)
            || positionToCheck.Equals(pBase3))
        {
            isValid = false;
        }
        if (positionToCheck.Equals(eBase1) || positionToCheck.Equals(eBase2)
            || positionToCheck.Equals(eBase3))
        {
            isValid = false;
        }

        return isValid;
    }

    private void addToPlayerRecruitList()
    {
        playerRecruitDictionary.Add("0.5,6.5", gridDictionary["0.5,6.5"]);
        playerRecruitDictionary.Add("-1.5,7.5", gridDictionary["-1.5,7.5"]);
        playerRecruitDictionary.Add("-1.5,6.5", gridDictionary["-1.5,6.5"]);
        playerRecruitDictionary.Add("-0.5,6.5", gridDictionary["-0.5,6.5"]);
        playerRecruitDictionary.Add("1.5,6.5", gridDictionary["1.5,6.5"]);
        playerRecruitDictionary.Add("2.5,6.5", gridDictionary["2.5,6.5"]);
        playerRecruitDictionary.Add("2.5,7.5", gridDictionary["2.5,7.5"]);
    }

    private void addToEnemyRecruitList()
    {
        enemyRecruitDictionary.Add("-0.5,-6.5", gridDictionary["-0.5,-6.5"]);
        enemyRecruitDictionary.Add("-2.5,-7.5", gridDictionary["-2.5,-7.5"]);
        enemyRecruitDictionary.Add("-2.5,-6.5", gridDictionary["-2.5,-6.5"]);
        enemyRecruitDictionary.Add("-1.5,-6.5", gridDictionary["-1.5,-6.5"]);
        enemyRecruitDictionary.Add("0.5,-6.5", gridDictionary["0.5,-6.5"]);
        enemyRecruitDictionary.Add("1.5,-6.5", gridDictionary["1.5,-6.5"]);
        enemyRecruitDictionary.Add("1.5,-7.5", gridDictionary["1.5,-7.5"]);
    }

    public Dictionary<string, GridObject> showMoveOption(Vector3 position, int points)
    {
        Renderer rend;
        Dictionary<string, GridObject> listOfOptions = new Dictionary<string, GridObject>();
        string squarePosition = position.x + "," + position.z;
        listOfOptions.Add(squarePosition, gridDictionary[squarePosition]);
        float x = position.x;
        float z = position.z;
        GridObject square = gridDictionary[squarePosition];   
        Renderer mainRend = square.getPlane().GetComponent<Renderer>();
        
        for (int i = 0; i < points + 1; i++)
        {
            for (int h = 1; h < points + 1; h++)
            {

                if ((i + h != (points + 1)) && (i + h < (points + 1)))
                {
                    Debug.Log("painted: " + (x + i) + ", " + (z + h));
                    Debug.Log(i + ", " + h);

                    if (gridDictionary.ContainsKey((x + i) + "," + (z + h)))
                    {
                        string key = (x + i) + "," + (z + h);
                        rend = gridDictionary[key].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if(!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                        
                    }
                    if (gridDictionary.ContainsKey((x + h) + "," + (z + i)))
                    {
                        string key = (x + h) + "," + (z + i);
                        rend = gridDictionary[(x + h) + "," + (z + i)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }

                    if (gridDictionary.ContainsKey((x - i) + "," + (z - h)))
                    {
                        string key = (x - i) + "," + (z - h);
                        rend = gridDictionary[(x - i) + "," + (z - h)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }

                    if (gridDictionary.ContainsKey((x + i) + "," + (z - h)))
                    {
                        string key = (x + i) + "," + (z - h);
                        rend = gridDictionary[(x + i) + "," + (z - h)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }

                    if (gridDictionary.ContainsKey((x + h) + "," + (z - i)))
                    {
                        string key = (x + h) + "," + (z - i);
                        rend = gridDictionary[(x + h) + "," + (z - i)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }

                    if (gridDictionary.ContainsKey((x - i) + "," + (z + h)))
                    {
                        string key = (x - i) + "," + (z + h);
                        rend = gridDictionary[(x - i) + "," + (z + h)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }

                    if (gridDictionary.ContainsKey((x - h) + "," + (z + i)))
                    {
                        string key = (x - h) + "," + (z + i);
                        rend = gridDictionary[(x - h) + "," + (z + i)].getPlane().GetComponent<Renderer>();
                        rend.material.color = Color.red;
                        if (!listOfOptions.ContainsKey(key))
                        {
                            listOfOptions.Add(key, gridDictionary[key]);
                        }
                    }
                }
            }

        }
        Debug.Log(position.ToString());
        Debug.Log("Items in list of options: " + listOfOptions.Count);
        return listOfOptions;
    }

    public void showRecruitOptions(string playerTurn)
    {
        Dictionary<string, GridObject> listOfOptions = new Dictionary<string, GridObject>();
        Renderer rend;
        if(playerTurn.Equals("PLAYER"))
        {
            listOfOptions = checkOccupiedSpaces(playerRecruitDictionary);
            foreach(KeyValuePair<string, GridObject> obj in listOfOptions)
            {
                rend = obj.Value.getPlane().GetComponent<Renderer>();
                rend.material.color = Color.cyan;
            }
            setPlayerRecruitOptions(listOfOptions);
        }
        else
        {
            listOfOptions = checkOccupiedSpaces(enemyRecruitDictionary);
            foreach (KeyValuePair<string, GridObject> obj in listOfOptions)
            {
                rend = obj.Value.getPlane().GetComponent<Renderer>();
                rend.material.color = Color.cyan;
            }
            setEnemyRecruitOptions(listOfOptions);
        }
    }

    public List<GridObject> checkOccupiedSpaces(List<GridObject> listOfOptions)
    {
        List<GridObject> returnList = new List<GridObject>();
        for (int i = 0; i < listOfOptions.Count; i++)
        {
            GridObject newGridObj = new GridObject();
            newGridObj.setOccupiedSoldier(listOfOptions[i].getOccupiedSoldier());
            newGridObj.setPlane(listOfOptions[i].getPlane());
            returnList.Add(newGridObj);
        }
        List<int> indexList = new List<int>();
        for (int i = 0; i < returnList.Count; i++)
        {
            if(returnList[i].getOccupiedSoldier() != null)
            {
                indexList.Add(i);
                //returnList.RemoveAt(i);
            }
        }
        indexList.Reverse();
        foreach(int i in indexList)
        {
            returnList.RemoveAt(i);
        }

        return returnList;
    }

    private Dictionary<string, GridObject> checkOccupiedSpaces(Dictionary<string, GridObject> optionsDictionary)
    {
        Dictionary<string, GridObject> returnDictionary = new Dictionary<string, GridObject>();
        foreach(KeyValuePair<string, GridObject> obj in optionsDictionary)
        {
            if(obj.Value.getOccupiedSoldier() == null)
            {
                string key = obj.Key;
                GridObject gridObject = obj.Value;

                returnDictionary.Add(key, gridObject);
            }
        }

        return returnDictionary;
    }
    public void clearGrid()
    {
        foreach(KeyValuePair<string, GridObject> obj in gridDictionary)
        {
            Renderer rend;
            rend = obj.Value.getPlane().GetComponent<Renderer>();
            rend.material = new Material(Shader.Find("Standard"));
            rend.material.color = Color.gray;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<string, GridObject> obj in gridDictionary)
        {
            if (obj.Value.getOccupiedSoldier() != null)
            {
                Renderer rend;
                rend = obj.Value.getPlane().GetComponent<Renderer>();
                AbstractSoldier occupyingSoldier = obj.Value.getOccupiedSoldier().GetComponent<AbstractSoldier>();
                if ((occupyingSoldier.getCurrentState().Equals(AbstractSoldier.TurnState.ACTIVE)) || (occupyingSoldier.getCurrentState().Equals(AbstractSoldier.TurnState.ATTACK))
                    || occupyingSoldier.getCurrentState().Equals(AbstractSoldier.TurnState.MOVE))
                {
                    rend.material = oneMat;
                }
                else
                {
                    //rend.material = new Material(Shader.Find("Standard"));
                    //rend.material.color = Color.gray;
                }
            }
        }
    }

    public void setGridDictionary(Dictionary<string, GridObject> gridDictionary)
    {
        this.gridDictionary = gridDictionary;
    }

    public Dictionary<string, GridObject> getGridDictionary()
    {
        return gridDictionary;
    }

    public void addSoldierToGrid(Vector3 position, GameObject recruit)
    {
        string square = position.x + "," + position.z;

        gridDictionary[square].setOccupiedSoldier(recruit);
    }

    public void liftSoldier(AbstractSoldier soldierToLift)
    {
        string square = soldierToLift.transform.position.x + "," + soldierToLift.transform.position.z;
        gridDictionary[square].setOccupiedSoldier(null);
    }

    public Dictionary<string, GridObject> showAttackOption(GameObject soldierObject)
    {
        AbstractSoldier soldier = soldierObject.GetComponent<AbstractSoldier>();
        string soldierSquare = soldier.transform.position.x + "," + soldier.transform.position.z;
        GridObject occupiedSpace = gridDictionary[soldierSquare];
        Dictionary<string, GridObject> listOfOptions = new Dictionary<string, GridObject>();
        listOfOptions = highlightOptions(occupiedSpace, soldier.getAtkRange());
        return listOfOptions;
    }

    private Dictionary<string, GridObject> highlightOptions(GridObject occupiedSpace, int atkRange)
    {
        Renderer rend;
        Dictionary<string, GridObject> dictionaryOfOptions = new Dictionary<string, GridObject>();
        dictionaryOfOptions.Add("occupiedSpace" ,occupiedSpace);
        Renderer mainRend = occupiedSpace.getPlane().GetComponent<Renderer>();
        GameObject plane = occupiedSpace.getPlane();
        float x = plane.transform.position.x;
        float z = plane.transform.position.z;

        for (int i = 1; i < atkRange + 1; i++)
        {
            if (gridDictionary.ContainsKey((x + i) + "," + (z)))
            {
                string key = (x + i) + "," + (z);
                rend = gridDictionary[(x + i) + "," + (z)].getPlane().GetComponent<Renderer>();
                rend.material.color = Color.red;
                if (!dictionaryOfOptions.ContainsKey(key))
                {
                    dictionaryOfOptions.Add(key, gridDictionary[key]);
                }
            }
            if (gridDictionary.ContainsKey((x - i) + "," + (z)))
            {
                string key = (x - i) + "," + (z);
                rend = gridDictionary[(x - i) + "," + (z)].getPlane().GetComponent<Renderer>();
                rend.material.color = Color.red;
                if (!dictionaryOfOptions.ContainsKey(key))
                {
                    dictionaryOfOptions.Add(key, gridDictionary[key]);

                }
            }
            if (gridDictionary.ContainsKey((x) + "," + (z + i)))
            {
                string key = (x) + "," + (z + i);
                rend = gridDictionary[(x) + "," + (z + i)].getPlane().GetComponent<Renderer>();
                rend.material.color = Color.red;
                if (!dictionaryOfOptions.ContainsKey(key))
                {
                    dictionaryOfOptions.Add(key, gridDictionary[key]);

                }
            }
            if (gridDictionary.ContainsKey((x) + "," + (z - i)))
            {
                string key = (x) + "," + (z - i);
                rend = gridDictionary[(x) + "," + (z - i)].getPlane().GetComponent<Renderer>();
                rend.material.color = Color.red;
                if (!dictionaryOfOptions.ContainsKey(key))
                {
                    dictionaryOfOptions.Add(key, gridDictionary[key]);
                }
            }
        }

        return dictionaryOfOptions;
    }

    public Dictionary<string, GridObject> getPlayerRecruitOptions()
    {
        return playersAvailableRecruitOptions;
    }

    public void setPlayerRecruitOptions(Dictionary<string, GridObject> playerOptions)
    {
        this.playersAvailableRecruitOptions = playerOptions;
    }

    public Dictionary<string, GridObject> getEnemyRecruitOptions()
    {
        return enemyAvailableRecruitOptions;
    }

    public void setEnemyRecruitOptions(Dictionary<string, GridObject> enemyOptions)
    {
        this.enemyAvailableRecruitOptions = enemyOptions;
    }

    public Dictionary<string, GridObject> getCurrentOptions(string whosTurn)
    {
        if(whosTurn.Equals("PLAYER"))
        {
            return playersAvailableRecruitOptions;
        }
        else
        {
            return enemyAvailableRecruitOptions;
        }
    }
}
