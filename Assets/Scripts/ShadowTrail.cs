using UnityEngine;
using System.Collections;

public class ShadowTrail : MonoBehaviour {

    SpriteRenderer curSpriteR;
    float spriteFreq = 0.05f;
    float t = 0f;
    bool paused;

    Material shadowMaterial;

	// Use this for initialization
	void Awake () {
        shadowMaterial = Resources.Load("Materials/FlashBlack") as Material;
        curSpriteR = GetComponent<SpriteRenderer>();
        paused = true;
        
	}

    public void Play()
    {

        
        paused = false;
        t = 0f;

    }

    public void Stop()
    {
       
        paused = true;
    }

    void PlaceShadow() {
        SpriteRenderer sR;
        GameObject shadow = new GameObject("shadow");
        sR = shadow.AddComponent<SpriteRenderer>();
        sR.sprite = curSpriteR.sprite;
        sR.color = curSpriteR.color; //( curSpriteR.color) ;
        sR.material = shadowMaterial; // curSpriteR.material;
        sR.transform.position = curSpriteR.transform.position;
        sR.transform.rotation = curSpriteR.transform.rotation;
        sR.transform.localScale = curSpriteR.transform.localScale * 0.9f;
        Destroy(shadow, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {

        

        if (paused)
            return;
        t-=Time.deltaTime;

        if (t < 0)
        {
            t = spriteFreq;
            PlaceShadow();
        }
	
	}
}
