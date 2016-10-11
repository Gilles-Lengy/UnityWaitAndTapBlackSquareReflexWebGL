using UnityEngine;
using System.Collections;

public class SquareColorChange : MonoBehaviour {

    public Color colorEnter = new Color(1.0f, 1.0f, 1.0f);// Editor !!! . Helpfull -> http://lslwiki.net/lslwiki/wakka.php?wakka=color
    public Color colorTaped = new Color(1.0f, 1.0f, 1.0f);
    public bool mouseDowned;

    public Renderer rend;
    void Start()
    {
        mouseDowned = false;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
    }
    void OnMouseEnter()
    {
        if (!mouseDowned)
        {
            rend.material.color = colorEnter;
        }
    }
    void OnMouseExit()
    {
        if (!mouseDowned) {
            rend.material.color = Color.black;
        }
       
    }
    void OnMouseDown()
    {
        
        if (!mouseDowned)
        {
            mouseDowned = true;
            rend.material.color = Color.white;
            Invoke("ToTapedtColor", 0.07F);
        }
    }

    void ToTapedtColor()
    {
        rend.material.color = colorTaped;
    }
}
