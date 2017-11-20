using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool isWalkable = true;

    public int gridX;
    public int gridY;

    public int gCost; // dis from start to tile
    public int hCost; // dis from end to tile
    public int fCost; // gCost + hCost

    public GameObject parent;

    public void setGridAxes(int x, int y) {
        gridX = x;
        gridY = y;
    }

    // debug
    void Update() {
        if (!isWalkable) {
            this.GetComponent<Renderer>().material.color = Color.black;
        } 
    }

}


