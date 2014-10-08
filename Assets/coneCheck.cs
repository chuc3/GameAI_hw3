using UnityEngine;
using System.Collections;

public class coneCheck : MonoBehaviour {

	RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Ray ray1 = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (ray1,out hit, 10f) && hit.transform.tag =="flocker") {
			evade ();
				}
	}
	void evade(){
		print (hit.transform);
		transform.position = Vector3.MoveTowards (transform.position, -hit.transform.position, 10f);
	}
}
