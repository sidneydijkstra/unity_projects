using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLIve : MonoBehaviour {


    public GameObject node;
    public int gridMaxX;
    public int gridMaxY;
    private GameObject[,] grid;

    void Start() {
        spawnGrid();
    }

    float timer = Time.time + 0.5f;
	void Update () {
        if (Time.time >= timer) {
            timer = Time.time + 0.0f;
            checkNodes();
        }
	}

    void checkNodes() {
        GameObject[,] _grid = grid;
        for (int x = 0; x < gridMaxX; x++){
            for (int y = 0; y < gridMaxY; y++){
                ArrayList neighbours = getNeighbour(grid[x,y]);
                int aliveNeigbours = 0;
                foreach (GameObject neighbours_ in neighbours) {
                    if (neighbours_.GetComponent<Node>().alive) {
                        aliveNeigbours++;
                    }
                }
                // rules
                if (grid[x, y].GetComponent<Node>().alive) {
                    if (aliveNeigbours < 2) {
                        _grid[x, y].GetComponent<Node>().alive = false;
                    } else if (aliveNeigbours == 2 || aliveNeigbours == 3) {
                        continue;
                    } else if (aliveNeigbours > 3) {
                        _grid[x, y].GetComponent<Node>().alive = false;
                    }
                } else if (aliveNeigbours == 3) {
                    _grid[x, y].GetComponent<Node>().alive = true;
                }
            }
        }
        grid = _grid;
    }



    // grid and neighbour functions
    void spawnGrid() {
        grid = new GameObject[gridMaxX,gridMaxY];
        for (int x = 0; x < gridMaxX; x++){
            for (int y = 0; y < gridMaxY; y++){
                Vector3 pos = new Vector3( x, y, 0);
                GameObject obj = GameObject.Instantiate(node, pos, Quaternion.identity) as GameObject;
                obj.GetComponent<Node>().setGridPos(x, y);
                obj.GetComponent<Node>().alive = randomBool();
                grid[x,y] = obj;
            }
        }
        print(grid.Length);
    }

    ArrayList getNeighbour(GameObject node_) {
        ArrayList neighbour = new ArrayList();
        Node script = node_.GetComponent<Node>();
        int x = script.gridX;
        int y = script.gridY;
        
        if (x - 1 != -1) neighbour.Add(grid[x - 1, y]);
        if (y - 1 != -1) neighbour.Add(grid[x, y - 1]);
        if (x + 1 != gridMaxX) neighbour.Add(grid[x + 1, y]);
        if (y + 1 != gridMaxY) neighbour.Add(grid[x, y + 1]);
        
        
        if (x - 1 != -1 && y - 1 != -1) neighbour.Add(grid[x - 1, y - 1]);
        if (x - 1 != -1 && y + 1 != gridMaxY) neighbour.Add(grid[x - 1, y + 1]);
        if (x + 1 != gridMaxX && y + 1 != gridMaxY) neighbour.Add(grid[x + 1, y + 1]);
        if (x + 1 != gridMaxX && y - 1 != -1) neighbour.Add(grid[x + 1, y - 1]);
        
        return neighbour;
    }

    bool randomBool() {
        float rand = Random.Range(0, 5);
        if (rand <= 1) {
            return true;
        }else {
            return false;
        }
    }
}
