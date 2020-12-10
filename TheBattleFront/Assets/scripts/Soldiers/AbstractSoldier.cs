using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractSoldier : MonoBehaviour {
    public enum TurnState { ATTACK, MOVE, WAIT, DEAD, ACTIVE, BASE, HAS_MOVED }
    public TurnState currentState;
    public int currentHealth;
    public int attackPower;
    public int currentStamina;
    public string soldierType;
    public Vector3 soldierVector;
    public int atkRange;
    public int atkDie;
    public Boolean hasMoved;
    public Boolean hasAttacked;
    public Boolean isActive;
    public string team;
    public Camera myCamera;
    public bool hasDiceBeenRolled;

    public AbstractSoldier() {

    }
    public virtual void init(string name) { }

    public virtual void init() { }

    public virtual void takeDamage(int damageTaken) {
        currentHealth = currentHealth - damageTaken;
    }

    public virtual void takeDamage() { }

    public void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return name;
    }

    public void setCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setAttackPower(int attackPower)
    {
        this.attackPower = attackPower;
    }

    public int getAttackPower()
    {
        return attackPower;
    }

    public void setCurrentStamina(int currentStamina)
    {
        this.currentStamina = currentStamina;
    }

    public int getCurrentStamina()
    {
        return currentStamina;
    }

    public void setCurrentState(TurnState state)
    {
        this.currentState = state;
    }

    public TurnState getCurrentState()
    {
        return currentState;
    }

    public void setSoldierType(string soldierType)
    {
        this.soldierType = soldierType;
    }

    public string getSoldierType()
    {
        return soldierType;
    }

    public virtual void rollDie() { }

    public void setSoldierVector(Vector3 soldierVector)
    {
        this.soldierVector = soldierVector;
    }

    public Vector3 getSoldierVector()
    {
        return soldierVector;
    }

    public void setAtkRange(int atkRange)
    {
        this.atkRange = atkRange;
    }

    public int getAtkRange()
    {
        return atkRange;
    }

    public void setAtkDie(int atkDie)
    {
        this.atkDie = atkDie;
    }

    public int getAtkDie()
    {
        return atkDie;
    }

    public virtual int atkRoll()
    {
        return 0;
    }

    public void setHasMoved(Boolean hasMoved) {
        this.hasMoved = hasMoved;
    }

    public Boolean getHasMoved() {
        return hasMoved;
    }

    public void destroySoldier()
    {
        Destroy(this.gameObject);
    }

    public virtual void beginTurn()
    {
        setCurrentState(TurnState.ACTIVE);
        hasDiceBeenRolled = false;
        setCurrentStamina(0);
        getMyCamara().enabled = true;
        getMyCamara().GetComponent<CameraFollow>().setSoldierToFollow(gameObject);
    }

    public virtual void endTurn()
    {
        setCurrentState(TurnState.WAIT);
        getMyCamara().enabled = false;
    }

    public void setHasAttacked(Boolean hasAttacked)
    {
        this.hasAttacked = hasAttacked;
    }

    public Boolean getHasAttacked()
    {
        return hasAttacked;
    }

    public void resetSoldier() {
        setHasAttacked(false);
        setHasMoved(false);
        setCurrentState(TurnState.WAIT);
    }

    public void setTeam(string team)
    {
        this.team = team;
    }

    public string getTeam()
    {
        return team;
    }

    public void setMyCamera(Camera cam)
    {
        this.myCamera = cam;
    }

    public Camera getMyCamara()
    {
        return myCamera;
    }

    public void setHasDiceBeenRolled(bool hasBeenRolled)
    {
        this.hasDiceBeenRolled = hasBeenRolled;
    }

    public bool getHasDiceBeenRolled()
    {
        return hasDiceBeenRolled;
    }
    
}
