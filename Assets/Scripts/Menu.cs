using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject teoriya;
    public GameObject Vstuplenie;
    public GameObject inst;
    private int gaid=1;
    public GameObject[] steps;
    public Controller cr;
    public GameObject question;
    public Toggle[] answerq;
    public GameObject can;
    public GameObject can2;

    GameManger manger;
    public void Start()
    {
        manger = GameObject.Find("GameManger").GetComponent<GameManger>();
    }
    public void OnPlay(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void next()
    {
        teoriya.SetActive(false);
        Vstuplenie.SetActive(true);
    }
    public void GaidNext()
    {
        if (gaid == steps.Length)
        {
            inst.SetActive(false);
        }
        else
        {
            steps[gaid].SetActive(true);
            for (int i = 0; i < steps.Length; i++)
            {
                if (i != gaid)
                {
                    steps[i].SetActive(false);
                }
            }
            gaid++;
            
        }
        
    }
    public void answer()
    {
       
            if (answerq[manger.Score-1].isOn)
            {
                question.SetActive(false);
                cr.loop1.paused = false;
            }
            else
            {
                question.SetActive(false);
                cr.Stop();
            }
        
    }
    public void Showlevels()
    {
        can.SetActive(false);
        can2.SetActive(true);
    }
    public void Back()
    {
        can.SetActive(true);
        can2.SetActive(false);
    }

}
