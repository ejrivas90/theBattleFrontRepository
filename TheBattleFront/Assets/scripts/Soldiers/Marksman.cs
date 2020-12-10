using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Marksman : AbstractSoldier
{
    void Start()
    {
        Debug.Log("Marksman State Machine Initiated");
        setAtkRange(2);
    }

    public override void init(string currentPlayer)
    {
        setName(currentPlayer + "Marksman");
        setSoldierType("Marksman");
        setSoldierVector(this.transform.position);
        Vector3 soldierVector = getSoldierVector();
        transform.position = new Vector3(soldierVector.x, 0.5f, soldierVector.z);
        setCurrentHealth(1);
        setAttackPower(20);
        setAtkDie(6);
        setCurrentState(TurnState.WAIT);
    }

    void Update()
    {
        setSoldierVector(this.transform.position);
    }

    public override void beginTurn()
    {
        Debug.Log("Marskman turn has just begun");
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
            Debug.Log("Marksman has " + getCurrentStamina() + " movement points");
            hasDiceBeenRolled = true;
        }
    }

    public override int atkRoll()
    {
        return DiceRoll.roll6Die();
    }

}