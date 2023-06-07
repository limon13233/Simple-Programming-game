using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject bolts;
    GameManger manger;


    public void Paly()
    {
        
        sequence.Clear();
        sequence = TranslateCodeFromBlocks(panel.transform, sequence);
        
        loop1 = new MainLoop(mainTarget, sequence);
        manger.Length_alg = sequence.Count;
        StartCoroutine(loop1.Play());

        isPlaying = 2; 
    }

    public void Stop()
    {
        mainTarget.transform.position =new Vector3(satrtPos.position.x,satrtPos.position.y,satrtPos.position.z);
        mainTarget.transform.rotation = satrtPos.rotation;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("bolt"))
        {
            Destroy(b);
        }
        GameObject respawnbolts = Instantiate(bolts);
        manger.Score = 0;
        respawnbolts.SetActive(true);
        isPlaying = 1;
    }

    void Start()
    {
        manger = GameObject.Find("GameManger").GetComponent<GameManger>();
        startbtn = GameObject.Find("Play");
        isPlaying = 0; 
        sequence = new List<Function_>();
    }
    
    void Update()
    {
      
        if (isPlaying == 2) //play
        {
           
            startbtn.SetActive(false);

            if (panel.GetComponentInChildren<Toggle>() != null)
            {
                loop1.infiniteLoop = panel.GetComponentInChildren<Toggle>().isOn;
            }
            if (loop1.infiniteLoop && loop1.end)
            {
                StartCoroutine(loop1.Play());
            }
        }
        if (isPlaying == 1) //stop
        {
            loop1.end = false;
            startbtn.SetActive(true);
            StopCoroutine(loop1.Play());
            StopAllCoroutines();
            
            
        }
    }
    
    //recursive parser function
    private List<Function_> TranslateCodeFromBlocks(Transform parent, List<Function_> sequence_)
    {
        foreach (Transform child in parent)
        {
            char[] separator = { '_', '(' };
            var functionName = child.name.Split(separator, System.StringSplitOptions.RemoveEmptyEntries); 

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
                    case "If":

                        sequence_.Add(new If("If"));
                        break;
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
        int ifcount = 0;
        foreach (Function_ fun in this.sequence_)
        {
           
            while (paused)
            {
                yield return null;
            }

            if (!paused)
            {
                if(fun.ID!="If")
                {
                    fun.Func(this.mainTarget);
                }
                else
                {
                    
                    GameObject[] obj = GameObject.FindGameObjectsWithTag("If_block");
                    TMP_Dropdown tmpDropdown = obj[ifcount].GetComponentInChildren<TMP_Dropdown>();
                    int selectedIndex = tmpDropdown.value;
                    Function_ dey = new Function_("ID");
                    foreach (Transform child in obj[ifcount].transform)
                    {
                        char[] separator = { '_', '(' };
                        var functionName = child.name.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);

                        if (functionName[0] == "Function")
                        {
                            string function = functionName[1];
                            switch (function)
                            {
                                case "MoveRight":
                                    dey = new MoveRight("MoveRight");
                                    break;
                                case "MoveLeft":
                                    dey = new MoveLeft("MoveLeft");
                                    break;
                                case "MoveUp":
                                    dey = new MoveUp("MoveUp");
                                    break;
                                case "MoveDown":
                                    dey = new MoveDown("MoveDown");
                                    break;
                                case "Jump":
                                    dey = new Jump("Jump");
                                    break;
                            }
                        }
                    }
                    fun.FuncIF(dey, this.mainTarget,selectedIndex.ToString());
                    ifcount++;
                }
                yield return wait;
            }
        }
        this.end = true;
    }


}
public class If: Function_
{

    public If(string ID) : base(ID)
    {
        this.ID = ID;
    }

    override public void FuncIF(Function_ f, GameObject mainTarget, string u)
    {
        RaycastHit hit;
        Vector3 rayOrigin;
        Vector3 rayDirection;
        switch (u)
        {
            case "1":
                rayOrigin = mainTarget.transform.position;
                rayDirection = mainTarget.transform.forward;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, 1f))
                {

                    Debug.Log("Luch pp");


                    if (hit.collider.CompareTag("Wall"))
                    {
                        Debug.Log("Стена обнаружена перед персонажем!");
                        f.Func(mainTarget);
                    }
                    else
                    {
                        Debug.Log("Стена ne обнаружена");
                    }
                }
                else
                {
                    Debug.Log("luch ne popal");
                }
                break;
            case "0":
                Vector3 forwardDirection = mainTarget.transform.forward; // Направление персонажа
                rayOrigin = mainTarget.transform.position + Vector3.up * 1f;
                rayDirection = Quaternion.Euler(45f, 0f, 0f) * Vector3.forward;
                rayDirection = Quaternion.LookRotation(forwardDirection) * rayDirection;
                if (Physics.Raycast(rayOrigin, rayDirection, out hit, 2.2f))
                {
                    Debug.DrawRay(rayOrigin, rayDirection, Color.red, 2f);
                    Debug.Log("Пропости нет");
                }
                else
                {
                    Debug.DrawRay(rayOrigin, rayDirection, Color.red, 2f);
                    f.Func(mainTarget);
                    Debug.Log("пропость обнаружена");
                }
                break;
        }


    }
}

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
    public virtual void FuncIF(Function_ f,GameObject mainTarget, string u)
    {

    }

}
