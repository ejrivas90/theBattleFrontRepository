using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank : AbstractSoldier
{
    void Start()
    {
        Debug.Log("Tank State Machine Initiated");
        setAtkRange(3);
    }

    public override void init(string currentPlayer)
    {
        setName(currentPlayer + "Tank");
        setSoldierType("Tank");
        setSoldierVector(this.transform.position);
        Vector3 soldierVector = getSoldierVector();
        transform.position = new Vector3(soldierVector.x, 0.5f, soldierVector.z);
        setCurrentHealth(1);
        setAttackPower(30);
        setAtkDie(8);
        setCurrentState(TurnState.WAIT);
    }

    void Update()
    {
        setSoldierVector(this.transform.position);
    }

    public override void beginTurn()
    {
        Debug.Log("Tank turn has just begun");
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
            setCurrentStamina(DiceRoll.roll4Die());
            Debug.Log("Tank has " + getCurrentStamina() + " movement points");
            hasDiceBeenRolled = true;
        }
    }

    public override int atkRoll()
    {
        return DiceRoll.roll8Die();
    }
}