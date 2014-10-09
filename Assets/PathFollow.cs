using UnityEngine;
using System.Collections;

public class PathFollow : MonoBehaviour {

	public Transform pt1;
	public Transform pt2;
	public Transform pt3;
	public Transform pt4;
	public Transform pt5;
	public Transform pt6;

	int count;
	float step;

	// Use this for initialization
	void Start () {
		count = 0;
		step = .05f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (count) 
		{
			case 0:
				transform.position = Vector3.MoveTowards(transform.position, pt1.position, step);
				break;	
			case 1:
				transform.position = Vector3.MoveTowards(transform.position, pt2.position, step);
				break;	
			case 2:
				transform.position = Vector3.MoveTowards(transform.position, pt3.position, step);
				break;
			case 3:
				transform.position = Vector3.MoveTowards(transform.position, pt4.position, step);
				break;
			case 4:
				transform.position = Vector3.MoveTowards(transform.position, pt5.position, step);
				break;
			case 5:
				transform.position = Vector3.MoveTowards(transform.position, pt6.position, step);
				break;

		}
		if (transform.position == pt1.position
			|| transform.position == pt2.position
			|| transform.position == pt3.position
			|| transform.position == pt4.position
			|| transform.position == pt5.position)
						count++;
	}
}
