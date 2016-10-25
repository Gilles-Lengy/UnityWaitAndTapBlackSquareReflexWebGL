using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


// this toggles a component (usually an Image or Renderer) on and off for an interval to simulate a blinking effect
public class BlinkImage : MonoBehaviour
{

    // this is the UI.Text or other UI element you want to toggle
    public MaskableGraphic imageToToggle;

    public float interval = 1f;
    public float startDelay = 0.5f;
    public bool currentState = true;
    public bool defaultState = true;
    bool isBlinking = false;
    public int repetition = 4;
    public string Scene2GO = "Level_1";

    private int repetitionCounter;

    public AudioClip clip;

    void Start()
    {
        imageToToggle.enabled = defaultState;
        repetitionCounter = 0;
        StartBlink();
    }

    public void StartBlink()
    {
        // do not invoke the blink twice - needed if you need to start the blink from an external object
        if (isBlinking)
            return;

        if (imageToToggle != null)
        {
            isBlinking = true;
            InvokeRepeating("ToggleState", startDelay, interval);
        }
    }

    public void ToggleState()
    {
        if (imageToToggle.enabled)
        {
            repetitionCounter++;
        }

        if (repetitionCounter == repetition)
        {
            CancelInvoke();
            Invoke("Go2Scene", interval);
        }
        

        imageToToggle.enabled = !imageToToggle.enabled;


        // plays the clip at (0,0,0)
        if (clip)
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        
       
    }

    public void Go2Scene()
    {
        SceneManager.LoadScene(Scene2GO);
    }

}
