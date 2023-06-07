using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class robot : MonoBehaviour
{
    public GameObject fs;
    public GameObject ms;
    public int nextlvl;

    public Controller cr;
    public GameObject btnAnswer;
    GameManger manger;
    public string[] question;
    public ArrayLayout answer;
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
            manger.Score += 1;
            Debug.Log("bolt");
            for (int i=0;i<manger.maxLength;i++)
            if (manger.Score == i)
            {
                    
                    btnAnswer.SetActive(true);
                    Destroy(other.gameObject);
                    if (GameObject.Find("Quest") != null)
                    {
                        GameObject.Find("Quest").GetComponent<TextMeshProUGUI>().text = question[i - 1];
                        GameObject.Find("TogleText1").GetComponent<Text>().text = answer.rows[i - 1].row[0];
                        GameObject.Find("TogleText2").GetComponent<Text>().text = answer.rows[i - 1].row[1];
                        GameObject.Find("TogleText3").GetComponent<Text>().text = answer.rows[i - 1].row[2];
                        GameObject.Find("TogleText4").GetComponent<Text>().text = answer.rows[i - 1].row[3];
                    }
                    
                Debug.Log(manger.Score);
                Debug.Log(manger.Length_alg);
                
            }
            cr.loop1.paused = true;
        }
    }
}
