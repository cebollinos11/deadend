using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {

    Transform playerpos;

    public bool followPlayer;
    public AnimationCurve bump;
    public float bumpTime;
    public float bumpAmplitude;
    Vector3 origPos;

	// Use this for initialization
	void Start () {
        origPos = transform.position;
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
	}


    IEnumerator CoBump() {

        float c = 0.0f;
        
        do
        {
            
            c += Time.deltaTime*1/bumpTime;
            transform.position = origPos + new Vector3(0f,1f,0f)*bump.Evaluate(c)*bumpAmplitude;
            yield return new WaitForEndOfFrame();

        } while (c < 1.0f);

        transform.position = origPos;
       
    }

    public void PlayBump()
    {
        StartCoroutine(CoBump());
    }

    IEnumerator ZoomToPosition(Vector2 targetPos) {

        float dist = 100f;
        do
        {
            yield return new WaitForEndOfFrame();

            transform.position = Vector2.Lerp(transform.position,targetPos,0.1f);
            dist = Vector2.Distance(transform.position, targetPos);

            transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        } while (dist > 0.01);


        transform.position = new Vector3(targetPos.x, targetPos.y, -1);


    
    }
    public void zoomTo(Vector2 targetPos) {

        followPlayer = false;
        StartCoroutine(ZoomToPosition(targetPos));
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.K)){
            zoomTo(new Vector2(1, 1));
        }


        if (followPlayer)
        {

            if (playerpos.position.y > 0)
            {
                transform.position = new Vector3(playerpos.position.x, transform.position.y, transform.position.z);
            }

            else
                transform.position = new Vector3(playerpos.position.x, playerpos.position.y, transform.position.z);
        
        }
            
	
	}
}
