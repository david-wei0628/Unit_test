using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitTest;

public class Passive : MonoBehaviour
{
    // Start is called before the first frame update
    float ClockTime;
    GameObject ActiveGameObject;
    public GameObject PassivePrefab;
    public void Start()
    {
        ClockTime = 0f;
        ActiveGameObject = GameObject.FindGameObjectWithTag("Active");
    }
    // Update is called once per frame
    public void Update()
    {
        ClockTime += Time.deltaTime;
        if (ClockTime > 10f)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    this.transform.position = Vector3.right;
                    break;
                case 1:
                    this.transform.position = Vector3.left;
                    break;
                case 2:
                    this.transform.position = Vector3.up;
                    break;
                case 3:
                    this.transform.position = Vector3.down;
                    break;
            }
            ClockTime = 0f;
        }
        if (Vector3.Distance(ActiveGameObject.transform.position, this.transform.position) < 10)
        {
            PassMove();
        }
        if (Vector3.Distance(PassivePrefab.transform.position, this.transform.position) < 10)
        {
            PassMove2();
        }
    }

    public void PassMove()
    {
        this.transform.position += Vector3.right * 10;
    }
    public void PassMove2()
    {
        this.transform.position += Vector3.left * 10;
    }
}
