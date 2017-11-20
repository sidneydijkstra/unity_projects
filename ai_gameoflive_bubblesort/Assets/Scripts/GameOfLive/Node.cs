using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public int gridX;
    public int gridY;

    public bool alive = true;

    void Update () {
        if (alive) {
            this.GetComponent<Renderer>().material.color = Color.white;
        }else {
            this.GetComponent<Renderer>().material.color = Color.black;
        }
	}

    public void setGridPos(int x, int y) {
        gridX = x;
        gridY = y;
    }
}
