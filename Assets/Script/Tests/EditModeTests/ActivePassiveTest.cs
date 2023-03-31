using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnitTest;

public class ActivePassiveTest
{
    [Test]
    public void _01_ActivePassiveTest_Test_AreEqual()
    {
        //取得現有物件元件資訊
        var UserActive1 = new GameObject();
        UserActive1 = GameObject.Find("Active");
        //UserActive1.AddComponent<Active>();

        var UserActive = UserActive1.AddComponent<Active>();
        var UserPass = new GameObject().AddComponent<Passive>();
        UserActive.init();
        var inputSystem = Substitute.For<UnitInput>();

        inputSystem.GetVerticalValue().Returns(returnThis: 1f);
        inputSystem.GetHorizontalValue().Returns(returnThis: 1f);

        UserActive.SetInputSystem(inputSystem);
        UserActive.ActiveMove();

        UserPass.Start();
        UserPass.Update();
        //UserPass.PassMove();
        var ActiveUnitMove = UserActive.transform.GetComponent<Transform>();
        var PassiveUnitMove = UserPass.transform.GetComponent<Transform>();

        //Debug.Log(ActiveUnitMove.position);
        //Debug.Log(PassiveUnitMove.position);
        Assert.AreEqual(new Vector3(5, 0, 5), ActiveUnitMove.position);
    }
}
