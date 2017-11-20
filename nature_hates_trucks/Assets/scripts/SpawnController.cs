using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject[] objects;
    public Transform[] spawnpoints;
    public float spawnraid;

	void Start () {
		
	}

    float current = 0;
	void Update () {
        if (Time.time >= current) {
            current = Time.time + spawnraid;
            spawnObject();
        }
	}

    void spawnObject() {
        for (int i = 0; i < spawnpoints.Length; i++) {
            if (ifTrue()) {
                int rand = Random.Range(0, objects.Length);
                GameObject obj;
                float extraSpeed;
                if (objects[rand].name == "redcar"){
                    obj = Instantiate(objects[rand], spawnpoints[i].position, Quaternion.Euler(-90f, 180f, 0f)) as GameObject;
                    obj.GetComponent<CarMovement>().barAddPoints = 5f;
                    obj.GetComponent<CarMovement>().addPoints = 200;
                    extraSpeed = 15;
                }
                else if (objects[rand].name == "slowcar"){
                    obj = Instantiate(objects[rand], spawnpoints[i].position, Quaternion.Euler(-90f, 180f, 0f)) as GameObject;
                    obj.GetComponent<CarMovement>().barAddPoints = 2.5f;
                    obj.GetComponent<CarMovement>().addPoints = 100;
                    extraSpeed = 10;
                }
                else if (objects[rand].name == "minicar"){
                    obj = Instantiate(objects[rand], spawnpoints[i].position, Quaternion.Euler(0, 180f, 0f)) as GameObject;
                    obj.GetComponent<CarMovement>().barAddPoints = 10f;
                    obj.GetComponent<CarMovement>().addPoints = 400;
                    extraSpeed = 32;
                }
                else {
                    obj = Instantiate(objects[rand], spawnpoints[i].position, Quaternion.Euler(0, 180f, 0f)) as GameObject;
                    obj.GetComponent<CarMovement>().barAddPoints = 5f;
                    obj.GetComponent<CarMovement>().addPoints = 200;
                    extraSpeed = 0;
                }
                obj.GetComponent<CarMovement>().setSpeed(this.GetComponent<WorldController>().getMovespeed() + extraSpeed);
            }
        }
    }

    bool ifTrue() {
        float rand = Random.Range(0, 3);
        if (rand <= 1){
            return true;
        }else {
            return false;
        }
    }

    // set spawn raid
    public void setSpawnraid(float spawnraid_) {
        spawnraid = spawnraid_;
    }
}
