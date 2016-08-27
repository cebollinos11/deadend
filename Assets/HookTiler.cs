using UnityEngine;
using System.Collections;

public class HookTiler : MonoBehaviour {

    LineRenderer LineRen;
    public float tileRatio,offsetMultiplier;

    [HideInInspector]public float hookLenght;

    [SerializeField]
    Material chain0, chain1;

	// Use this for initialization
	void Start () {
        LineRen = GetComponent<LineRenderer>();
        LineRen.material = chain0;
	}
    public void RetractChain()
    {
        LineRen.material = chain0;
    }
    public void ExtendChain()
    {
        LineRen.material = chain1;

        Debug.Log("material changed to " + LineRen.materials[0].ToString());
    }
	
	// Update is called once per frame
	void Update () {

        LineRen.material.mainTextureScale = new Vector2(tileRatio*hookLenght, 1f);
        LineRen.material.mainTextureOffset = new Vector2( -hookLenght*offsetMultiplier, 1f);
	
	}
}
