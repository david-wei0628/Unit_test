using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTest
{
    public interface IInputSystem
    {
        float GetHorizontalValue();
        float GetVerticalValue();
        float GetScrollWheelValue();
    }

    public class InputSystem : IInputSystem
    {
        public float GetHorizontalValue()
        {
            float moveInputRL = Input.GetAxisRaw(axisName: "Horizontal");
            return moveInputRL;
        }
        public float GetVerticalValue()
        {
            float moveInputUD = Input.GetAxisRaw(axisName: "Vertical");
            return moveInputUD;
        }
        public float GetScrollWheelValue()
        {
            float MouseScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
            return MouseScrollWheel;
        }


    }

}
