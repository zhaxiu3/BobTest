using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    float _timer = 0;
    public float Speed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(_timer * Speed, Vector3.up);
        transform.position = Vector3.zero + transform.rotation * new Vector3(0, 0, -100);
	}
}
