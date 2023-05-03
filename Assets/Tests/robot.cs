using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class robot : MonoBehaviour
{
    public GameObject fs;
    public GameObject ms;
    public int nextlvl;

    public Controller cr;
    public GameObject btnAnswer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Respawn")
        {
            Debug.Log("respawn");
            cr.StopAllCoroutines();
            cr.Stop();
          
        }
        if (other.tag == "finish")
        {
            cr.StopAllCoroutines();
            Debug.Log("finish");
            ms.SetActive(false);
            fs.SetActive(true);
            //SceneManager.LoadScene(nextlvl);
        }
        if (other.tag == "bolt")
        {
            Debug.Log("bolt");
            //btnAnswer = GameObject.Find("answer");
            btnAnswer.SetActive(true);
            cr.loop1.paused = true;
        }
    }
}
