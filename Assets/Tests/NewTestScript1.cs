using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.TestTools;

public class RespawnTest
{

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // загружаем сцену с блоком и канвасом
        yield return SceneManager.LoadSceneAsync("level2", LoadSceneMode.Single);

    }
    [UnityTest]
    public IEnumerator PlayerMovesOnCollision()
    {
        // Arrange
        GameObject player = GameObject.Find("Jammo_Player");
        GameObject rs = GameObject.Find("RsCube");
        Transform sp = player.transform;
        // Act
        player.GetComponent<Rigidbody>().AddForce(Vector3.left*50,ForceMode.Impulse);
        //player.GetComponent<Rigidbody>().transform.position = rs.transform.position;
        //Assert
        yield return null;
        Assert.AreEqual(player.transform.position, sp.position);
    }
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        // Выгрузка сцены
        yield return SceneManager.UnloadSceneAsync("level2");
        //yield return SceneManager.LoadSceneAsync("level2", LoadSceneMode.Single);
    }
    [UnityTest]
    public IEnumerator LevelComplite()
    {
        GameObject player = GameObject.Find("Jammo_Player");
        GameObject finish = GameObject.Find("fsCube");
        
        // Act
        player.GetComponent<Rigidbody>().transform.position = finish.transform.position;
        yield return null;
        GameObject imgFinish = GameObject.Find("finalScreen");
        Assert.AreEqual(true, imgFinish.active);
    }
}
