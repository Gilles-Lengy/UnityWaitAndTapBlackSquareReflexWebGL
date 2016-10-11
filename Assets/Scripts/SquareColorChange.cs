using UnityEngine;
using System.Collections;

public class SquareColorChange : MonoBehaviour {

    public Color colorEnter = new Color(1.0f, 1.0f, 1.0f);// Editor !!! . Helpfull -> http://lslwiki.net/lslwiki/wakka.php?wakka=color

    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
    }
    void OnMouseEnter()
    {
            rend.material.color = colorEnter;
    }
    void OnMouseExit()
    {
            rend.material.color = Color.black;
       
    }


}
