using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour {

    private float moveSpeed = 12;

	void Start () {
		
	}
	
	public void move () {
        transform.position += new Vector3(0, 0, -moveSpeed) * Time.deltaTime;
	}

    public void setSpeed(float moveSpeed_) {
        moveSpeed = moveSpeed_;
    }
}
