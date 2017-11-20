using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public GameObject playerhpbar;
	public int player;
	public float maxHealth;
	float curentHealth;
	public float Bullet = 10;

	public bool hit;

	// Use this for initialization
	void Start () {
		curentHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		if (curentHealth <= 0){
			Destroy (gameObject);
			if (player == 0) {
				PlayerPrefs.SetInt ("WINNER", 1);
			} else {
				PlayerPrefs.SetInt ("WINNER", 0);
			}
			Application.LoadLevel (2);
		}
		if (hit) {
			hit = false;
		}
	}

	public void PlayerDamage(float dmg) {
		curentHealth -= dmg;
		if (curentHealth <= 0) {
			curentHealth = 0;
		}
		playerhpbar.GetComponent<GuiPlayerHealth> ().Dmg ();
	}

	public void HPRegen(float Regen){
		curentHealth += Regen;
		if (curentHealth >= maxHealth) {
			curentHealth = maxHealth;
		}
	}

	public float GetHealth(){
		return curentHealth;
	}

	void OnCollisionEnter2D(Collision2D col){
		// Do somethingprint ("Enter");
		print("enter");
		if (col.gameObject.tag == "BULLET") {
			print ("hit");
		}

		//HP Pack

	}
}
