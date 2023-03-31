using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTest
{
    public interface UnitInput
    {
        float GetHorizontalValue();
        float GetVerticalValue();
    }

    public class ActPassUnitInput : UnitInput
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
    }
}

