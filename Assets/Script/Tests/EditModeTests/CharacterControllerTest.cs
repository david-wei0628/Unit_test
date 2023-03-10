using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnitTest;
using CharacterController = UnitTest.CharacterContorller;


public class CharacterControllerTest
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
    
    [Test]
    [Category(name:"characterContorller")]
    public void Test3()
    {
        var characterController = new GameObject().AddComponent<CharacterController>();
        characterController.init(); 
        characterController.SetMoveSpeed(moveSpeed: 10);
        //var inputSystem = Substitute.For<IInputSystem>();
        //inputSystem.GetHorizontalValue().Returns(1);
        var inputSystem = Substitute.For<IInputSystem>();
        //characterController.SetInputSystem(inputSystem: Substitute.For<InputSystem>());
        inputSystem.GetHorizontalValue().Returns(returnThis: 1);
        inputSystem.GetVerticalValue().Returns(returnThis: 1);
        characterController.SetInputSystem(inputSystem);
        characterController.Update();
        var rigidbody = characterController.GetComponent<Rigidbody>();
        //var rigidbody = characterController.GetComponent<Transform>();

        Assert.AreEqual(new Vector3(10, 0, 10), rigidbody.velocity);
    }
    [Test]
    [Category(name:"RayMove")]
    public void _04_rayMoveTest()
    {
        var rayMove = new GameObject().AddComponent<RayMove>();
        var inputSystem = Substitute.For<IInputSystem>();
        inputSystem.GetScrollWheelValue().Returns(returnThis: 1);
        rayMove.SetInputSystem(inputSystem);
        rayMove.ScrollView();
        var WheelValue = rayMove.GetComponent<Rigidbody>();
        Assert.AreEqual(new Vector3(10, 0, 0), WheelValue.velocity);
    }

    


}
