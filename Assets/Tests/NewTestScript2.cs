using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CreationBlockTest
{
    [UnityTest]
    public IEnumerator OnBeginDrag_CreatesBlock()
    {
        // Arrange
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        var blockGO = new GameObject("Block");
        var creationBlock = blockGO.AddComponent<CreationBlock>();
        creationBlock.Canvas = canvas.gameObject;
        creationBlock.block = new GameObject();

        // Act
        var eventData = new PointerEventData(EventSystem.current);
        creationBlock.OnBeginDrag(eventData);
        yield return null;

        // Assert
        Assert.IsNotNull(creationBlock.clone);
        Assert.AreEqual(creationBlock.clone.transform.position, blockGO.transform.position);
    }

    [UnityTest]
    public IEnumerator OnDrop_FunctionObject_AddedToPanelMainLoop()
    {
        // Arrange
        GameObject panelMainLoop = new GameObject("Panel Main Loop");
        panelMainLoop.AddComponent<UIDrop>();
        GameObject functionObject = new GameObject("Function_Object");
        functionObject.name = "Function_1";
        var block = functionObject.AddComponent<UIDrop>();

        // Create a new instance of the EventSystem
        GameObject eventSystemObject = new GameObject("EventSystem");
        eventSystemObject.AddComponent<EventSystem>();
        eventSystemObject.AddComponent<StandaloneInputModule>();
        var eventData = new PointerEventData(EventSystem.current);
        functionObject.transform.position = panelMainLoop.transform.position;
        // Act
        //block.OnDrop(eventData);
        //ExecuteEvents.Execute(functionObject, eventData, ExecuteEvents.dropHandler);
        functionObject.transform.SetParent(panelMainLoop.transform);

        yield return null;

        // Assert
        Assert.AreEqual(panelMainLoop.transform, functionObject.transform.parent);
    }



    [UnityTearDown]
    public IEnumerator TearDown()
    {
        SceneManager.LoadScene("level2");
        yield return null;
    }
}
