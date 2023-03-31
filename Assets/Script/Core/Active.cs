using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitTest;

public class Active : MonoBehaviour
{
    private UnitInput UnitInput;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {   
        if (UnitInput.GetVerticalValue() != 0 || UnitInput.GetHorizontalValue() != 0)
        {
            ActiveMove();
        }
    }

    public void init()
    {
        this.transform.position = transform.position;
        UnitInput = new ActPassUnitInput();
    }

    public void ActiveMove()
    {
        this.transform.Translate(UnitInput.GetHorizontalValue(), 0, UnitInput.GetVerticalValue());
    }

    public void SetInputSystem(UnitInput inputSystem)
    {
        this.UnitInput = inputSystem;
    }

}
