using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FaderManager : MonoBehaviour {

    Image Panel;

	// Use this for initialization
	void Start () {
        Panel = GetComponent<Image>();
        StartCoroutine(FadeIn());
	}


    IEnumerator FadeIn()
    {
        float maxtime = 0.8f;
        float t = 0f;

        while(t<maxtime)
        {
            t += Time.deltaTime;
            Debug.Log("colouring");
            Panel.color = Color.Lerp(Color.black, new Color(0f,0f,0f, 0f),t/maxtime);
            yield return new WaitForEndOfFrame();

        }


    }

}
