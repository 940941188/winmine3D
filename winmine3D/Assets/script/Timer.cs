using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float mTimer;
    
    public bool start=false;
    public bool stop = false;
    public bool run = false;

    private Text timerText;
	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            start = false;
            mTimer = 0;
            run = true;
        }
        if (stop)
        {
            stop = false;
            run = false;
        }
        if (run)
        {
            addtime();
        }
	}
    void addtime()
    {
        mTimer += Time.deltaTime;
        timerText.text = System.Math.Floor(mTimer).ToString();
    }
    public void TextClear()
    {
        timerText.text = System.Math.Floor(mTimer).ToString();
    }
}
