using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FlockingAgent : MonoBehaviour {

	// vars
	public float speed;
	public float visionAngle;
	public float alignmentDistance;
	public float cohesionDistance;
	public float separationDistance;
	
	public float separationDistanceLarge;
	public float separationWeightLarge;
	
	public float alignmentWeight;
	public float cohesionWeight;
	public float separationWeight;
	
	public float visionAngleLead;
	public float alignmentDistanceLead;
	public float cohesionDistanceLead;
	public float separationDistanceLead;
	
	public float alignmentWeightLead;
	public float cohesionWeightLead;
	public float separationWeightLead;
	
	public float scatterDistance;
	public float scatterSeparationWeight;


	public List<GameObject> birds = new List<GameObject>();
	public List<GameObject> leaders = new List<GameObject>();

	Vector2 velocity;

	// Use this for initialization
	void Awake () {

		rigidbody2D.velocity = transform.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Vector2.Angle (Vector2.right, rigidbody2D.velocity);
		if(Vector2.Angle (Vector2.up, rigidbody2D.velocity) > 90){
			angle *= -1;
		}
		transform.rotation = Quaternion.Euler (new Vector3(0,0,angle));
	}

	void FixedUpdate(){
		rigidbody2D.velocity = velocity;
	}

	public void Flock(){
		Vector2 align = Align(sbirds, alignmentDistance, visionAngle);
		Vector2 cohere = Cohere (birds, cohesionDistance);
		Vector2 separate = Separate (birds, separationDistance);
		
		Vector2 sep2 = Separate(birds, separationDistanceLarge);
		
		Vector2 alignLead = Align(leaders, alignmentDistanceLead, visionAngleLead);
		Vector2 cohereLead = Cohere (leaders, cohesionDistanceLead);
		Vector2 separateLead = Separate (leaders, separationDistanceLead);
		
		Vector2 result = rigidbody2D.velocity +
			(align * alignmentWeight) + (cohere * cohesionWeight) + (separate * separationWeight) + (sep2 * separationWeightLarge) +
			(alignLead * alignmentWeightLead) + (cohereLead * cohesionWeightLead) + (separateLead * separationWeightLead);

		velocity = result.normalized * speed;
	}

	Vector2 Align(List<GameObject> birds, float distance, float angle){
		int count = 0;
		return Align (birds, distance, angle, out count);
	}

	Vector2 Align(List<GameObject> birds, float distance, float angle, out int count){
		Vector2 sum = Vector2.zero;
		count = 0;
		for(int i = 0; i < birds.Count; i++){
			if(birds[i]!=null && birds[i] != gameObject && Vector2.Distance (birds[i].transform.position, transform.position) < distance){
				if(Vector2.Angle (birds[i].transform.position - transform.position, transform.right) < angle){
					sum += birds[i].rigidbody2D.velocity;
					count++;
				}
			}
		}
		if(count == 0){
			return sum;
		}
		sum.x = sum.x / count;
		sum.y = sum.y / count;
		return sum.normalized;
	}

	Vector2 Cohere(List<GameObject> birds, float distance){
		int count = 0;
		return Cohere (birds, distance, out count);
	}
	
	Vector2 Cohere(List<GameObject> birds, float distance, out int count){
		Vector2 sum = Vector2.zero;
		count = 0;
		for(int i = 0; i < birds.Count; i++){
			if(birds[i]!=null && birds[i] != gameObject && Vector2.Distance (birds[i].transform.position, transform.position) < distance){
				sum += (Vector2)birds[i].transform.position;
				count++;
			}
		}
		if(count == 0){
			return sum;
		}
		sum.x = sum.x / count;
		sum.y = sum.y / count;
		sum = sum - (Vector2)transform.position;
		return sum.normalized;
	}

	Vector2 Separate(List<GameObject> birds, float distance){
		int count = 0;
		return Separate (birds, distance, out count);
	}
	
	Vector2 Separate(List<GameObject> birds, float distance, out int count){
		Vector2 sum = Vector2.zero;
		count = 0;
		for(int i = 0; i < birds.Count; i++){
			if(birds[i]!=null && birds[i] != gameObject && Vector2.Distance (birds[i].transform.position, transform.position) < distance){
				sum += (Vector2)(birds[i].transform.position - transform.position);
				count++;
			}
		}
		if(count == 0){
			return sum;
		}
		sum.x = sum.x / count;
		sum.y = sum.y / count;
		return -sum.normalized;
	}
}
