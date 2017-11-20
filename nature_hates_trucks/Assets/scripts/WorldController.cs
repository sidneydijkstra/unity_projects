using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public float moveSpeed = 5;
    public GameObject[] roads;

    public GameObject spawnpoint;

    public GameObject loadScreen;

    private ArrayList roadList = new ArrayList();

    int spawnId = 0;
    bool firstSpawn = true;
    float timer;

	void Start () {
        moveSpeed = 420;
        loadScreen.SetActive(true);
        timer = Time.time + 5;
    }
	
	void Update () {
        // load map
        if (firstSpawn && roadList.Count >= 15) {
            moveSpeed = 20;
            firstSpawn = false;
            loadScreen.SetActive(false);
        }

        // each 5 sec add 1 to movespeed
        if (Time.time >= timer) {
            timer = Time.time + 5;
            moveSpeed += 1;
            if (moveSpeed == 40){
                this.GetComponent<SpawnController>().setSpawnraid(1);
            }
            else if (moveSpeed >= 60){
                this.GetComponent<SpawnController>().setSpawnraid(0.7f);
                timer = Time.time + 120;
            }
        }
        // call funnctions
        spawnRoad();
        checkRoads();
    }
    
    void spawnRoad() {
        if (roadList.Count != 0) {
            GameObject lastRoad = (GameObject)roadList[roadList.Count - 1];
            if (spawnpoint.transform.position.z - lastRoad.transform.position.z >= 7f) {
                GameObject spawndRoad;

                if (spawnId == 0) {
                    if (randomBool()){
                        float rot = 0;
                        if (randomBool())
                            rot = 180;
                        spawndRoad = Instantiate(roads[2], new Vector3(lastRoad.transform.position.x, lastRoad.transform.position.y, lastRoad.transform.position.z + 8f), Quaternion.Euler(0, rot, 0)) as GameObject;
                    }else { 
                        spawndRoad = Instantiate(roads[0], new Vector3(lastRoad.transform.position.x, lastRoad.transform.position.y, lastRoad.transform.position.z + 8f), Quaternion.identity) as GameObject;
                    }
                    spawnId++;
                } else if (spawnId == 1 || spawnId == 2) {
                    if (randomBool()){
                        float rot = 0;
                        if (randomBool())
                            rot = 180;
                        spawndRoad = Instantiate(roads[2], new Vector3(lastRoad.transform.position.x, lastRoad.transform.position.y, lastRoad.transform.position.z + 8f), Quaternion.Euler(0, rot,0)) as GameObject;
                    }else { 
                        spawndRoad = Instantiate(roads[0], new Vector3(lastRoad.transform.position.x, lastRoad.transform.position.y, lastRoad.transform.position.z + 8f), Quaternion.identity) as GameObject;
                    }
                    spawnId++;
                }else {
                    spawndRoad = Instantiate(roads[1], new Vector3(lastRoad.transform.position.x, lastRoad.transform.position.y, lastRoad.transform.position.z + 8f), Quaternion.identity) as GameObject;
                    spawnId = 0;
                }

                spawndRoad.GetComponent<WorldMovement>().setSpeed(moveSpeed);
                roadList.Add(spawndRoad);
            }
        }else {
            GameObject spawndRoad = Instantiate(roads[1], spawnpoint.transform.position, Quaternion.identity) as GameObject;
            spawndRoad.GetComponent<WorldMovement>().setSpeed(moveSpeed);
            roadList.Add(spawndRoad);
            spawnId++;
        }
    }

    void checkRoads() {
        
        foreach (GameObject rd in roadList) {
            rd.GetComponent<WorldMovement>().setSpeed(moveSpeed);
        }
        
        foreach (GameObject rd in roadList) {
            if (rd.transform.position.z <= -30){
                roadList.Remove(rd);
                Destroy(rd);
            }else{
                rd.GetComponent<WorldMovement>().move();

            }
        }
    }

    bool randomBool() {
        int rand = Random.Range(0, 100);
        if (rand <= 20) {
            return true;
        } else {
            return false;
        }
    }

    // get move speed
    public float getMovespeed() {
        return moveSpeed;
    }

    
}
