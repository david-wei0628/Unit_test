using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test1()
    {
        var gameobjectName = "MyChar";
        var gameobject = new GameObject(name:gameobjectName);
        Assert.AreEqual(expected:"MyChar",actual:gameobject.name);
    }
    
    [Test]
    public void Test2()
    {
        var gameObject = new GameObject().AddComponent<CharacterController>();
        var characterController = gameObject.GetComponent<CharacterController>();
        Assert.IsNotNull(anObject: characterController, "characterController is no exits. ");
    }

    


}
