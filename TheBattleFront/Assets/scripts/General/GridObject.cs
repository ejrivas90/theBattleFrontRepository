using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridObject {
    private GameObject plane;
    private GameObject occupiedSoldier;
    public Material[] materials;

    public bool isSquareOccupied()
    {
        if (occupiedSoldier == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void setPlane(GameObject plane)
    {
        this.plane = plane;
    }

    public GameObject getPlane()
    {
        return plane;
    }

    public void setOccupiedSoldier(GameObject occupiedSoldier)
    {
        this.occupiedSoldier = occupiedSoldier;
    }

    public GameObject getOccupiedSoldier()
    {
        return occupiedSoldier;
    }

    public Vector3 getSquarePosition()
    {
        return this.plane.transform.position;
    }
}
