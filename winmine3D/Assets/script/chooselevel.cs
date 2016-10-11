using UnityEngine;
using System.Collections;

public class chooselevel : MonoBehaviour {
    public GameObject gamecontrol;
    public GameObject leveltext;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
        switch (gamecontrol.GetComponent<gameControl>().level)
        {
            case 1: leveltext.GetComponent<TextMesh>().text="easy"; break;
            case 2: leveltext.GetComponent<TextMesh>().text = "normal"; break;
            case 3: leveltext.GetComponent<TextMesh>().text = "hard"; break;
        }
    }
    void OnMouseDown()
    {
        if (gamecontrol.GetComponent<gameControl>().level ==3)
        {
            gamecontrol.GetComponent<gameControl>().level = 1;
        }
        else
        {
            gamecontrol.GetComponent<gameControl>().level++;
        }
        //print(gamecontrol.GetComponent<gameControl>().level);
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
