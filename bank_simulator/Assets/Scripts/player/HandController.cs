using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {
	public Camera player_camera;
	public Transform phys_dragger; //empty parented to camera
	public float grab_distance = 5.0f;
	private float temp_drag = 10.0f;

	void Start(){
		player_camera = Camera.main;
	}

	void Update () {
		if(Input.GetButtonDown("Fire1")){
			RaycastHit hit;
			if(Physics.Raycast(player_camera.transform.position, player_camera.transform.forward, out hit, grab_distance)){
				if(hit.transform.GetComponent<Rigidbody>() && hit.transform.tag == "CANPICKUP"){
					phys_dragger.position = hit.transform.GetComponent<Rigidbody>().position;
					phys_dragger.rotation = hit.transform.GetComponent<Rigidbody>().rotation;
					StartCoroutine(DragObject(hit.transform.GetComponent<Rigidbody>()));
				}
			}
		}
	}

	IEnumerator DragObject(Rigidbody r){
		float oldDrag = r.drag;
		r.drag = temp_drag;
		r.useGravity = false;
		while(Input.GetButton("Fire1")){
			r.transform.rotation = Quaternion.Slerp(r.transform.rotation, phys_dragger.rotation, Time.deltaTime * 15.0f);
			Vector3 vel = (phys_dragger.position - r.position).normalized;
			float d = Mathf.Min(Vector3.Distance(r.transform.position, phys_dragger.position), 1.0f);
			r.AddForce(vel * d *3, ForceMode.VelocityChange);

			if(Input.GetButton("Fire2")){
				r.AddForce(player_camera.transform.forward * 7, ForceMode.Impulse);
				r.drag = oldDrag;
				r.useGravity = true;
				yield break;
			}
			yield return new WaitForFixedUpdate();
		}
		r.drag = oldDrag;
		r.useGravity = true;
	}
}