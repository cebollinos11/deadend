using UnityEngine;
using System.Collections;

public class BossHook : MonoBehaviour {

    [HideInInspector] public Player Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
