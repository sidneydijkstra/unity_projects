using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// load level \\
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelLoader : MonoBehaviour {

    // canvas \\
    [Header("Canvas component")]
    [SerializeField]
    private Dropdown dropdown_levellist;
    [SerializeField]
    private Button button_loadlevel;
    [SerializeField]
    private Button button_startediter;

    // file name list \\
    FileInfo[] levelnames;

    // get level editer \\
    LevelEditor lEditer;

	void Start () {
        // active hud
        dropdown_levellist.transform.parent.gameObject.SetActive(true);

        // load level editer script
        lEditer = this.GetComponent<LevelEditor>();

        // load dropdown info
        loadDropdown();

        // set button listener
        button_loadlevel.onClick.AddListener(loadLevel);
        button_startediter.onClick.AddListener(startEditer);
	}

    // **************************** canvas controller **************************** \\

    void loadDropdown() {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        levelnames = dir.GetFiles("*.*");
        dropdown_levellist.ClearOptions();
        foreach (FileInfo file in levelnames) {
            dropdown_levellist.options.Add(new Dropdown.OptionData() { text = file.Name });
        }
    }

    // **************************** start editer **************************** \\
    void startEditer() {
        lEditer.enabled = true;
        dropdown_levellist.transform.parent.gameObject.SetActive(false);
    }


    // **************************** level loader **************************** \\
    void loadLevel() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open("" + levelnames[dropdown_levellist.value], FileMode.Open);
        string level = (string)bf.Deserialize(file);
        string[] levelArray = level.Split(","[0]);

        int gridX = int.Parse(levelArray[0]);
        int gridY = int.Parse(levelArray[1]);

        GameObject[] grid = lEditer.getTiles();
        int int_ = 2;

        for (int y = 0; y < gridX; y++){
            for (int x = 0; x < gridY; x++){
                Vector3 pos = new Vector3(x, y, 0);
                int type = int.Parse(levelArray[int_]);
                if (type != -1) Instantiate(grid[type], pos, Quaternion.identity);
                int_++;
            }
        }

        dropdown_levellist.transform.parent.gameObject.SetActive(false);

    }
}


