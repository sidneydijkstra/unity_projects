using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

    public bool calculate = false;

    public GameObject tile;

    public int gridMaxX;
    public int gridMaxY;

    public GameObject[,] grid;

	void Start (){
        spawnGrid();
        Astar();
    }

	void Update (){
        if (calculate) {
            for (int x = 0; x < gridMaxX; x++){
                for (int y = 0; y < gridMaxY; y++){
                    grid[x, y].GetComponent<Renderer>().material.color = Color.white;
                }
            }
            Astar();
            calculate = false;
        }

    }

    void Astar() {
        // debug START
        GameObject s = grid[0, 0];
        GameObject n = grid[9, 9];

        grid[3, 3].GetComponent<Tile>().isWalkable = false;
        // debug END

        ArrayList open = new ArrayList();
        ArrayList closed = new ArrayList();

        open.Add(s);
        while (open.Count > 0) {
            // get the best tile you can chose
            GameObject currentTile = null;
            float fCost = Mathf.Infinity;
            foreach (GameObject obj in open) {
                if (obj.GetComponent<Tile>().fCost <= fCost) {
                    fCost = obj.GetComponent<Tile>().fCost;
                    currentTile = obj;
                }
            }

            currentTile.GetComponent<Renderer>().material.color = Color.grey;

            // remove current tile from open and add it to closed
            open.Remove(currentTile);
            closed.Add(currentTile);

            // check if you are at the end
            if (currentTile == n) {
                print("end found");
                break;
            }

            // search new tiles
            foreach (GameObject neighbour in getNeighbour(currentTile)) {

                // check if in closed
                if (closed.Contains(neighbour) || !neighbour.GetComponent<Tile>().isWalkable) {
                    continue;
                }

                int costToNeighbour = currentTile.GetComponent<Tile>().gCost + GetDistance(currentTile, neighbour);
                if (currentTile.GetComponent<Tile>().gCost < costToNeighbour || !open.Contains(neighbour)) {
                    neighbour.GetComponent<Tile>().gCost = costToNeighbour;
                    neighbour.GetComponent<Tile>().hCost = GetDistance(neighbour, n);
                    neighbour.GetComponent<Tile>().fCost = costToNeighbour + GetDistance(neighbour, n);
                    neighbour.GetComponent<Tile>().parent = currentTile;

                    if (!open.Contains(neighbour)) {
                        open.Add(neighbour);
                    }
                }


            }
        }

        ArrayList path = new ArrayList();
        GameObject currentNode = n;

        while (currentNode != s)
        {
            path.Add(currentNode);
            currentNode = currentNode.GetComponent<Tile>().parent;
        }
        path.Reverse();

        foreach (GameObject obj in path){
            obj.GetComponent<Renderer>().material.color = Color.blue;
        }

        s.GetComponent<Renderer>().material.color = Color.green;
        n.GetComponent<Renderer>().material.color = Color.red;
    }

    void spawnGrid() {
        grid = new GameObject[gridMaxX,gridMaxY];
        for (int x = 0; x < gridMaxX; x++){
            for (int y = 0; y < gridMaxY; y++){
                Vector3 pos = new Vector3( x, y, 0);
                GameObject obj = GameObject.Instantiate(tile, pos, Quaternion.identity) as GameObject;
                obj.GetComponent<Tile>().setGridAxes(x, y);
                grid[x,y] = obj;
            }
        }

        print(grid.Length);
    }

    ArrayList getNeighbour(GameObject tile_) {
        ArrayList neighbour = new ArrayList();
        Tile script = tile_.GetComponent<Tile>();
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

    int GetDistance(GameObject tileA, GameObject tileB){
        int x = tileA.GetComponent<Tile>().gridX - tileB.GetComponent<Tile>().gridX;
        int y = tileA.GetComponent<Tile>().gridY - tileB.GetComponent<Tile>().gridY;

        if (x < 0)
            x *= -1;

        if (y < 0)
            y *= -1;

        return 14 * y + 10 * (x - y);
    }
}