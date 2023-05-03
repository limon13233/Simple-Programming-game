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
        // �������� �����
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        // ����� ������
        button = GameObject.Find("Button_Exit");
        button1 = GameObject.Find("Button_Start");
    }

    [UnityTest]
    public IEnumerator ButtonExitGameTest()
    {
        // ��������, ��� ������ ����������
        Assert.IsNotNull(button);

        // �������� ������� ������� �� ������
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);

        // ��������, ��� ��� ������� �� ������ ���� �����������
        yield return null; // ���� ����� ���������� ������� ������
        Assert.IsTrue(Application.isEditor || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer, "Game should exit");
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        // �������� �����
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }
    [UnityTest]
    public IEnumerator ButtonChangeSceneTest()
    {
        // ��������, ��� ������ ����������
        Assert.IsNotNull(button1);

        // �������� ������� ������� �� ������
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button1.gameObject, pointer, ExecuteEvents.pointerClickHandler);

        // ��������, ��� ��� ������� �� ������ ���� ��������� �� ������ �����
        yield return null; // ���� ����� ���������� ������� ������
        Assert.AreEqual("Gaid", SceneManager.GetActiveScene().name);
    }

}