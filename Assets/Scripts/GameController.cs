using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject srite2Duplicate;
    public Vector2 spawnValues;
    public int hitCount;

    public float startWait;
    public float spawnWait;
    public float destroyWait;
    public Text textHitCount;

    private string strScore;
    private int sprite2HitCount;


    /* Timer */ // http://mafabrique2jeux.fr/blog-fabriquer-jeu-video/20-tutoriels-fr-unity3d/55-timer-unity3d
    public Text textTimer;
    public Text textElapsedTimeTotal;
    public float startTime;
    public float elapsedTime;
    public float elapsedTimeTotal;

    /*** pour le formatage ****/
     private string minutes;
     private string seconds;
     private string centiems;
    private string milliems;
    private string dixmilliems;
    private int milliemsElapsedTimeLimit;
    private string elapsedTimeTotalFormated;


    private bool timerOn;





    private GameObject sprite2Hit;

    void Start()
    {
        timerOn = false;
        elapsedTimeTotal= 0F;
        strScore = "Score : ";
        hitCount = 0;
        sprite2HitCount = 0;
        setHitText();
        textElapsedTimeTotal.text = elapsedTimeTotal.ToString();

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {

        if (timerOn)
        {
            //le temps écoulé = temps actuel - start time
            elapsedTime = Time.time - startTime;

            //formatage
            /*
            minutes = elapsedTime / 60;
            seconds = elapsedTime % 60;
            centiems = (elapsedTime * 100) % 100;



            textTimer.text = elapsedTime.ToString()+ string.Format ("{ 0:00}:{ 1:00}:{ 2:00}",minutes,seconds,centiems);
            */
            //formatage
            /*
            minutes = elapsedTime / 60;
            seconds = elapsedTime % 60;
            centiems = (elapsedTime * 100 )%100;
            milliems = (elapsedTime * 1000) % 1000;


            textTimer.text = "Temps écoulé : "+string.Format ("{0:00}:{1:00}:{2:00}:{3:00}", minutes,seconds,centiems,milliems);

            */
            
            textTimer.text = formatTime(elapsedTime);


        }
        else {
            textTimer.text = "Wait...";
        }

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (sprite2Hit != null && sprite2Hit.GetComponent<Collider2D>().OverlapPoint(wp))
                {
                    hitCount++;
                    setHitText();
                    Destroy(sprite2Hit);
                    elapsedTimeTotal += elapsedTime;
                    timerOn = false;
                elapsedTimeTotalFormated =  formatTime(elapsedTimeTotal);
                textElapsedTimeTotal.text = elapsedTimeTotalFormated;

            }

        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        { 
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y));
                Quaternion spawnRotation = Quaternion.identity;
            sprite2Hit = Instantiate(srite2Duplicate, spawnPosition, spawnRotation) as GameObject;
            timerOn = true;
            startTime = Time.time; // on note le startTime
            sprite2HitCount++;  
            destroyWait = 0.777F;
            yield return new WaitForSeconds(destroyWait);
            if (sprite2Hit != null) { 
            Destroy(sprite2Hit);
            }
            timerOn = false;
            yield return new WaitForSeconds(spawnWait);
        }
    }


    void setHitText()
    {
        textHitCount.text = strScore + hitCount.ToString();
    }

    private string formatTime(float timeToFormat)
    {
        string value = timeToFormat.ToString();
        char delimiter = '.';
        string[] substrings = value.Split(delimiter);
        int lenghtAfterDelimiter = substrings[1].Length;

        seconds = substrings[0];
        centiems = substrings[1].Substring(0, 2);
        milliems = substrings[1].Substring(2, 2);

        switch (lenghtAfterDelimiter)
        {
            case 7:
                milliemsElapsedTimeLimit = 3;
                break;
            case 6:
                milliemsElapsedTimeLimit = 2;
                break;
            case 5:
                milliemsElapsedTimeLimit = 1;
                break;

        }

        if (lenghtAfterDelimiter == 4)
        {
            dixmilliems = "00";
        }
        else
        {
            dixmilliems = substrings[1].Substring(4, milliemsElapsedTimeLimit);// Lenght-4 because the lenght of the substring depends on the dixmilliems, when a 0 should be present at the end, tere is nothing... So, the lenght is variable...
        }


        if (seconds.Length < 2)
        {
            seconds = "0" + seconds;
        }
        if (dixmilliems.Length < 2)
        {
            dixmilliems = dixmilliems + "0";
        }

        /*
        string zero = "00";
        int lengthZero = zero.Length;
        */
        // textElapsedTimeTotal.text = lengthZero.ToString() + " / " +  elapsedTimeTotal.ToString() + " / " + substrings[0] + " / " + substrings[1] + " / " + lenghtAfterDelimiter.ToString() + " / " + seconds + " : " + centiems + " : " + milliems + " : " + dixmilliems;

        string text = seconds + " : " + centiems + " : " + milliems + " : " + dixmilliems;

        return text;
    }
}
