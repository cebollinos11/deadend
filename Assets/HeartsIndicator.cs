using UnityEngine;
using System.Collections;

public class HeartsIndicator : MonoBehaviour {


    public GameObject FullHeart,EmptyHeart;
    public Transform StartPoint;

    public float distance;

    void PrintHearts(int currentlives,int maxlives)
    {

        GameObject foo;
        GameObject heart;
        for (int i = 0; i < maxlives; i++)
        {
            Vector3 pos = StartPoint.position;
            pos += Vector3.right*distance*i;
            if (i < currentlives)
                heart = FullHeart;
            else
            {
                heart = EmptyHeart;
            }
            foo = GameObject.Instantiate(heart, pos, Quaternion.identity) as GameObject;
        }
    }
	// Use this for initialization
	void Start () {

        GameManager gm = GetComponent<GameManager>();
        PrintHearts(gm.GS.lives,5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
