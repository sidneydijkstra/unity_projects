using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSlider : MonoBehaviour {

    public float slideSpeed;

    // start spawn points
    private Vector3 _leftPoint;
    private Vector3 _startPoint;
    private Vector3 _rightPoint;

    // background objects
    private GameObject _left;
    private GameObject _middel;
    private GameObject _right;

    void Start () {
        // get background size
        float sizeX = this.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;

        // set spawn points
        _startPoint = this.transform.position;
        _leftPoint = new Vector3(_startPoint.x - sizeX, _startPoint.y, _startPoint.z);
        _rightPoint = new Vector3(_startPoint.x + sizeX, _startPoint.y, _startPoint.z);

        // spawn backgrounds
        GameObject background = this.gameObject;

        _left = Instantiate(background, _leftPoint, Quaternion.identity) as GameObject;
        _right = Instantiate(background, _rightPoint, Quaternion.identity) as GameObject;
        _left.GetComponent<BackgroundSlider>().enabled = false;
        _right.GetComponent<BackgroundSlider>().enabled = false;

        _left.transform.SetParent(this.transform.parent);
        _right.transform.SetParent(this.transform.parent);

        _middel = this.gameObject;
    }
	
	void Update () {
        _left.transform.position = Vector3.Lerp(_left.transform.position, _left.transform.position + Vector3.left, slideSpeed * Time.deltaTime);
        _middel.transform.position = Vector3.Lerp(_middel.transform.position, _middel.transform.position + Vector3.left, slideSpeed * Time.deltaTime);
        _right.transform.position = Vector3.Lerp(_right.transform.position, _right.transform.position + Vector3.left, slideSpeed * Time.deltaTime);

        if (_left.transform.position.x < _leftPoint.x) {
            GameObject temp = _left;
            temp.transform.position = this.getRightPos(_right);
            _left = _middel;
            _middel = _right;
            _right = temp;
        }


        float sizeX = this.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        _startPoint = this.transform.parent.transform.position;
        _leftPoint = new Vector3(_startPoint.x - sizeX, _startPoint.y, _startPoint.z);
        _rightPoint = new Vector3(_startPoint.x + sizeX, _startPoint.y, _startPoint.z);
    }

    // get new spawn point to right
    private Vector3 getRightPos(GameObject _obj) {
        float sizeX = _obj.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        return new Vector3(_obj.transform.position.x + sizeX, _obj.transform.position.y, _obj.transform.position.z);
    }

    // get new spawn point to left
    private Vector3 getLeftPos(GameObject _obj) {
        float sizeX = _obj.GetComponent<SpriteRenderer>().bounds.size.x - 0.1f;
        return new Vector3(_obj.transform.position.x - sizeX, _obj.transform.position.y, _obj.transform.position.z);
    }
}
