using UnityEngine;
using System.Collections;

public class coneCheck : MonoBehaviour {

	RaycastHit hit;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray (transform.position, transform.forward);
		LineRenderer line = new LineRenderer();
		line.SetPosition(0, ray.origin);
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast (ray,out hit, 10f) && hit.transform.tag =="flocker") {
			evade ();
				}
		if (Physics.Raycast (ray, out hit, 100))
						line.SetPosition (1, hit.point);
		else line.SetPosition(1,ray.GetPoint(100));
	}
	void evade(){
		print (hit.transform.name);
		Debug.DrawLine (transform.position, hit.point, Color.cyan);
		Vector3 dir = (hit.transform.position - transform.position).normalized;
		Quaternion rot = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime);
		transform.position += transform.forward * 10f * Time.deltaTime;
	}
}
