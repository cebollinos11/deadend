using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HSController : MonoBehaviour
{
    private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    string addScoreURL = "http://pablosan.com/deadend/addhighscore.php?"; //be sure to add a ? to your url
    string highscoreURL = "http://pablosan.com/deadend/display.php";

    public Text ScoresName;
    public Text ScoresPoints;
    public Text ScoresOrder;
    public Text PositionMade;

    public GameObject NewHSPanel;

    public Text YourScore;

    int lowerHS;

    bool AlreadyDeliveredHS;

    void Start()
    {
        
        StartCoroutine(GetScores());
    }

    string Md5Sum( string strToEncrypt)
    {
       System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
	    byte[] bytes = ue.GetBytes(strToEncrypt);
 
	    // encrypt bytes
	    System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
	    byte[] hashBytes = md5.ComputeHash(bytes);
 
	    // Convert the encrypted bytes back to a string (base 16)
	    string hashString = "";
 
	    for (int i = 0; i < hashBytes.Length; i++)
	    {
		    hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
	    }
 
	    return hashString.PadLeft(32, '0');
    }

    // remember to use StartCoroutine when calling this function!
    public IEnumerator PostScores(string name, int score)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }

        StartCoroutine(GetScores());

        
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        ScoresName.text = "Loading Scores";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            //ScoresName.text = hs_get.text; // this is a GUIText that will display the scores in game.
            SortResults(hs_get);
        }
    }

    void SortResults(WWW hsget )
    {

        ScoresName.text = "";
        ScoresPoints.text = "";
        ScoresOrder.text = "";
        Debug.Log(hsget.text);

        int yourScore = Int32.Parse(YourScore.text);
        int positionPlayerMade = -1;
        int positionMadeIndex = -1;
        bool positionSet = false;

        char delimiter = '@';
        string[] lines = hsget.text.Split(delimiter);
        for (int i = 0; i < lines.Length-1; i++)
        {
            //ScoresOrder.text += (i + 1).ToString() + ".\n";

            string[] entry = lines[i].Split('%');

            //name
            //ScoresName.text += entry[0] + '\n';

            //points
            //ScoresPoints.text += entry[1] + '\n';
            lowerHS = Int32.Parse(entry[1]);

            if(positionSet==false && yourScore>lowerHS)
            {
                positionSet = true;
                positionPlayerMade = i + 1;
                switch (positionPlayerMade)
                {
                    case 1:
                        PositionMade.text = positionPlayerMade.ToString() + "st";
                        break;
                    case 2:
                        PositionMade.text = positionPlayerMade.ToString() + "nd";
                        break;
                    case 3:
                        PositionMade.text = positionPlayerMade.ToString() + "rd";
                        break;
                    default:
                        PositionMade.text = positionPlayerMade.ToString() + "th";
                        break;
                }
                //PositionMade.text = positionPlayerMade.ToString()+"st";
                positionMadeIndex = i+1;

                for (int j = 5; j > -10; j--)
                {
                    if ((i - j) > -1 && (i - j) < lines.Length - 1)
                    {
                        Debug.Log("ha petado en j =" + j.ToString() + "where i-j = " + (i - j).ToString());
                        ScoresName.text += lines[i-j].Split('%')[0] + '\n';
                        ScoresPoints.text += lines[i-j].Split('%')[1] + '\n';
                        ScoresOrder.text += (i-j + 1).ToString() + ".\n";
                    }
                }
            }
            //int i = 0; i < lines.Length-1

            /*
            if(positionSet==false && i+15>lines.Length)
            {
                Debug.Log("relleno");
                ScoresName.text += entry[0] + '\n';

                ScoresPoints.text += entry[1] + '\n';

                ScoresOrder.text += (i + 1).ToString() + ".\n";
            }
             * */
            

        }
        if (positionSet == false)
        {
            int i = lines.Length - 1;
            for (int j = 15; j > 0; j--)
            {
                if ((i - j) > -1 )
                {
                    Debug.Log("Relleno");
                    ScoresName.text += lines[i - j].Split('%')[0] + '\n';
                    ScoresPoints.text += lines[i - j].Split('%')[1] + '\n';
                    ScoresOrder.text += (i - j + 1).ToString() + ".\n";
                }
            }
        }
        


        //filter display scores


        

        Debug.Log("Comparing " + yourScore.ToString() + " with " + lowerHS);
        if (AlreadyDeliveredHS == false && yourScore > lowerHS)
        {
            AlreadyDeliveredHS = true;

            StartCoroutine(ShowNH());
           
        }
            

        

    }

    IEnumerator ShowNH()
    {
        yield return new WaitForSeconds(1f);
        NewHSPanel.SetActive(true);
        //iTween.ShakePosition(NewHSPanel, iTween.Hash("x", 10, "y", 10, "Time", 0.5f));
        iTween.ShakeScale(NewHSPanel, iTween.Hash("x", 2, "y", 2, "time", 0.5));


        AudioManager.PlayClip(AudioManager.Instance.coin);
    }


    

}