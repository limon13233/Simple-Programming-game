using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    public delegate void FuntionsList();
    private GameObject startbtn;
    public GameObject mainTarget; //target object of the code is for
    public List<Function_> sequence; //list of functions (type Functions_). The code sequence is read from here
    private int isPlaying;
    public GameObject panel;
    public Transform satrtPos ;
    public float distance;
    public MainLoop loop1;
    
    public void Paly()
    {
        
        sequence.Clear();
        sequence = TranslateCodeFromBlocks(panel.transform, sequence);
        
        loop1 = new MainLoop(mainTarget, sequence);
        StartCoroutine(loop1.Play());

        isPlaying = 2; 
    }

    public void Stop()
    {
        //Debug.Log(mainTarget.GetComponent<robot>().sp.position.x);
        //mainTarget.transform.position =mainTarget.GetComponent<robot>().sp.position;
        mainTarget.transform.position =new Vector3(satrtPos.position.x,satrtPos.position.y,satrtPos.position.z);
        mainTarget.transform.rotation = satrtPos.rotation;
        isPlaying = 1;
    }

    void Start()
    {
        startbtn = GameObject.Find("Play");
        isPlaying = 0; 
        sequence = new List<Function_>();
    }
    
    void Update()
    {
       
        if (isPlaying == 2) //play
        {
           
            startbtn.SetActive(false);
            
            loop1.infiniteLoop = panel.GetComponentInChildren<Toggle>().isOn;
            Debug.Log(loop1.infiniteLoop.ToString());
            if (loop1.infiniteLoop && loop1.end)
            {
                StartCoroutine(loop1.Play());
            }
        }
        if (isPlaying == 1) //stop
        {
            StopCoroutine(loop1.Play());
            startbtn.SetActive(true);
        }
    }
    
    //recursive parser function
    private List<Function_> TranslateCodeFromBlocks(Transform parent, List<Function_> sequence_)
    {
        foreach (Transform child in parent)
        {
            char[] separator = { '_', '(' };
            var functionName = child.name.Split(separator, System.StringSplitOptions.RemoveEmptyEntries); //looks like a little face ^^

            if (functionName[0] == "Function")
            {
                string function = functionName[1];
                switch (function)
                {
                    case "ChangeColor":
                        sequence_.Add(new ChangeColor_("ChangeColor"));
                        break;
                    case "MoveRight":
                        sequence_.Add(new MoveRight("MoveRight"));
                        break;
                    case "MoveLeft":
                        sequence_.Add(new MoveLeft("MoveLeft"));
                        break;
                    case "MoveUp":
                        sequence_.Add(new MoveUp("MoveUp"));
                        break;
                    case "MoveDown":
                        sequence_.Add(new MoveDown("MoveDown"));
                        break;
                    case "Jump":
                        sequence_.Add(new Jump("Jump"));
                        break;
                    //case "If":
                    //    sequence_.Add(new If("If"));
                    //    break;
                }
            }
        }
        
        return sequence_;
    }
    
}

public class MainLoop
{
    GameObject mainTarget;
    List<Function_> sequence_;
    public bool infiniteLoop;
    public bool end;
    private float waitTime;
    private int step;
    public bool paused=false;

    public MainLoop(GameObject mainTarget, List<Function_> sequence_)
    {
        this.end = false;
        this.mainTarget = mainTarget;
        this.sequence_ = sequence_;
        this.waitTime = 1.2f;//wait time between functions in sequence (list)
    }
    public IEnumerator Play()
    {

        WaitForSeconds wait = new WaitForSeconds(waitTime);
        this.end = false;
        foreach (Function_ fun in this.sequence_)
        {
            while (paused)
            {
                yield return null;
            }

            fun.Func(this.mainTarget);

            if (!paused)
            {
                yield return wait;
            }
        }
        this.end = true;
    }


}
//public class MainLoop
//{
//    GameObject mainTarget;
//    List<Function_> sequence_;
//    public bool infiniteLoop;
//    public bool end;
//    private float waitTime;
//    private int step;
//    private bool paused;
//    private Coroutine currentCoroutine;

//    public MainLoop(GameObject mainTarget, List<Function_> sequence_)
//    {
//        this.end = false;
//        this.mainTarget = mainTarget;
//        this.sequence_ = sequence_;
//        this.waitTime = 1.2f;//wait time between functions in sequence (list)
//    }

//    public void Pause()
//    {
//        paused = true;
//    }

//    public void Resume()
//    {
//        paused = false;
//        if (currentCoroutine != null)
//        {
//            // Start coroutine from the point where it was paused
//            MonoBehaviour.print("Resuming coroutine");
//            StartCoroutine(currentCoroutine);
//            StartCoroutine(currentCoroutine);
//        }
//    }

//    public IEnumerator Play()
//    {
//        WaitForSeconds wait = new WaitForSeconds(waitTime);
//        this.end = false;
//        paused = false;
//        currentCoroutine = PlaySequence();
//        StartCoroutine(currentCoroutine);
//        yield return currentCoroutine;
//        this.end = true;
//    }

//    private IEnumerator PlaySequence()
//    {
//        foreach (Function_ fun in this.sequence_)
//        {
//            fun.Func(this.mainTarget);
//            yield return waitTime;
//            // Check if the coroutine is paused
//            while (paused)
//            {
//                yield return null;
//            }
//        }
//    }
//}

public class Jump : Function_
{
    public Jump(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        mainTarget.GetComponent<Rigidbody>().transform.Translate(new Vector3(0,1,1) * 1.2f);
    }
}

public class MoveUp : Function_
{
    public MoveUp(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        mainTarget.GetComponent<Rigidbody>().transform.Rotate(Vector3.up,-90);
    }
}

public class MoveDown : Function_
{
    public MoveDown(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        Vector3 rotate = mainTarget.transform.eulerAngles;
        rotate.y += 90;
        mainTarget.transform.rotation = Quaternion.Euler(rotate);
    }
}

public class MoveRight : Function_
{
    public MoveRight(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        mainTarget.GetComponent<Rigidbody>().transform.Translate(Vector3.forward * 1.2f);
    }
}

public class MoveLeft : Function_
{
    public MoveLeft(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        mainTarget.GetComponent<Rigidbody>().transform.Translate(Vector3.back * 1.2f);
    }
}

public class ChangeColor_ : Function_
{
    public ChangeColor_(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void Func(GameObject mainTarget)
    {
        mainTarget.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
//public class If : Function_
//{
//    public If(string ID) : base(ID)
//    {
//        this.ID = ID;
//    }

//    override public void Func(GameObject mainTarget)
//    {
//        bool condition = true;// implement the condition here
//        if (condition)
//        {
//            // execute the true block of the If statement
//            Transform trueBlock = this.transform.Find("TrueBlock");
//            if (trueBlock != null)
//            {
//                TranslateCodeFromBlocks(trueBlock, new List<Function_>());
//            }
//        }
//        else
//        {
//            // execute the false block of the If statement
//            Transform falseBlock = this.transform.Find("FalseBlock");
//            if (falseBlock != null)
//            {
//                TranslateCodeFromBlocks(falseBlock, new List<Function_>());
//            }
//        }
//    }
//}


public class Function_
{
    public string ID;

    //contructor for sinple functions
    public Function_(string ID)
    {
        this.ID = ID;
    }

    public virtual void Func(GameObject mainTarget)
    {

    }

}
