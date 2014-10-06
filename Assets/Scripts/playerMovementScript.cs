using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour {
	
	public float speed;
	

	// check if user has a gamepad
	public bool gamePadPresent;
	public Vector3 movedirection;

	// need to cast horPos, verPos
	public float horPos;
	public float verPos;  

	// Use this for initialization
	void Start () {
		if (Input.GetJoystickNames ().Length == 0)
			gamePadPresent = false;
		else
			gamePadPresent = true;
		print (gamePadPresent);
	}
	
	// Update is called once per frame
	void Update () {
		if (gamePadPresent) {
			
			horPos = Input.GetAxis("Horizontal");
			verPos = Input.GetAxis("Vertical");

		}
		else {
			horPos = 0; //Input.GetAxis("Horizontal");
			verPos = 0; //Input.GetAxis("Vertical");
			if( Input.GetKey(KeyCode.A))
			{
				horPos = -1;
			}
			if ( Input.GetKey(KeyCode.W))
			{
				verPos = 1;
			}
			if( Input.GetKey(KeyCode.S))
			{
				verPos = -1;
			}
			if ( Input.GetKey(KeyCode.D))
			{
				horPos = 1;
			}
			Vector2 mov = new Vector2(horPos,verPos);
			mov = mov.normalized;
			movedirection = new Vector3(mov.x, mov.y,0);

		}
	}
	
	void FixedUpdate()
	{
		//rigidbody2D.AddForce(movedirection*speed*Time.deltaTime);
		if(rigidbody2D.velocity!=Vector2.zero){
			float angle = Vector2.Angle (Vector2.right, rigidbody2D.velocity);
			if(Vector2.Angle (Vector2.up, rigidbody2D.velocity) > 90){
				angle *= -1;
			}
			transform.rotation = Quaternion.Euler (new Vector3(0,0,angle));
		}
		rigidbody2D.velocity = movedirection*speed*Time.deltaTime;
	
	}
}
