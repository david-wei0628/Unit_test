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
        Vector2 GetMousePosition();
        float GetMouseXValue();
        float GetMouseYValue();
    }

    public class InputSystem : IInputSystem
    {
        public float GetHorizontalValue()
        {
            float moveInputRL = Input.GetAxis(axisName: "Horizontal");
            return moveInputRL;
        }
        public float GetVerticalValue()
        {
            float moveInputUD = Input.GetAxis(axisName: "Vertical");
            return moveInputUD;
        }
        public float GetScrollWheelValue()
        {
            float MouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            return MouseScrollWheel;
        }

        public Vector2 GetMousePosition()
        {
            Vector2 mousePosition = Input.mousePosition;
            return mousePosition;
        }

        public float GetMouseXValue()
        {
            float GetMouseXValue = Input.GetAxis("Mouse X");
            return GetMouseXValue;
        }

        public float GetMouseYValue()
        {
            float GetMouseYValue = Input.GetAxis("Mouse Y");
            return GetMouseYValue;
        }
    }

}
