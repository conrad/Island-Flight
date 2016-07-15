using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour {
	
	public GameObject leftEye;    
	Rigidbody rr;
	public float forwardSpeed = 100f;    
	private float rotationSpeed = 300f;        
	private Vector3 axis;    
	private float rotationY;    
	private float rotationX;    
	private float rotationZ;        
	private bool start = true;

//	private float boundaryMaxY = 700;
//	private DictionaryBase boundaries
//	private Dictionary<string,bool> loaded;   

	// Use this for initialization    
	void Start () {        
		rr = GetComponent<Rigidbody> (); 
	}        

	// Update is called once per frame    
	void Update () {   
        if (start) {                        
            FlightMode();        
        } else {
            rr.velocity = Vector3.zero;
        }
	}                

    void FixedUpdate() {
        if (GvrViewer.Instance.Triggered) {
            Debug.Log("start/stop triggered");
            start = !start;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("start/stop called by space");
            start = !start;        
        } 
    }

	void FlightMode () {
		//get rotation values for the leftEye        
		rotationX = leftEye.transform.localRotation.x / 2;        
		rotationY = leftEye.transform.localRotation.y / 2;        
		rotationZ = leftEye.transform.localRotation.z;                

		//put them into a vector        
		axis = new Vector3 (rotationX, rotationY, rotationZ);                

		// Rotate - Use this methodology in order to turn at the same rate even if frames dropped
		transform.Rotate (axis * Time.deltaTime * rotationSpeed);
		// Move forward
		rr.velocity = leftEye.transform.forward * forwardSpeed;
//        GetComponent ().AddForce (transform.forward * BSpeed);
	}
}
