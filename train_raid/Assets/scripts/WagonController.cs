using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonController : MonoBehaviour {

    [Header("Wagon Config")]
    public GameObject normal = null;
    public GameObject wall = null;

    private float wallAplha = 1.0f;

    private Vector3 normalPos;

    // Use this for initialization
    void Start() {
        normalPos = normal.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (wall != null) {
            Color col = wall.GetComponent<SpriteRenderer>().color;
            col.a = Mathf.Lerp(col.a, wallAplha, 10f * Time.deltaTime);
            wall.GetComponent<SpriteRenderer>().color = col;
        }

        // shake train
        this.shake(normal, normalPos);
    }

    private void OnTriggerEnter(Collider collider) {
        if (wall != null && collider.tag == "Player") {
            print("enter");
            wallAplha = 0.45f;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (wall != null && collider.tag == "Player") {
            print("exit");
            wallAplha = 1.0f;
        }
    }

    private void shake(GameObject _obj, Vector3 _pos) {
        Vector3 r = this.randomVec3(_pos, 0.01f);
        _obj.transform.position = r;
    }

    private Vector3 randomVec3(Vector3 _vec, float _off) {
        _vec.x = Random.Range(_vec.x - _off, _vec.x + _off);
        _vec.y = Random.Range(_vec.y - _off, _vec.y + _off);

        return _vec;
    }

}
