using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [Header("Normal Menu Config:")]
    public Button btOptions;

    [Header("Options Menu Config:")]
    public Button btExitOptions;

    [Header("GameObject Menu Config:")]
    public GameObject gbMain;
    public GameObject gbOptions;
    
    public GameObject gbSpawner;
    public GameObject gbHearth;
    public GameObject gbRagebar;
    public GameObject gbPoints;

    public GameObject gbCar;


    [Header("Dead Config:")]
    public Text txtPoint;
    public GameObject gbDead;
    public Button btRestart;


    private bool gameActive = false;

    private Vector3 pos;

    void Start () {
        // set main active and rest not active
        gbMain.SetActive(true);
        gbOptions.SetActive(false);

        // set hud screen active false
        gbHearth.SetActive(false);
        gbRagebar.SetActive(false);
        gbPoints.SetActive(false);

        // set dead screen active false
        gbDead.SetActive(false);
        
        // main menu
        btOptions.onClick.AddListener(onClickBtOptions);

        // restart game bt bind
        btRestart.onClick.AddListener(onClickBtRestart);

        // exit options to start
        btExitOptions.onClick.AddListener(onClickBtExitOptions);

        //set menu move pos
        pos.x = -Screen.width + 100;
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !gameActive) {
            onClickBtStart();
            Camera.main.GetComponent<AudioController>().musicGame();
        }

        if (gameActive && gbMain.GetComponent<RectTransform>().position.x >= pos.x) {
            // move menu
            gbMain.GetComponent<RectTransform>().position -= new Vector3(500,0,0) * Time.deltaTime;
        }else if(gameActive){
            // activate hud
            gbHearth.SetActive(true);
            gbRagebar.SetActive(true);
            gbPoints.SetActive(true);
        }
	}

    // main menu buttons
    private void onClickBtStart(){
        // set game active
        gameActive = true;

        // deactivate menu's
        //gbMain.SetActive(false);
        gbOptions.SetActive(false);


        // activate car spawner
        gbSpawner.GetComponent<SpawnController>().enabled = true;

        // activate car "player"
        gbCar.GetComponent<CarController>().enabled = true;
    }

    private void onClickBtOptions(){
        gbMain.SetActive(false);
        gbOptions.SetActive(true);
    }

    // options menu buttons
    private void onClickBtExitOptions() {
        gbOptions.SetActive(false);
        gbMain.SetActive(true);
    }

    // activate dead screen
    private void onClickBtRestart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    // if player dead
    public void activateDeadScreen(int points){
        // set hud not active
        gbHearth.SetActive(false);
        gbRagebar.SetActive(false);
        gbPoints.SetActive(false);

        // set texts
        txtPoint.text = "You got: " + points + "points!";

        // set dead screen active
        gbDead.SetActive(true);
    }
}
