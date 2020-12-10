using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScript : MonoBehaviour {
    public GameObject playerTankPre;
    public GameObject enemyTankPre;
    public GameObject playerArtPre;
    public GameObject enemyArtPre;
    public GameObject playerInfPre;
    public GameObject enemyInfPre;
    public GameObject playerMarkPre;
    public GameObject enemyMarkPre;
    GameObject tankClone;
    GameObject artClone;
    GameObject infClone;
    GameObject marksClone;
    GameObject clone;
    private bool isRecruiting;
    private string prefabToMake;
    private string activePlayer;
    private Vector3 recruitPosition;

    public void Update()
    {
        switch (prefabToMake)
        {
            case ("tank"):
                if (isRecruiting)
                {
                    clone = createTankClone();
                }
                break;
            case ("marksman"):
                if (isRecruiting)
                {
                    clone = createMarkClone();
                }
                break;
            case ("infantry"):
                if (isRecruiting)
                {
                    clone = createInfClone();
                }
                break;
            case ("artillery"):
                if (isRecruiting)
                {
                    clone = createArtClone();
                }
                break;
        }
     }

    public GameObject createTankClone()
    {
        if(activePlayer.Equals("PLAYER"))
        {
            tankClone = Instantiate(playerTankPre, recruitPosition, Quaternion.identity) as GameObject;
            tankClone.GetComponent<Tank>().init(activePlayer.ToString());
            tankClone.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else
        {
            tankClone = Instantiate(enemyTankPre, recruitPosition, Quaternion.identity) as GameObject;
            tankClone.GetComponent<Tank>().init(activePlayer.ToString());
        }
        Vector3 newPos = new Vector3(tankClone.transform.position.x, 0f, tankClone.transform.position.z);
        tankClone.transform.position = newPos;
        return tankClone;
    }

    public GameObject createArtClone()
    {
        if (activePlayer.Equals("PLAYER"))
        {
            artClone = Instantiate(playerArtPre, recruitPosition, Quaternion.identity) as GameObject;
            artClone.GetComponent<Artillery>().init(activePlayer.ToString());
            artClone.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else
        {
            artClone = Instantiate(enemyArtPre, recruitPosition, Quaternion.identity) as GameObject;
            artClone.GetComponent<Artillery>().init(activePlayer.ToString());
        }
        artClone.transform.position = new Vector3(artClone.transform.position.x, .27f, artClone.transform.position.z);
        return artClone;
    }

    public GameObject createInfClone()
    {
        if (activePlayer.Equals("PLAYER"))
        {
            infClone = Instantiate(playerInfPre, recruitPosition, Quaternion.identity) as GameObject;
            infClone.GetComponent<Infantry>().init(activePlayer.ToString());
            infClone.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else
        {
            infClone = Instantiate(enemyInfPre, recruitPosition, Quaternion.identity) as GameObject;
            infClone.GetComponent<Infantry>().init(activePlayer.ToString());
        }
        infClone.transform.position = new Vector3(infClone.transform.position.x, 0f, infClone.transform.position.z);
        return infClone;
    }

    public GameObject createMarkClone()
    {
        if (activePlayer.Equals("PLAYER"))
        {
            marksClone = Instantiate(playerMarkPre, recruitPosition, Quaternion.identity) as GameObject;
            marksClone.GetComponent<Marksman>().init(activePlayer.ToString());
            marksClone.transform.Rotate(0f, 180f, 0f, Space.World);
        }
        else
        {
            marksClone = Instantiate(enemyMarkPre, recruitPosition, Quaternion.identity) as GameObject;
            marksClone.GetComponent<Marksman>().init(activePlayer.ToString());
        }
        marksClone.transform.position = new Vector3(marksClone.transform.position.x, 0f, marksClone.transform.position.z);
        return marksClone;
    }

    public void setIsRecruiting(bool isRecruiting)
    {
        this.isRecruiting = isRecruiting;
    }

    public bool getIsRecruiting()
    {
        return isRecruiting;
    }

    public void setPrefabToMake(string prefabToMake)
    {
        this.prefabToMake = prefabToMake;
    }

    public string getPrefabToMake()
    {
        return prefabToMake;
    }

    public void setActivePlayer(string activePlayer)
    {
        this.activePlayer = activePlayer;
    }

    public string getActivePlayer()
    {
        return activePlayer;
    }

    public GameObject getClone()
    {
        return clone;
    }

    public void setRecruitPosition(Vector3 recruitPosition)
    {
        this.recruitPosition = recruitPosition;
    }

    public Vector3 getRecruitPosition()
    {
        return recruitPosition;
    }
}
