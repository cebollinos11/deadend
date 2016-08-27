using UnityEngine;
using System.Collections;

public class HookChainHandler : MonoBehaviour {

    public GameObject Boss,Hook;
    LineRenderer lr;
    HookTiler hookTiler;


	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        hookTiler = GetComponent<HookTiler>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Boss.transform.position);
        //lr.SetPositions(new Vector3[2] { Boss.transform.position,Hook.transform.position});
        lr.SetPosition(0, Boss.transform.position);
        lr.SetPosition(1, Hook.transform.position);

        hookTiler.hookLenght = Boss.transform.position.x - Hook.transform.position.x;
	
	}
}
