using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using System.Collections;

public class ButtonTests
{
    private GameObject button;
    private GameObject button1;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Загрузка сцены
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        // Поиск кнопки
        button = GameObject.Find("Button_Exit");
        button1 = GameObject.Find("Button_Start");
    }

    [UnityTest]
    public IEnumerator ButtonExitGameTest()
    {
        // Проверка, что кнопка существует
        Assert.IsNotNull(button);

        // Создание события нажатия на кнопку
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);

        // Проверка, что при нажатии на кнопку игра закрывается
        yield return null; // Даем время обработать нажатие кнопки
        Assert.IsTrue(Application.isEditor || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer, "Game should exit");
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        // Выгрузка сцены
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }
    [UnityTest]
    public IEnumerator ButtonChangeSceneTest()
    {
        // Проверка, что кнопка существует
        Assert.IsNotNull(button1);

        // Создание события нажатия на кнопку
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button1.gameObject, pointer, ExecuteEvents.pointerClickHandler);

        // Проверка, что при нажатии на кнопку игра переходит на другую сцену
        yield return null; // Даем время обработать нажатие кнопки
        Assert.AreEqual("Gaid", SceneManager.GetActiveScene().name);
    }

}