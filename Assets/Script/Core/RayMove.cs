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
    bool CameRotRound;
    //bool movelimit = true;
    //public GameObject MouseVFX;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;//垂直同步
        Application.targetFrameRate = 100;//FPS禎數

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
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log(SeBox.transform.localEulerAngles.y);
            Debug.Log(CamearTrans.transform.localEulerAngles);
            //Debug.Log(transform.localEulerAngles.y);
            //maps = rayT.SelectRay(CamearTrans);
            //Debug.Log(VFX.transform.position);
            //Destroy(MouseVFX);
        }

        if (inputSystem.GetVerticalValue() != 0 || inputSystem.GetHorizontalValue() != 0)
        {
            PlayKeyBoardMove();
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
        Unit_camObj.AddComponent<Camera>();
        CamearTrans = Unit_camObj.GetComponent<Camera>();
        PlayTrans = GetComponent<Transform>();  
        PlayTrans.position = new Vector3(0, 0.5f, 0);
        PlayMode_Star();
        //inputSystem = new InputSystem();
    }

    void PlayMode_Star()
    {
        CameTrans.localPosition = new Vector3(PlayTrans.position.x, PlayTrans.position.y + 4, PlayTrans.position.z - 7);

        transform.localEulerAngles = new Vector3(0, 0, 0);
        offset = CameTrans.position - PlayTrans.position;
        MoveSpeed = Time.deltaTime * 10;
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
        //ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        ray = CamearTrans.GetComponent<Camera>().ScreenPointToRay(inputSystem.GetMousePosition());

        Physics.Raycast(ray, out hit, 3500);
        maps = hit.point;
        maps.y = transform.position.y;
        //Debug.DrawLine(CamearTrans.transform.position, hit.transform.position, Color.blue, 0.5f, true);
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
            if (inputSystem.GetHorizontalValue() == 0 && inputSystem.GetVerticalValue() == 0)
            { 
                Invoke("PlayMove", 0.05f);
            }
            else
            {
                return;
            }
        }
    }

    void PlayJump()
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
        if (inputSystem.GetMouseXValue() != 0)
        {
            CamearTrans.transform.RotateAround(this.transform.position, Vector3.up, inputSystem.GetMouseXValue() * 5);
            CamerTrans();
        }
        
        if(inputSystem.GetMouseYValue() != 0)
        {
            CameRotRound = CamearRround();
            if (CamearTrans.transform.position.z > 0 && CameRotRound)
            {
                CamearTrans.transform.RotateAround(this.transform.position, Vector3.right, inputSystem.GetMouseYValue() * 5);
                CamerTrans();
            }
            else if(CamearTrans.transform.position.z < 0 && CameRotRound)
            {
                CamearTrans.transform.RotateAround(this.transform.position, Vector3.left, inputSystem.GetMouseYValue() * 5);
                CamerTrans();
            }
        }
        //CamerTrans();
    }

    bool CamearRround()
    {
        if (CameTrans.localEulerAngles.x > 340 || CameTrans.localEulerAngles.x < 40)
        {
            return true;
        }
        else
        {
            if (CameTrans.localEulerAngles.x == 340 && inputSystem.GetMouseYValue() < 0)
            {
                return true;
            }
            else if (CameTrans.localEulerAngles.x == 40 && inputSystem.GetMouseYValue() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    void CamerTrans()
    {
        CameTrans.LookAt(this.transform.position);
        CameTrans.localEulerAngles = new Vector3(CameTrans.localEulerAngles.x - 20, CameTrans.localEulerAngles.y, 0);
        if (Input.GetMouseButton(1))
        {
            if (CameTrans.localEulerAngles.x < 340 && CameTrans.localEulerAngles.x > 180)
            {
                CameTrans.LookAt(this.transform.position);
                CameTrans.localEulerAngles = new Vector3(340, CameTrans.localEulerAngles.y, 0);
            }
            else if (CameTrans.localEulerAngles.x > 40 && CameTrans.localEulerAngles.x < 180)
            {
                CameTrans.LookAt(this.transform.position);
                CameTrans.localEulerAngles = new Vector3(40, CameTrans.localEulerAngles.y, 0);
            }
        }
    }

    void RayMoveCamera()
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        this.transform.LookAt(maps);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }

    void KeyBoardMoveCamera()
    {
        Vector3 InitCoor = CamearTrans.transform.position;
        float PlayEulerY;
        PlayEulerY = transform.localEulerAngles.y + CamearTrans.transform.localEulerAngles.y;
        //V<0 180 H<0 270 H>0 90

        PlayEulerY += KeyInputRot("V") + KeyInputRot("H");

        this.transform.localEulerAngles = new Vector3(0, PlayEulerY, 0);
        CamearTrans.transform.position = InitCoor;
        CamerTrans();
    }

    int KeyInputRot(string InputKey)
    {
        int Rot = 0;
        if (InputKey == "V")
        {
            if (inputSystem.GetVerticalValue() < 0)
            {
                Rot = 180;
            }
        }
        if (InputKey == "H")
        {
            if (inputSystem.GetHorizontalValue() > 0)
            {
                Rot = 90 * (int)Mathf.Sign(inputSystem.GetVerticalValue());
            }
            if (inputSystem.GetHorizontalValue() < 0)
            {
                Rot = -90 * (int)Mathf.Sign(inputSystem.GetVerticalValue());
            }

            if (inputSystem.GetVerticalValue() != 0)
            {
                Rot /= 2;
            }
        }
        return Rot;
    }

    public void PlayKeyBoardMove()
    {
        KeyBoardMoveCamera();

        if (inputSystem.GetVerticalValue() == 0 && inputSystem.GetHorizontalValue() != 0)
            this.transform.Translate(0, 0, Mathf.Abs(inputSystem.GetHorizontalValue()) * MoveSpeed);
        else if (inputSystem.GetVerticalValue() != 0 && inputSystem.GetHorizontalValue() == 0)
            this.transform.Translate(0, 0, Mathf.Abs(inputSystem.GetVerticalValue()) * MoveSpeed);
        else
            this.transform.Translate(0, 0, Mathf.Abs(inputSystem.GetVerticalValue()* inputSystem.GetHorizontalValue()) * MoveSpeed);
    }
}
