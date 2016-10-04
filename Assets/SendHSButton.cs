using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class SendHSButton : MonoBehaviour {

    public
    Text EnterName, EnterValue;

    HSController HS;
    Button button;
    bool enabled;
    public void SendHS()
    {
        
        HS.StartCoroutine(HS.PostScores(EnterName.text,Int32.Parse(EnterValue.text)));
        transform.parent.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        HS = GameObject.FindObjectOfType<HSController>();
        button = GetComponent<Button>();
        button.interactable = false;

	}
	
	// Update is called once per frame
	void Update () {
	    if(!enabled)
        {
            if(EnterName.text!="")
            {
                button.interactable = true;
                enabled = true;
            }
        }
	}
}
