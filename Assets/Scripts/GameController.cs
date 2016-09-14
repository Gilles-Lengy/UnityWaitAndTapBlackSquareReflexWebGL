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
    /* private float minutes;
     private float seconds;
     private float centiems;
     */

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
            centiems = (elapsedTime * 100) % 1000;
            */


            textTimer.text = elapsedTime.ToString();
        }
        else {
            textTimer.text = "Wait...";
        }

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (sprite2Hit.GetComponent<Collider2D>().OverlapPoint(wp))
                {
                    hitCount++;
                    setHitText();
                    Destroy(sprite2Hit);
                    elapsedTimeTotal += elapsedTime;
                    timerOn = false;
                    textElapsedTimeTotal.text = elapsedTimeTotal.ToString();
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
            switch (sprite2HitCount) {
                case 1:
                    destroyWait = 1F;
                    break;
                case 3:
                    destroyWait = 0.9F;
                    break;
                case 6:
                    destroyWait = 0.8F;
                    break;
                case 9:
                    destroyWait = 0.7F;
                    break;
                case 12:
                    destroyWait = 0.6F;
                    break;
                case 15:
                    destroyWait = 0.5F;
                    break;
                case 24:
                    destroyWait = 0.4F;
                    break;
                case 27:
                    destroyWait = 0.3F;
                    break;

            }
            yield return new WaitForSeconds(destroyWait);
            Destroy(sprite2Hit);
            timerOn = false;
            yield return new WaitForSeconds(spawnWait);
        }
    }


    void setHitText()
    {
        textHitCount.text = strScore + hitCount.ToString();
    }
}
