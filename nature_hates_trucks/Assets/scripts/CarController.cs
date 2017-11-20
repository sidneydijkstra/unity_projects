using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour {

    [Header("Car Config:")]
    public float moveSpeed = 5;

    [Header("RageBar Config:")]
    public RawImage bar;
    public float barsize;

    [Header("Health Config:")]
    public RawImage[] hearts;
    private int currenthp = 2; // 0, 1, 2 (3 hp)

    [Header("Points Config:")]
    public Text pointText;
    public Text pointPopupText;
    private int points;

    // no hit timer
    private float hitTimer;
    private float blinkTimer;
    private bool blinking = false;
    public RawImage boostImage;

    // hud / GUI
    public GameObject GUI;

    private float counter;
    private int carPos = 1; // |0|1|2| (road)

    // jumping
    bool jumping = false;

    void Start (){
        setBar(0);
        counter = Time.time + 1f;
    }

    void Update(){
        // move car
        carMovement();

        // bar system
        if (Time.time >= counter && barsize < 100){
            counter = Time.time + 1f;
            addBar(-1);
        }else if (barsize >= 100) { // boost controller
            noHitTimer(4);
            setBar(0);
            boostImage.color = new Vector4(255, 255, 255, 1f);
        }

        // boost system
        if (boostImage.color.a > 0) {
            boostImage.color = new Vector4(255,255,255, boostImage.color.a - 0.005f);
        }

        // point system
        if (pointPopupText.color.a > 0) {
            pointPopupText.color = new Vector4(255,255,255, pointPopupText.color.a - 0.01f);
        }

        // blink system
        if (Time.time >= blinkTimer && Time.time < hitTimer){
            blinkTimer = (hitTimer - Time.time) / 10;
            blink(!blinking);
        }else if (Time.time >= hitTimer && !blinking){
            blink(true);
        }

        // vinger movement
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < (Screen.height / 3) * 2) {
            print("left");
        }

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x >= Screen.width / 2 && Input.mousePosition.y < (Screen.height / 3) * 2) {
            print("right");
        }

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y > (Screen.height / 3) * 2){
            print("up");
        }

    }



    void carMovement() {
        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && !jumping && this.transform.position.y <= 0 || Input.GetMouseButtonDown(0) && Input.mousePosition.y > (Screen.height / 3) * 2 && !jumping && this.transform.position.y <= 0) {
            jumping = true;
        }

        if (jumping && this.transform.position.y < 5) {
            this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 5, transform.position.z), 12 * Time.deltaTime);
        } else if(this.transform.position.y > 0){
            jumping = false;
            this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 12 * Time.deltaTime);
        }

        // get key input
        if (Input.GetKeyDown(KeyCode.A) && carPos != 0 || Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < (Screen.height / 3) * 2&& carPos != 0){
            carPos--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && carPos != 2 || Input.GetMouseButtonDown(0) && Input.mousePosition.x >= Screen.width / 2 && Input.mousePosition.y < (Screen.height / 3) * 2 && carPos != 2) {
            carPos++;
        }

        // move car
        if (carPos == 0 && transform.position != new Vector3(6.75f, transform.position.y,transform.position.z)) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-6.75f, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }else if (carPos == 1 && transform.position != new Vector3(0, transform.position.y, transform.position.z)) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }else if (carPos == 2 && transform.position != new Vector3(-6.75f, transform.position.y, transform.position.z)) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(6.75f  , transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    // bar fucntions
    public void setBar(float num) {
        if (num > 100) {
            num = 100;
        }
        bar.rectTransform.sizeDelta = new Vector2(num * 3, 21.5f);

        barsize = num;
    }

    public void addBar(float num){
        if (barsize + num > 100){
            barsize = 100;
        }else if(barsize + num < 0) {
            barsize = 0;
        }else {
            barsize += num;
        }
        bar.rectTransform.sizeDelta = new Vector2(barsize * 3, 21.5f);
    }

    // hearts functions
    float infTimer = 0;
    public void removeHeart() {
        if (Time.time >= hitTimer) {
            Destroy(hearts[currenthp]);
            currenthp--;

            if (currenthp < 0)
                death();

            noHitTimer(2);
        }
    }

    // points functions
    public void addPoints(int points_) {
        points += points_;
        pointText.text = "" + points;
        pointPopupText.color = new Vector4(255, 255, 255, 1);
        pointPopupText.text = "+" + points_;
    }

    // no hit timer
    public void noHitTimer(float timer_) {
        hitTimer = Time.time + timer_;
        blinkTimer = (hitTimer - Time.time) / 10;
    }

    private void blink(bool blink_) {
        for (int i = 0; i < this.transform.childCount; i++){
            this.transform.GetChild(i).gameObject.SetActive(blink_);
        }
        blinking = blink_;
    }

    // player death
    void death() {
        Destroy(this.gameObject);
        GUI.GetComponent<MenuController>().activateDeadScreen(points);
    }
    
}
