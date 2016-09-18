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
            textTimer.text = elapsedTime.ToString();


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


                //formatage
                /*
                                minutes = elapsedTimeTotal / 60;
                                seconds = elapsedTimeTotal % 60;
                                centiems = (elapsedTimeTotal * 100) % 100;




                                textElapsedTimeTotal.text = string.Format("{ 0:00}:{ 1:00}:{ 2:00}", minutes, seconds, centiems);

                */
/*
                minutes = elapsedTimeTotal / 60;
                seconds = elapsedTimeTotal % 60;
                centiems = (elapsedTimeTotal * 100) % 100;
                milliems = (elapsedTimeTotal * 1000) % 1000;
                */

                string value = elapsedTimeTotal.ToString();
                char delimiter = '.';
                string[] substrings = value.Split(delimiter);
                int lenght = substrings[1].Length;
                seconds = substrings[0];
                centiems = substrings[1].Substring(0,2);
                milliems = substrings[1].Substring(2,2);
               dixmilliems = substrings[1].Substring(4, lenght-4);// Lenght-4 because the lenght of the substring depends on the dixmilliems, when a 0 should be present at the end, tere is nothing... So, the lenght is variable...

                if (seconds.Length < 2) {
                    seconds = "0" + seconds;
                }
                if (dixmilliems.Length < 2)
                {
                    dixmilliems = dixmilliems + "0";
                }

                textElapsedTimeTotal.text = elapsedTimeTotal.ToString() + " / " + substrings[0] + " / " + substrings[1] + " / " + seconds + " : " + centiems + " : " + milliems + " : " + dixmilliems;


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
            /*
            switch (sprite2HitCount) {
                case 7:
                    destroyWait = 0.9F;
                    break;
                case 14:
                    destroyWait = 0.88F;
                    break;
                case 21:
                    destroyWait = 0.777F;
                    break;

            }*/
            destroyWait = 0.9F;
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
}
