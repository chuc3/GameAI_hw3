using UnityEngine;
using System.Collections;

public class coneCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, 10f)) {
			float angle = transform.rotation.w+10f;
			//transform.rotation = Quaternion.Euler (new Vector3(0,0,angle));
				}
	}
}
