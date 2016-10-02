using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class SendHSButton : MonoBehaviour {

    public
    Text EnterName, EnterValue;

    HSController HS;

    public void SendHS()
    {
        
        HS.StartCoroutine(HS.PostScores(EnterName.text,Int32.Parse(EnterValue.text)));
        transform.parent.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        HS = GameObject.FindObjectOfType<HSController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
