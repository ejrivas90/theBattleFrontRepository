using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Artillery : AbstractSoldier
{
    void Start()
    {
        Debug.Log("Artillery State Machine Initiated");
        setAtkRange(4);
    }

    public override void init(string currentPlayer)
    {
        setName(currentPlayer + "Artillery");
        setSoldierType("Artillery");
        setSoldierVector(this.transform.position);
        Vector3 soldierVector = getSoldierVector();
        transform.position = new Vector3(soldierVector.x, 0.5f, soldierVector.z);
        setCurrentHealth(1);
        setAttackPower(40);
        setAtkDie(10);
        setCurrentState(TurnState.WAIT);
    }

    void Update()
    {
        setSoldierVector(this.transform.position);
    }

    public override void beginTurn()
    {
        Debug.Log("Artillery turn has just begun");
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
            Debug.Log("Artillery has " + getCurrentStamina() + " movement points");
            hasDiceBeenRolled = true;
        }
    }

    public override int atkRoll()
    {
        return DiceRoll.roll10Die();
    }
}