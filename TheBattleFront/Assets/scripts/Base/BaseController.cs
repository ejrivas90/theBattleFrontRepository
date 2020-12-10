using UnityEngine;
using System.Collections;

public class BaseController : AbstractSoldier
{
    public int baseHealth;

	// Use this for initialization
	void Start () {
        setCurrentState(TurnState.BASE);
	}
	
	// Update is called once per frame
	void Update () {
        if(getCurrentHealth() < 1)
        {
            Destroy(this.gameObject);
        }
	}

    public override void takeDamage() {
        setCurrentHealth(getCurrentHealth() - 1);
    }

    public void initBase(string player, int health)
    {
        setSoldierType(player + "Base");
        setCurrentHealth(health);
    }    
}
