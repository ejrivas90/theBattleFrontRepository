using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClicked : MonoBehaviour {

    public float x;
    public float z;
    private MoveAction moveAction;
    private Attack attackAction;
    private RecruitButton recruitAction;

    private void Start()
    {
        moveAction = GameObject.Find("actionPanel").GetComponent<MoveAction>();
        attackAction = GameObject.Find("actionPanel").GetComponent<Attack>();
        recruitAction = GameObject.Find("actionPanel").GetComponent<RecruitButton>();
    }

    public TileClicked(Vector3 tileVector)
    {
        this.x = tileVector.x;
        this.z = tileVector.z;
    }

    private void OnMouseDown()
    {
        bool recruitButtonClicked = recruitAction.getHasBeenClicked();
        bool attackButtonClicked = attackAction.getAttackButtonClicked();
        bool moveButtonClicked = moveAction.getHasButtonBeenClicked();
        Debug.Log("tile clicked was: " + x + ", " + z);

        if (recruitButtonClicked)
        {
            recruitAction.handleTileClick(x, z);
        }
        if(attackButtonClicked)
        {
            attackAction.handleTileClick(x, z);
        }
        if(moveButtonClicked)
        {
            moveAction.handleTileClicked(x, z);
        }      
    }

    public void setX(float x)
    {
        this.x = x;
    }

    public float getX()
    {
        return x;
    }

    public void setZ(float z)
    {
        this.z = z;
    }

    public float getZ()
    {
        return z;
    }

}
