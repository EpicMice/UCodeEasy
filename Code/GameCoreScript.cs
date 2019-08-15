using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using gamecore;

public class GameCoreScript : MonoBehaviour
{

    public Action Init;
    public Action Run;

    // Start is called before the first frame update
    void Start()
    {

        GameCore GC = new GameCore();

        Init = () => { GC.Init(); };

        Run = () => { GC.Update(); };

        Init();
        Debug.Log(GC);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
}
