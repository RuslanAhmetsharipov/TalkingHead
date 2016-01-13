using UnityEngine;
using System.Collections;

public class CameraMotoring : MonoBehaviour {
    public float speed=10f;
    private float d;
    public Transform object1;
    private float maxdistance;
	// Use this for initialization
	void Start () {
        maxdistance = Vector3.Distance(transform.position, object1.position);
	}
	
	// Update is called once per frame
	void Update () {
        d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            if (Vector3.Distance(transform.position, object1.position) > 0.5f)
                transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            if(d<0f)
            if (Vector3.Distance(transform.position, object1.position) < maxdistance)
                transform.position -= transform.forward * speed * Time.deltaTime;
        }

	}
}
