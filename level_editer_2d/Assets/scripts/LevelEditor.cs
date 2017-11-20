using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// save level \\
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelEditor : MonoBehaviour {

    // gameobjects \\
    [Header("Tile's")]
    [SerializeField]
	private GameObject placeHolderTile;
    [SerializeField]
    private GameObject[] tiles;
    private int currentTile = 0;

    // grid options \\
    [Header("Grid settings")]
    [SerializeField]
    private int gridMaxY;
    [SerializeField]
    private int gridMaxX;
    private GameObject[] grid;

    // canvas \\
    [Header("Canvas component")]
    [SerializeField]
    private Text text_postition;
    [SerializeField]
    private Text text_editmode;
    [SerializeField]
    private Text text_currenttile;
    [SerializeField]
    private Text text_filename;
    [SerializeField]
    private Button button_savelevel;

    // mode \\
    private int currentMode = 0;

    // camera gameobject \\
    private Camera cam;

    void Start () {

        // *************** \\
        Input.location.Start();
        // *************** \\

        // active hud
        text_postition.transform.parent.gameObject.SetActive(true);

        // set button listener
        button_savelevel.onClick.AddListener(saveGame);

        // set grid max
        grid = new GameObject[gridMaxY * gridMaxX];
        // set camera
        cam = Camera.main;
        // spawn grid
        spawnGrid();   
	}

    void Update() {

        

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            saveGame();
        }

        // get key input
        modeController();
        tileController();

        // get mouse input
        mouseClick();

        // move camera
        moveCamera();

        // change canvas
        canvasController();
    }
    // **************************** tile controller **************************** \\
    void tileController() {
        if (Input.GetKeyDown(KeyCode.Q) && currentTile-1 >= 0) currentTile--;
        if (Input.GetKeyDown(KeyCode.E) && currentTile+1 < tiles.Length) currentTile++;
    }

    // **************************** mode controller **************************** \\
    void modeController() {
        if (Input.GetKey(KeyCode.Z)) currentMode = 1;
        if (Input.GetKey(KeyCode.X)) currentMode = 2;
        if (Input.GetKey(KeyCode.C)) currentMode = 3;
    }

    void mouseClick() {
        if (Input.GetMouseButton(0)) {
            if (currentMode == 1) modePlaceTile(); else
            if (currentMode == 2) modeDelTile();else
            if (currentMode == 3) modeCopyTile();
        }
    }

    // place tile
    void modePlaceTile() {
        Transform tile = RayGet();
        if (tile != null) {
            Vector3 pos = new Vector3(tile.position.x, tile.position.y, 0);
            Destroy(tile.gameObject);
            GameObject obj = Instantiate(tiles[currentTile], pos, Quaternion.identity) as GameObject;
            obj.name = tiles[currentTile].name;
            obj.transform.parent = this.transform;
            grid[getGridPos((int)tile.position.x, (int)tile.position.y)] = obj;
        }
    }

    // delete tile
    void modeDelTile() {
        Transform tile = RayGet();
        if (tile != null)
        {
            Vector3 pos = new Vector3(tile.position.x, tile.position.y, 0);
            Destroy(tile.gameObject);
            GameObject tile_ = Instantiate(placeHolderTile, pos, Quaternion.identity) as GameObject;
            tile_.name = placeHolderTile.name;
            tile.transform.parent = this.transform;
            grid[getGridPos((int)tile.position.x, (int)tile.position.y)] = tile_;
        }
    }

    // copy tile
    void modeCopyTile(){
        Transform tile = RayGet();
        if (tile != null){
            for (int i = 0; i < tiles.Length; i++){
                if (tiles[i].name == tile.name) {
                    currentTile = i;
                    break;
                }
            }
        }
    }

    // **************************** canvas controller **************************** \\
    void canvasController() {
        // text position
        if (RayGet() != null) text_postition.text = "Position: " + RayGet().position.x + "X," + RayGet().position.y + "Y";

        // text mode
        if (currentMode == 1) text_editmode.text = "Mode: place tile"; else
        if (currentMode == 2) text_editmode.text = "Mode: del tile";else
        if (currentMode == 3) text_editmode.text = "Mode: copy tile";else
        if (currentMode == 4) text_editmode.text = "Mode: fill tile"; else
            text_editmode.text = "Mode: NULL";

        // text current tile
        if (tiles.Length != 0) text_currenttile.text = "Tile: " + tiles[currentTile].name;
    }

    // **************************** spawn grid **************************** \\
    void spawnGrid() {
        int int_ = 0;
        for (int y = 0; y < gridMaxY; y++){
            for (int x = 0; x < gridMaxX; x++) {

                // spawn positions
                Vector3 pos = new Vector3(x, y, 0);

                // spawn tile
                GameObject tile = Instantiate(placeHolderTile, pos, Quaternion.identity) as GameObject;
                tile.name = placeHolderTile.name;
                tile.transform.parent = this.transform;
                grid[int_] = tile;
                int_++;
            }
        }
    }

    // grid calculations \\

    int getGridPos(int x, int y) {
        return ((y * gridMaxX) + x);
    }
    // **************************** camera movement **************************** \\

    void moveCamera() {
        if (Input.GetKey(KeyCode.W)) cam.transform.position += new Vector3(0,10,0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) cam.transform.position -= new Vector3(0,10, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) cam.transform.position += new Vector3(10, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) cam.transform.position -= new Vector3(10, 0, 0) * Time.deltaTime;
    }

    // **************************** mouse look controller **************************** \\
    public Transform RayGet() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.red);
                return hit.collider.transform;
            }
            else
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.red);
            }
        }
        return null;
    }

    // **************************** save level **************************** \\
    void saveGame() {
        // format level \\
        string str = gridMaxX + "," + gridMaxY + ",";
        for (int g = 0; g < grid.Length; g++){
            for (int t = 0; t < tiles.Length; t++){
                if (grid[g].name == tiles[t].name) {
                    str += t + ",";
                    break;
                }
                else if (grid[g].name == placeHolderTile.name) {
                    str += "-1,";
                    break;
                }
            }
        }

        // save levle \\
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + text_filename.text + ".gd");
        bf.Serialize(file, str); 
        file.Close();
        print(Application.persistentDataPath);
    }

    public GameObject[] getTiles() {
        return tiles;
    }
}
