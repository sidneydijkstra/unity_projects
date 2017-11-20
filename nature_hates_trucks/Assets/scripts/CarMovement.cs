using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public bool doesDamage = false;
    public GameObject exploadPartical;

    public float barAddPoints = 0;
    public int addPoints = 0;

    public GameObject[] exploadObject;

    private float moveSpeed = 15;

    private bool canExpload = false;
    private float exploadDelay;

    Rigidbody rig;

    void Start(){
        rig = GetComponent<Rigidbody>();
    }

    

    void Update(){
        transform.position += new Vector3(0, 0, -moveSpeed) * Time.deltaTime;

        // check if he can expload
        if (canExpload && Time.time >= exploadDelay) {
            // play sound
            Camera.main.GetComponent<AudioController>().audioExplosion();

            Instantiate(exploadPartical, this.transform.position, Quaternion.identity);
            exploadInObjects(exploadObject, 5);
            Destroy(this.gameObject);
        }
        
        // destroy car when out of bounds
        if (this.transform.position.z <= -50){
            Destroy(this.gameObject);
        }
    }

    public void setSpeed(float moveSpeed_){
        moveSpeed = moveSpeed_;
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            if (doesDamage) {
                // remove hearth
                col.GetComponent<CarController>().removeHeart();

                // play sound
                Camera.main.GetComponent<AudioController>().audioHit();
                Camera.main.GetComponent<AudioController>().audioExplosion();

                // expload
                Vector3 ObjectPos = this.transform.position;
                for (int i = 0; i < 4; i++){
                    // create object
                    Vector3 randomPos = new Vector3(Random.Range(0.5f, -0.5f), Random.Range(0.5f, -0.5f), Random.Range(0.5f, 1));
                    GameObject obj = Instantiate(exploadObject[Random.Range(0, exploadObject.Length)], ObjectPos + randomPos, Quaternion.Euler(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;

                    // add force
                    obj.GetComponent<Rigidbody>().AddExplosionForce(30, ObjectPos, 9999, 1, ForceMode.Impulse);
                }

                // expload partical
                Instantiate(exploadPartical, this.transform.position, Quaternion.identity);

                // destroy me
                Destroy(this.gameObject);
            }else{ 
                rig.AddExplosionForce(40, col.transform.position, 9999, 1, ForceMode.Impulse);
                col.GetComponent<CarController>().addBar(barAddPoints);
                moveSpeed = 0;

                // add points
                col.GetComponent<CarController>().addPoints(addPoints);

                // play sound
                Camera.main.GetComponent<AudioController>().audioSplat();

                canExpload = true;
                exploadDelay = Time.time + 0.6f;
            }
        }
    }

    void exploadInObjects(GameObject[] exploadObject, int exploadAmount) {
        // some vars
        Vector3 ObjectPos = this.transform.position;

        // loop to create object
        for (int i = 0; i < exploadAmount; i++){
            // create object
            Vector3 randomPos = new Vector3(Random.Range(-3, 3), Random.Range(-0.5f, -1), Random.Range(-3, 3));
            GameObject obj = Instantiate(exploadObject[Random.Range(0, exploadObject.Length)], ObjectPos + randomPos, Quaternion.identity);

            // add force
            obj.GetComponent<Rigidbody>().AddExplosionForce(50, ObjectPos, 9999, 1, ForceMode.Impulse);
        }
    }
}
