using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement Config")]
    public float speed = 7;

    private Rigidbody rig;
    private Camera cam;

    void Start() {
        // get rigidbody
        rig = this.GetComponent<Rigidbody>();

        // get cam
        cam = Camera.main;
    }

    void Update() {
        playerMovement();
        cameraMovement();
    }

    // player movement
    Vector3 dir;
    private void playerMovement() {

        float x = Input.GetAxis("Horizontal");
        
        // movement left and right
        dir.x = x * speed;

        if (this.isGrounded()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                print("jump");
                rig.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }
        }

        rig.MovePosition(rig.position + dir * Time.deltaTime);

    }

    // camera movement
    private void cameraMovement() {
        cam.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y, -10); ;
    }

    // get target is ground
    private bool isGrounded() {
        // cast ray to ground
        RaycastHit _hit;
        Vector3 pos = this.transform.position;
        pos.y -= this.GetComponent<BoxCollider>().size.y;
        Physics.Raycast(pos, Vector3.down, out _hit);

        Debug.DrawRay(pos, Vector3.down * 0.5f, Color.red);

        // check if hit is null and then the distance
        if (_hit.collider != null && _hit.distance <= 0.5) {
            //print("grounded");
            return true;
        }
        return false;
    }
}
