using UnityEngine;
using System.Collections;

public class eyeLaser : MonoBehaviour {

    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float angle = transform.rotation.eulerAngles.z *  Mathf.PI / 180;
        
        Vector3 v = new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0f);
        transform.position += v * Time.timeScale * speed * 0.001f;
	
	}

    
}
