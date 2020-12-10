using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierManager : MonoBehaviour {
    public List<GameObject> playerSoldiers = new List<GameObject>();
    public List<GameObject> enemySoldiers = new List<GameObject>();
    public List<GameObject> currentSoldiers = new List<GameObject>();
    private TurnManager turnManager;
	private GameObject attackButton;
	private GameObject moveButton;
    public ParticleSystem explosion;

    private void Awake()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    void Start () {
		attackButton = GameObject.Find("AttackButton");
		moveButton = GameObject.Find ("Move");
	}
	
	void Update () {
		
	}

    public List<GameObject> getPlayerSoldiers()
    {
        return playerSoldiers;
    }

    public void setPlayerSoldiers(List<GameObject> playerSoldiers)
    {
        this.playerSoldiers = playerSoldiers;
    }

    public List<GameObject> getEnemySoldiers()
    {
        return enemySoldiers;
    }

    public void setEnemySoldiers(List<GameObject> enemySoldiers)
    {
        this.enemySoldiers = enemySoldiers;
    }

    public List<GameObject> getCurrentSoldiers()
    {
        return currentSoldiers;
    }

    public void setCurrentSoldiers(List<GameObject> currentSoldiers)
    {
        this.currentSoldiers = currentSoldiers;
    }

    public void removeSoldier(GameObject soldier)
    {
        if (currentSoldiers.Contains(soldier))
        {
            currentSoldiers.Remove(soldier);
            if (turnManager.whosTurn.ToString().Equals("PLAYER"))
            {
                playerSoldiers.Remove(soldier);

            }
            else
            {
                enemySoldiers.Remove(soldier);
            }
        }
        else
        {
            if (turnManager.whosTurn.ToString().Equals("PLAYER"))
            {
                enemySoldiers.Remove(soldier);
            }
            else
            {
                playerSoldiers.Remove(soldier);
            }
        }
        Destroy(soldier);
    }

    public void updateCurrentSoliderList(string whosTurn)
    {
        endCurrentTurn(currentSoldiers);
        currentSoldiers.Clear();
        if(whosTurn.Equals("player"))
        {
            currentSoldiers.AddRange(playerSoldiers);
            
        }
        else
        {
            currentSoldiers.AddRange(enemySoldiers);
        }
		if (currentSoldiers.Count > 0) {
            beginNextPlayerTurn(currentSoldiers);
		}
    }

    
    public void addSoldier(string whosTurn, GameObject soldierToAdd)
    {
        AbstractSoldier soldierAbstract = soldierToAdd.GetComponent<AbstractSoldier>();
        if (whosTurn.Equals("player"))
        {
            soldierAbstract.setTeam("player");
            if (currentSoldiers.Count == 0) {
                soldierAbstract.setCurrentState (AbstractSoldier.TurnState.ACTIVE);
                soldierAbstract.setHasAttacked(false);
                soldierAbstract.setHasMoved(false);
                attackButton.GetComponent<Button> ().interactable = true;
				moveButton.GetComponent<Button> ().interactable = true;
			}
            playerSoldiers.Add(soldierToAdd);
            currentSoldiers.Add(soldierToAdd);
        }
        else
        {
            soldierAbstract.setTeam("enemy");
            if (currentSoldiers.Count == 0) {
                soldierAbstract.setHasAttacked(false);
                soldierAbstract.setHasMoved(false);
                attackButton.GetComponent<Button> ().interactable = true;
				moveButton.GetComponent<Button> ().interactable = true;
			}
            enemySoldiers.Add(soldierToAdd);
            currentSoldiers.Add(soldierToAdd);
        }
    }

    private void endCurrentTurn(List<GameObject> currentList)
    {
        foreach (GameObject obj in currentList)
        {
            AbstractSoldier soldier = obj.GetComponent<AbstractSoldier>();
            soldier.endTurn();
        }

    }

    private void beginNextPlayerTurn(List<GameObject> currentSoldiers)
    {
        foreach (GameObject thisSoldier in currentSoldiers)
        {
            thisSoldier.GetComponent<AbstractSoldier>().resetSoldier();
        }
        currentSoldiers[0].GetComponent<AbstractSoldier>().beginTurn();

    }

	private void setNewStates() {
		for (int i = 0; i < currentSoldiers.Count; i++) {
			AbstractSoldier sold = currentSoldiers[i].GetComponent<AbstractSoldier> ();
			if (i == 0) {
				sold.setCurrentState (AbstractSoldier.TurnState.ACTIVE);
			} else {
				sold.setCurrentState (AbstractSoldier.TurnState.WAIT);
			}
		}
	}

	public GameObject findSoldier(string soldierState) {
        GameObject requestedSoldier = null;
		foreach(GameObject obj in currentSoldiers) {
			AbstractSoldier soldierAbstract = obj.GetComponent<AbstractSoldier> ();
			if (soldierState.Equals (soldierAbstract.getCurrentState ().ToString ())) {
				requestedSoldier = obj;
			}
		}

		return requestedSoldier;
	}

    public void addPlayerChamp(GameObject playerChamp)
    {
        playerSoldiers.Add(playerChamp);
        currentSoldiers.Add(playerChamp);
    }

    public void addEnemyChamp(GameObject enemyChamp)
    {
        enemySoldiers.Add(enemyChamp);
    }
}
