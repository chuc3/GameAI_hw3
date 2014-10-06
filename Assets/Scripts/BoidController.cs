using UnityEngine;
using System.Collections;

public class BoidController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public float randomness = 1;
	public int flockSize = 20;
	public GameObject prefab;
	public GameObject chasee;
	
	public Vector3 flockCenter;
	public Vector3 flockVelocity;
	
	private GameObject[] boids;
	
	void Start()
	{
		boids = new GameObject[flockSize];
		for (var i=0; i<flockSize; i++)
		{
			Vector3 position = new Vector3 (
				Random.value * collider.bounds.size.x,
				Random.value * collider.bounds.size.y,
				0f//Random.value * collider.bounds.size.z
				) - collider.bounds.extents;
			Quaternion rot = new Quaternion(0f,0f,0f,0f);
			GameObject boid = Instantiate(prefab, transform.position, rot) as GameObject;
			boid.transform.parent = transform;
			boid.transform.localPosition = position;
			boid.GetComponent<BoidFlocking>().SetController (gameObject);
			boids[i] = boid;
			print (boid);
		}
	}
	
	void Update ()
	{
		Vector3 theCenter = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;
		
		foreach (GameObject boid in boids)
		{
			theCenter = theCenter + boid.transform.localPosition;
			theVelocity = theVelocity + boid.rigidbody.velocity;
			//keep z 0
			Vector3 temp = boid.transform.position; 
			temp.z = 2.0f; 
			boid.transform.position = temp; 

			// rotations stuff
			Quaternion rotation = Quaternion.LookRotation
				(boid.rigidbody.velocity, boid.transform.TransformDirection(Vector3.up));
			boid.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			/*
			Quaternion tempQ = boid.transform.rotation; 
			tempQ = Quaternion.identity;
			boid.transform.rotation = tempQ;
			if (boid.rigidbody.velocity != Vector3.zero) {
				boid.transform.rotation = Quaternion.LookRotation((boid.rigidbody.velocity), Vector3.forward);
				Quaternion tempqq = new Quaternion(0f,0f,boid.transform.rotation.z,1f);
				boid.transform.rotation = tempqq;
			}
			*/
			//transform.rotation=Quaternion.SetLookRotation(velocity);
		}
		
		flockCenter = theCenter/(flockSize);
		flockVelocity = theVelocity/(flockSize);
	}
}