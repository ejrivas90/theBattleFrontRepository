using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Camera camTemplate;
    public Camera player1Camera;
    public Camera player2Camera;
    public Camera battleCamera;
    public Camera activeCamera;
    public GameObject p1RecruitCameraPos;
    public GameObject p2RecruitCameraPos;
    private GameObject p1StartingPosition;
    private GameObject p2StartingPosition;
    private string activePlayerCamera;
    private SoldierManager soldierManager;
    private List<Camera> cameraList;

    void Start() {
        p1StartingPosition = setStartingPosition(player1Camera);
        p2StartingPosition = setStartingPosition(player2Camera);
        soldierManager = GameObject.Find("soldierManager").GetComponent<SoldierManager>();
        activePlayerCamera = "p1";
        cameraList = new List<Camera>();
    }

    private GameObject setStartingPosition(Camera startingCamera)
    {
        GameObject newObject = new GameObject();
        float x = startingCamera.transform.position.x;
        float y = startingCamera.transform.position.y;
        float z = 0;
        if (startingCamera.name.Contains("player1"))
        {
            z = startingCamera.transform.position.z - 4;
        }
        else
        {
            z = startingCamera.transform.position.z + 4;
        }
        Vector3 startingVector = new Vector3(x, y, z);
        newObject.transform.position = startingVector;
        newObject.transform.rotation = startingCamera.transform.rotation;
        return newObject;
    }

    void Update() {

    }

    public void disableP1Camera(bool shouldDisable)
    {
        player1Camera.enabled = !shouldDisable;
    }

    public void disableP2Camera(bool shouldDisable)
    {
        player2Camera.enabled = !shouldDisable;
    }

    public void disableBattleCamera(bool shouldDisable)
    {
        battleCamera.enabled = !shouldDisable;
    }

    public void startGame()
    {
        //player1Camera.enabled = true;
        //player1Camera.GetComponent<CameraFollow>().setSoldierToFollow(soldierManager.findSoldier("ACTIVE"));
       //player2Camera.enabled = false;
        battleCamera.enabled = false;
        //activeCamera = player1Camera;
    }

    public void switchTurns(string whosTurn)
    {
        if (whosTurn.Equals("PLAYER"))
        {
            player1Camera.enabled = true;
            player2Camera.enabled = false;
            activePlayerCamera = "p1";
            activeCamera = player1Camera;
            player1Camera.GetComponent<CameraFollow>().setSoldierToFollow(soldierManager.findSoldier("ACTIVE"));
        } else
        {
            player1Camera.enabled = false;
            player2Camera.enabled = true;
            activePlayerCamera = "p2";
            activeCamera = player2Camera;
            player2Camera.GetComponent<CameraFollow>().setSoldierToFollow(soldierManager.findSoldier("ACTIVE"));
        }

    }

    public Camera getActiveCamera()
    {
        return activeCamera;
    }

    public void showRecruitView()
    {
        activeCamera.GetComponent<CameraFollow>().shouldOffset = false;
        if (activePlayerCamera.Equals("p1"))
        {
            activeCamera.transform.position = p1RecruitCameraPos.transform.position;
            activeCamera.transform.rotation = p1RecruitCameraPos.transform.rotation;
            activeCamera.GetComponent<CameraFollow>().setSoldierToFollow(p1RecruitCameraPos);
        }
        else
        {
            activeCamera.transform.position = p2RecruitCameraPos.transform.position;
            activeCamera.transform.rotation = p2RecruitCameraPos.transform.rotation;
            activeCamera.GetComponent<CameraFollow>().setSoldierToFollow(p2RecruitCameraPos);
        }
    }

    public void returnCameraToSoldier()
    {
        activeCamera.GetComponent<CameraFollow>().shouldOffset = true;
        GameObject returnSoldier = soldierManager.findSoldier("ACTIVE");
        if (activePlayerCamera.Equals("p1"))
        {
            activeCamera.transform.position = p1StartingPosition.transform.position;
            activeCamera.transform.rotation = p1StartingPosition.transform.rotation;
        }
        else
        {
            activeCamera.transform.position = p2StartingPosition.transform.position;
            activeCamera.transform.rotation = p2StartingPosition.transform.rotation;
        }

        activeCamera.GetComponent<CameraFollow>().setSoldierToFollow(returnSoldier);
    }

    public void panToHoveredSoldier(AbstractSoldier selectedSoldier)
    {
        GameObject soldierObject = selectedSoldier.gameObject;
        activeCamera.GetComponent<CameraFollow>().setSoldierToFollow(soldierObject);
    }

    public void createStartingSoldierCamera(GameObject soldierObject)
    {
        AbstractSoldier abstractSoldier = soldierObject.GetComponent<AbstractSoldier>();
        Vector3 soldierVector = soldierObject.transform.position;
        if ("player".Equals(abstractSoldier.getTeam()))
        {
            Vector3 cameraPos = new Vector3(soldierVector.x, soldierVector.y+5, soldierVector.z+4);
            Camera playerChampCam = Instantiate(camTemplate, cameraPos, Quaternion.identity);
            playerChampCam.name = "champCamera";
            Vector3 cameraRotation = playerChampCam.transform.eulerAngles;
            cameraRotation.x = 38;
            cameraRotation.y = 180;
            cameraRotation.z = 0;
            playerChampCam.transform.rotation = Quaternion.Euler(cameraRotation);

            abstractSoldier.setMyCamera(playerChampCam);
            playerChampCam.enabled = true;
        }
        else
        {
            Vector3 cameraPos = new Vector3(soldierVector.x, soldierVector.y + 5, soldierVector.z - 4);
            Camera enemyChampCam = Instantiate(camTemplate, cameraPos, Quaternion.identity);
            enemyChampCam.name = "enemyCamera";
            Vector3 cameraRotation = enemyChampCam.transform.eulerAngles;
            cameraRotation.x = 38;
            cameraRotation.y = 0;
            cameraRotation.z = 0;
            enemyChampCam.transform.rotation = Quaternion.Euler(cameraRotation);

            abstractSoldier.setMyCamera(enemyChampCam);
            enemyChampCam.enabled = false;
        }
        
        
    }
}
