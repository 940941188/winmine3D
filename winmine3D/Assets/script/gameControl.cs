using UnityEngine;
using System.Collections;
using System;//注意这里需要引进DateTime对象的内容了
using UnityEngine.UI;

public class gameControl : MonoBehaviour {
    public GameObject cubeP;
    public GameObject cubeFirst;
    public Material[] cubeMaterial ;
    public bool gameover = false;
    public Text result;

    public int BoomN = 0;//剩下未排除的地雷

    public int level = 1;
    public GameObject timer;

    private int boomNum = 10;//总地雷数量
    private int height = 9;
    private int width = 9;

    private GameObject[,] cubeArray;
    private DateTime t1, t2;
    private bool show = false;
    private int excludedCube = 0;
    private int excludedboom = 0;//排除地雷书
    private bool win= false;
    private int winCubeNum;
    private bool end=false;

    // Use this for initialization
    void Start () {
        t1 = DateTime.Now;

        initGame();
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked;//固定鼠标在中间
        BoomN = boomNum - excludedboom;
        if (!show && (win||gameover))
        {
            show = true;
            activeAll();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("quit");
            Application.Quit();
        }
	}
    //初始化
    void initGame()
    {
        gameover = false;
        win = false;
        show = false;
        excludedCube = 0;
        excludedboom = 0;
        end = false;

        clearAG();
        choiceLevel();
        initCubeArray();
        initBoom();

        BoomN = boomNum - excludedboom;
        result.GetComponent<Text>().text = "";
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                TraversalCube(x, z);
            }
        }
    }
    //从新开始
    public void restart()
    {
        timer.GetComponent<Timer>().run = false;
        timer.GetComponent<Timer>().mTimer = 0;
        timer.GetComponent<Timer>().TextClear();

        initGame();
    }
    //清空 数组 gameobject
    void clearAG()
    {
        for (int i = 0; i < cubeFirst.transform.childCount; i++)
        {
            GameObject go = cubeFirst.transform.GetChild(i).gameObject;
            Destroy(go);
        }
        cubeArray = null;
    }
    //选择难度
    void choiceLevel()
    {
        switch (level)
        {
            case 1: boomNum = 10; width =  9; height =  9; break;
            case 2: boomNum = 40; width = 16; height = 16; break;
            case 3: boomNum = 99; width = 30; height = 16; break;
        }
    }
    //初始化 雷区方块 
    void initCubeArray() {
        cubeArray = new GameObject[30, 16];
        winCubeNum = width * height - boomNum;
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
             
                cubeArray[x, z] = Instantiate(cubeP);
                cubeArray[x, z].GetComponent<cube>().x = x;
                cubeArray[x, z].GetComponent<cube>().z = z;
                cubeArray[x,z].GetComponent<Transform>().position = cubeFirst.GetComponent<Transform>().position + new Vector3(x, 0, z);

                cubeArray[x, z].transform.SetParent(cubeFirst.transform);
            };
        };
    }
    //初始化 地雷 随机
    void initBoom() {
        int x;
        int z;
        for (int n = 0; n < boomNum; n++) {
            x = UnityEngine.Random.Range(0, width-1); 
            z = UnityEngine.Random.Range(0, height-1);
            if (cubeArray[x, z].GetComponent<cube>().boom)
            {
                n--;
            }
            else {
                cubeArray[x, z].GetComponent<cube>().boom = true;
                cubeArray[x, z].GetComponent<cube>().num = 9;
                //cubeArray[x, z].GetComponent<Renderer>().material = cubeMaterial[cubeArray[x, z].GetComponent<cube>().num];//删
            }
        }
    }
    //周围 方块 遍历 储存附近炸弹数量
    void TraversalCube(int x ,int z) {
        int boomNum = 0;
        if(cubeArray[x, z].GetComponent<cube>().boom == false)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (x - i < 0 || z - j < 0 || x - i > width-1 || z - j > height-1)
                    {
                        continue;
                    }
                    else if (cubeArray[x - i, z - j].GetComponent<cube>().boom)
                    {
                        boomNum++;
                    };
                }
            }
            cubeArray[x, z].GetComponent<cube>().num = boomNum;
        }

    }
    //点击 判断 零方块
    public void checkCube(int x, int z) {
        GameObject cube = cubeArray[x, z];

        if (cube.GetComponent<cube>().hide && !cube.GetComponent<cube>().active) {
            showNum(cube);
            if (excludedCube == 1)
            {
                GameObject.Find("time").GetComponent<Timer>().start = true;
            }
            if (cube.GetComponent<cube>().num == 0)
            {
                activeArround(x, z);
            }
        } 
    }
    //激活四周
    void activeArround(int x,int z)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (x - i < 0 || z - j < 0 || x - i > width-1 || z - j > height-1)
                {
                    continue;
                }
                else {
                    checkCube(x - i, z - j);
                };
            }
        }
    }
    //双击 检测
    public void doublecheck(int x, int z)
    {
        t2 = DateTime.Now;

        if (t2 - t1 < new TimeSpan(0, 0, 0, 0, 500))
        {
            doubleClick(x, z);
        }
        t1 = t2;
    }
    //双击 判断
    void doubleClick(int x, int z)
    {
        GameObject cubeDoubleClick = cubeArray[x, z];
        int markN = marknum(x, z);
        if (!cubeDoubleClick.GetComponent<cube>().hide && markN >= cubeDoubleClick.GetComponent<cube>().num && cubeDoubleClick.GetComponent<cube>().num != 0)
        {
            activeArround(x, z);
        }
    }
    //四周标记统计
    int marknum(int x, int z)
    {
        int num = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (x - i < 0 || z - j < 0 || x - i > width-1 || z - j > height-1)
                {
                    continue;
                }
                else if (cubeArray[x - i, z - j].GetComponent<cube>().mark)
                {
                    num++;
                };
            }
        }
        return num;
    }
    //右键点击
    public void rightclick(int x,int z)
    {
        GameObject cube = cubeArray[x, z];
        if (cube.GetComponent<cube>().hide&&!end)//bug
        {
            cube.GetComponent<cube>().active = !cube.GetComponent<cube>().active;
            cube.GetComponent<cube>().mark = !cube.GetComponent<cube>().mark;
            if(cube.GetComponent<cube>().active&& cube.GetComponent<cube>().mark)
            {
                excludedboom++;
                cube.GetComponent<Transform>().position = cube.GetComponent<Transform>().position + new Vector3(0,0.4f,0);
            }
            else if (!cube.GetComponent<cube>().active && !cube.GetComponent<cube>().mark)
            {
                excludedboom--;
                cube.GetComponent<Transform>().position = cube.GetComponent<Transform>().position + new Vector3(0, -0.4f, 0);
                //cube.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        
    }
    //遍历所有cube
    void activeAll()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (win&& cubeArray[x, z].GetComponent<cube>().boom && !cubeArray[x, z].GetComponent<cube>().mark)
                {
                    rightclick(x, z);
                }
                if (!cubeArray[x, z].GetComponent<cube>().active)
                {
                    showNum(cubeArray[x, z]);
                }
                else if (cubeArray[x, z].GetComponent<cube>().mark&& !cubeArray[x, z].GetComponent<cube>().boom)
                {
                    cubeArray[x, z].GetComponent<Renderer>().material.color = Color.gray;
                }
            }
        }
        end = true;
        if (win)
        {
            result.GetComponent<Text>().text = "Win";
        }
        else if (gameover){
            result.GetComponent<Text>().text = "Game Over";
        }
    }
    //显示数字
    void showNum(GameObject cube)
    {
        if (!cube.GetComponent<cube>().boom&&!cube.GetComponent<cube>().active)
        {
            excludedCube++;
            
            cube.GetComponent<Renderer>().material = cubeMaterial[cube.GetComponent<cube>().num];
        }
        cube.GetComponent<cube>().hide = false;
        cube.GetComponent<cube>().active = true;
        if(excludedCube == winCubeNum&&!gameover){
            win = true;
            GameObject.Find("time").GetComponent<Timer>().stop = true;
        }
    }
}
