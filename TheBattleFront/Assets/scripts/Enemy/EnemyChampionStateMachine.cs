using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyChampionStateMachine : AbstractSoldier
{
    private GameObject champTileLocation;

    void Start()
    {
        Debug.Log("Enemy Champ State Machine Initiated");
        setAtkRange(2);
    }

    public override void init()
    {
        setName("enemyChampion");
        setSoldierType("Champion");
        champTileLocation = (GameObject)GameObject.Find("square: 6,1");
        setSoldierVector(GameObject.Find("square: 6,1").transform.position);
        Vector3 champVector = getSoldierVector();
        transform.position = new Vector3(champVector.x, 0f, champVector.z);
        setCurrentHealth(100);
        setAttackPower(50);
        setAtkDie(6);
        setTeam("enemy");
        setCurrentState(TurnState.WAIT);
        Debug.Log("champ is at " + champTileLocation);
    }

    void Update()
    {
        //Debug.Log("champ vector: " + this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z);
        setSoldierVector(this.transform.position);
    }
    /*
    public override void beginTurn()
    {
        Debug.Log("Enemy turn has just begun");
        Debug.Log("Champ is at " + champTileLocation);
        setCurrentState(TurnState.ACTIVE);
        hasDiceBeenRolled = false;
        setCurrentStamina(0);
        getMyCamara().enabled = true;
    }

    public void endTurn()
    {
        setCurrentState(TurnState.WAIT);
        getMyCamara().enabled = false;
    }
    */
    public override void rollDie()
    {
        if (!hasDiceBeenRolled)
        {
            setCurrentState(TurnState.MOVE);
            setCurrentStamina(DiceRoll.roll8Die());
            Debug.Log("Champ has " + getCurrentStamina() + " movement points");
            hasDiceBeenRolled = true;
        }
    }

    public GameObject getChampTileLocation()
    {
        return champTileLocation;
    }

    public override int atkRoll()
    {
        return Utilities.roll6Die();
    }
}
