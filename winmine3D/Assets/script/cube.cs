using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {
    public bool boom = false;
    public int num =0;
    public bool hide = true;
    public int x;
    public int z;
    public bool mark=false;
    public bool active = false;
    public GameObject tp;

    private bool show = false;
    private GameObject tpC;

    void Start ()
    {
        tpC =  Instantiate(tp);
        tpC.transform.position = transform.position + new Vector3(0,1,-0.5f);
        tpC.transform.SetParent(transform);
        tpC.SetActive(false);

    }
	// Update is called once per frame
	void Update () {
        if (!hide && boom && active&&!show)
        {
            show = true;
            GameObject.Find("gameControl").GetComponent<gameControl>().gameover = true;
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        
    }
    void OnMouseEnter()
    {
        tpC.SetActive(true);
        //GetComponent<Transform>().position = GetComponent<Transform>().position + new Vector3(0, 1, 0);
    }

    void OnMouseExit()
    {
        tpC.SetActive(false);
        //GetComponent<Transform>().position = GetComponent<Transform>().position + new Vector3(0, -1, 0);
    }
    void OnMouseOver() {
        if (Input.GetMouseButtonUp(0)) {
            leftMoseUp();
            if (boom&&!mark)
            {
                if (!show)
                {
                    GetComponent<Renderer>().material.color = Color.red;
                    show = true;
                }
                
                GameObject.Find("gameControl").GetComponent<gameControl>().gameover = true;
                GameObject.Find("time").GetComponent<Timer>().stop = true;
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMoseUp();
        }
    }
    //左键点击事件
    void leftMoseUp() {
        GameObject.Find("gameControl").GetComponent<gameControl>().checkCube(x, z);
        GameObject.Find("gameControl").GetComponent<gameControl>().doublecheck(x, z);
    }
    //右键点击事件
    void rightMoseUp()
    {
        GameObject.Find("gameControl").GetComponent<gameControl>().rightclick(x, z);
    }
}
