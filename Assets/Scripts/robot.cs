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
    GameManger manger;
    private void Start()
    {
        manger = GameObject.Find("GameManger").GetComponent<GameManger>();
    }
    private void OnTriggerEnter(Collider other)
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
            manger.GetStars();
            //SceneManager.LoadScene(nextlvl);
        }
        if (other.tag == "bolt")
        {
            Debug.Log("bolt");
            //btnAnswer = GameObject.Find("answer");
            manger.Score += 1; 
            btnAnswer.SetActive(true);
            Destroy(other.gameObject);
            Debug.Log(manger.Score);
            Debug.Log(manger.Length_alg);
            cr.loop1.paused = true;
        }
    }
}
