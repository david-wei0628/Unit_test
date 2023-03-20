using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitTest;

public class RayMove : MonoBehaviour
{
    public Transform PlayTrans;
    public Transform CameTrans;
    public Camera CamearTrans;
    public GameObject SeBox;
    private Vector3 offset;
    private IInputSystem inputSystem;
    float distance = 0;
    float MoveSpeed;
    Ray ray;
    RaycastHit hit;
    Vector3 maps;
    //bool movelimit = true;
    //public GameObject MouseVFX;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;//垂直同步
        Application.targetFrameRate = 100;//FPS禎數

        //init_Unit();
        PlayMode_Star();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ScrollView();
        }
        if (Input.GetMouseButtonDown(0))
        {
            SelectRay();
        }

        if (Input.GetMouseButton(1))
        {
            CameRat();
            //Debug.Log(Input.GetAxis("Mouse Y"));
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log(SeBox.transform.localEulerAngles.y);
            //Debug.Log(CamearTrans.transform.localRotation.y);
            //Debug.Log(transform.localEulerAngles.y);
            //maps = rayT.SelectRay(CamearTrans);
            //Debug.Log(VFX.transform.position);
            //Destroy(MouseVFX);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                KeyBoardMoveCamera(Input.GetAxis("Vertical"), "V");
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                this.transform.Translate(0, 0, -Input.GetAxis("Vertical") * MoveSpeed);
            }
            else
            {
                this.transform.Translate(0, 0, Input.GetAxis("Vertical") * MoveSpeed);
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                KeyBoardMoveCamera(Input.GetAxis("Horizontal"), "H");
            }
            //this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * MoveSpeed);
            if (Input.GetAxis("Horizontal") < 0)
            {
                this.transform.Translate(0, 0, -Input.GetAxis("Horizontal") * MoveSpeed);
            }
            else
            {
                this.transform.Translate(0, 0, Input.GetAxis("Horizontal") * MoveSpeed);
            }
        }

        if (Input.GetAxis("Jump") == 1 && transform.localPosition.y <= 0.06f)
        {
            PlayJump();
            //Debug.Log(Input.GetAxis("Jump"));
        }

    }

    public void init_Unit()
    {
        GameObject Unit_camObj = new GameObject();
        CameTrans = Unit_camObj.GetComponent<Transform>();
        PlayTrans = GetComponent<Transform>();  
        PlayTrans.position = new Vector3(0, 0.5f, 0);
        Debug.Log(CameTrans.position);
        Debug.Log(PlayTrans.position);
        PlayMode_Star();
        //inputSystem = new InputSystem();
    }

    void PlayMode_Star()
    {
        CameTrans.localPosition = new Vector3(PlayTrans.position.x, PlayTrans.position.y + 4, PlayTrans.position.z - 7);

        transform.localEulerAngles = new Vector3(0, 0, 0);
        offset = CameTrans.position - PlayTrans.position;
        MoveSpeed = Time.deltaTime * 20;
        CameTrans.position = offset + PlayTrans.position;
        CamerTrans();
        inputSystem = new InputSystem();
    }

    public void SetInputSystem(IInputSystem inputSystem)
    {
        this.inputSystem = inputSystem;
    }

    public void PlayUnitMove()
    {
        transform.Translate(inputSystem.GetHorizontalValue() * MoveSpeed, 0, inputSystem.GetVerticalValue() * MoveSpeed);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.MoveSpeed = moveSpeed;
    }

    public void ScrollView()
    {
        offset = CameTrans.position - transform.position;
        distance = offset.magnitude;
        //distance -= Input.GetAxis("Mouse ScrollWheel") * 10;
        distance -= inputSystem.GetScrollWheelValue() * 10; 
        if (distance > 26)
        {   
            distance = 26;
        }
        if (distance < 5)
        {    
            distance = 5;
        }
        offset = offset.normalized * distance;
        CameTrans.position = offset + transform.position;
    }

    public void SelectRay()
    {
        ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 3500);
        maps = hit.point;
        maps.y = this.transform.position.y;
        Debug.DrawLine(CamearTrans.transform.position, hit.transform.position, Color.blue, 0.5f, true);

        //Instantiate(MouseVFX, maps, new Quaternion(0, 0, 0,0));
        if (Vector3.Distance(this.transform.position, maps) < 100f)
        {
            RayMoveCamera();
            PlayMove();
        }
    }

    public void PlayMove()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, maps, 0.1f);
        if (Vector3.Distance(this.transform.position, maps) > 0.1f )
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            { 
                Invoke("PlayMove", 0.05f);
            }
            else
            {
                return;
            }
        }
    }

    public void PlayJump()
    {
        transform.localPosition += new Vector3(0, 100, 0) * Time.deltaTime;
        if (transform.localPosition.y < 4)
        {
            Invoke("PlayJump", 0.01f);
        }
        else
        {
            return ;
        }
        //if (transform.position.y < 3)
        //{
        //    Invoke("PlayJump", 0.05f);
        //}
    }

    public void CameRat()
    {
        //CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        //CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);

        if (Input.GetAxis("Mouse X") != 0)
        {
            CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        }
        else
        {
            if(Input.GetAxis("Mouse Y") < 0)
            {
                if(CamearTrans.transform.localEulerAngles.x > 40 && CamearTrans.transform.localEulerAngles.x < 180)
                {
                    CamearTrans.transform.localEulerAngles = new Vector3(30, CamearTrans.transform.localEulerAngles.y, 0);
                }
                else
                {
                    CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                }
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                if (CamearTrans.transform.localEulerAngles.x > 340)
                {
                    CamearTrans.transform.localEulerAngles = new Vector3(350, CamearTrans.transform.localEulerAngles.y, 0);
                }
                else
                {
                    CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                }
                //CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 5);
                //Debug.Log(CamearTrans.transform.localEulerAngles.x);
            }
        }
        
        CamerTrans();
        //上下範圍 X軸 40~-10
        //Y,Z做導向,Z平行於場景  X 做指向
    }

    void CamerTrans()
    {
        CameTrans.LookAt(this.transform.position);
        //Debug.Log(CamearTrans.transform.localEulerAngles.x);
        if(CameTrans.localEulerAngles.x > 300)
        {
            CameTrans.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x + 20, CameTrans.localEulerAngles.y, 0);
        }
        else
        {
            CameTrans.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x - 20, CameTrans.localEulerAngles.y, 0);        
        }
        //Debug.Log(CamearTrans.transform.localEulerAngles.x);
    }

    void RayMoveCamera()
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        this.transform.LookAt(maps);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }

    void KeyBoardMoveCamera(float move,string direc)
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        float PlayEulerY;
        PlayEulerY = transform.localEulerAngles.y + CamearTrans.transform.localEulerAngles.y;
        switch (direc)
        {
            case "V":
                if (move < 0)
                {
                    this.transform.localEulerAngles = new Vector3(0, PlayEulerY + 180, 0);
                }
                else
                {
                    this.transform.localEulerAngles = new Vector3(0, PlayEulerY, 0);
                }
                //this.transform.Translate(0, 0, move * MoveSpeed);
                break;
            case "H":
                if (move < 0)
                {
                    this.transform.localEulerAngles = new Vector3(0, PlayEulerY + 270, 0);
                }
                else
                {
                    this.transform.localEulerAngles = new Vector3(0, PlayEulerY + 90, 0);
                }
                //this.transform.Translate(move * MoveSpeed, 0, 0);
                break;
        }

        //this.transform.Translate(0, 0, move * MoveSpeed);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }
}
