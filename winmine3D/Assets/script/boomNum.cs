using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class boomNum : MonoBehaviour {
    public GameObject gamecontrol;
    private int num;
    private TextMesh boomNumText;
    // Use this for initialization
    void Start () {
	    boomNumText = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        num = gamecontrol.GetComponent<gameControl>().BoomN;
        boomNumText.text = num.ToString() ;
    }
}
