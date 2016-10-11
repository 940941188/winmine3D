using UnityEngine;
using System.Collections;

public class restart : MonoBehaviour {
    public GameObject gamecontrol;

    void OnMouseDown()
    {
        gamecontrol.GetComponent<gameControl>().restart();
    }
    //void OnMouseEnter()
    //{
    //    GetComponent<Renderer>().material.color = Color.black;
    //}

    //void OnMouseExit()
    //{
    //    GetComponent<Renderer>().material.color = Color.red;
    //}
}
