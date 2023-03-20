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

        var inputSystem = Substitute.For<IInputSystem>();

        inputSystem.GetHorizontalValue().Returns(returnThis: 1);
        inputSystem.GetVerticalValue().Returns(returnThis: 1);
        characterController.SetInputSystem(inputSystem);
        characterController.Update();
        var rigidbody = characterController.GetComponent<Rigidbody>();
        //var rigidbody = characterController.GetComponent<Transform>();
        var transbody = characterController.GetComponent<Transform>();

        Assert.AreEqual(new Vector3(10, 0, 10), rigidbody.velocity);
    }
    [Test]
    [Category(name:"RayMove")]
    public void _04_rayMoveTest_Camear_ScrollWheel()
    {
        var rayMove = new GameObject().AddComponent<RayMove>();
        rayMove.init_Unit();
        var inputSystem = Substitute.For<IInputSystem>();
        inputSystem.GetScrollWheelValue().Returns(returnThis: -0.1f);
        rayMove.SetInputSystem(inputSystem);

        rayMove.ScrollView();
        var WheelValue = rayMove.CameTrans.GetComponent<Transform>();
        Assert.AreEqual(new Vector3(0, 4, 0), WheelValue.position);
    }
    [Test]
    [Category(name: "RayMove")]
    public void _05_rayMoveTest_HandleMovement()
    {
        var rayMove =new GameObject().AddComponent<RayMove>();
        
        rayMove.init_Unit();

        rayMove.SetMoveSpeed(moveSpeed: 10);
        var inputSystem = Substitute.For<IInputSystem>();
        inputSystem.GetHorizontalValue().Returns(returnThis: 1);
        inputSystem.GetVerticalValue().Returns(returnThis: 0);
        rayMove.SetInputSystem(inputSystem);
        rayMove.PlayUnitMove();
        var PlayUnitMove = rayMove.transform.GetComponent<Transform>();
        
        Assert.AreEqual(new Vector3(10, 0.5f, 0), PlayUnitMove.position);
    }




}
