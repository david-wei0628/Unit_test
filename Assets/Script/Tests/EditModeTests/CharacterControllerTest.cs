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
    public void _01_CharacterContorller_Test_AreEqual()
    {
        var gameobjectName = "MyChar";
        var gameobject = new GameObject(name:gameobjectName);
        Assert.AreEqual(expected:"MyChar",actual:gameobject.name);
    }
    
    [Test]
    public void _02_CharacterContorller_Test_IsNotNull()
    {
        var gameObject = new GameObject().AddComponent<CharacterController>();
        var characterController = gameObject.GetComponent<CharacterController>();
        Assert.IsNotNull(anObject: characterController, "characterController is no exits. ");
    }
    
    [Test]
    [Category(name:"characterContorller")]
    public void _03_CharacterContorller_User_UnitMove()
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
        inputSystem.GetScrollWheelValue().Returns(returnThis: -1f);
        rayMove.SetInputSystem(inputSystem);

        rayMove.ScrollView();
        var WheelValue = rayMove.CameTrans.GetComponent<Transform>();

        //Debug.Log(WheelValue.position);

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
        inputSystem.GetVerticalValue().Returns(returnThis: -1);
        rayMove.SetInputSystem(inputSystem);
        rayMove.PlayKeyBoardMove();
        var PlayUnitMove = rayMove.transform.GetComponent<Transform>();
        Debug.Log(PlayUnitMove.localEulerAngles.y);
        Assert.AreEqual(new Vector3(10, 0.5f, 10), PlayUnitMove.position);
    }
    
    [Test]
    [Category(name: "RayMove")]
    public void _06_rayMoveTest_ScreenPointToRay()
    {
        var rayMove =new GameObject().AddComponent<RayMove>();
        
        rayMove.init_Unit();
        rayMove.SetMoveSpeed(moveSpeed: 10);
        var inputSystem = Substitute.For<IInputSystem>();

        inputSystem.GetMousePosition().Returns(new Vector2(418, 231));
        rayMove.SetInputSystem(inputSystem);
        rayMove.SelectRay();

        var PlayUnitMoveTransform = rayMove.transform.GetComponent<Transform>();

        Assert.AreEqual(new Vector3(0, 0, 0), PlayUnitMoveTransform.localEulerAngles);
        Assert.AreEqual(new Vector3(100, 0.5f, 0), PlayUnitMoveTransform.position);
    }




}
