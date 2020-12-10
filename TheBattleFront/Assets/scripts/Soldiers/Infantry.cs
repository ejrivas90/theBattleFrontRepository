using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Infantry : AbstractSoldier
{
    void Start()
    {
        Debug.Log("Infantry State Machine Initiated");
        setAtkRange(1);
    }

    public override void init(string currentPlayer)
    {
        setName(currentPlayer + "Infantry");
        setSoldierType("Infantry");
        setSoldierVector(this.transform.position);
        Vector3 soldierVector = getSoldierVector();
        transform.position = new Vector3(soldierVector.x, 0.5f, soldierVector.z);
        setCurrentHealth(1);
        setAttackPower(10);
        setAtkDie(4);
        setCurrentState(TurnState.WAIT);
    }

    void Update()
    {
        setSoldierVector(this.transform.position);
    }

    public override void beginTurn()
    {
        Debug.Log("Infantry turn has just begun");
        setCurrentState(TurnState.ACTIVE);
        hasDiceBeenRolled = false;
        setCurrentStamina(0);
    }

    public void endTurn()
    {
        setCurrentState(TurnState.WAIT);
    }

    public override void rollDie()
    {
        if (!hasDiceBeenRolled)
        {
            setCurrentState(TurnState.MOVE);
            setCurrentStamina(DiceRoll.roll6Die());
            Debug.Log("Infantry has " + getCurrentStamina() + " movement points");
            hasDiceBeenRolled = true;
        }
    }

    public override int atkRoll()
    {
        return DiceRoll.roll4Die();
    }
}