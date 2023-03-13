using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnitTest
{
    public class CharacterContorller : MonoBehaviour
    {
        private float moveSpeed = 5f;
        private IInputSystem inputSystem;
        private Rigidbody rb;

        [Header ("US")]
        [SerializeField]
        private int a;
        
        [Header ("MS")]
        [SerializeField]
        private int a1;
        // Start is called before the first frame update
        void Start()
        {
            init();
        }

        // Update is called once per frame
        public void Update()
        {
            HandleMovement();
        }

        public void init()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
            Assert.IsNotNull(rb, "rb is null");
            inputSystem = new InputSystem();
        }   

        void HandleMovement()
        {
            rb.velocity = new Vector3(x: inputSystem.GetHorizontalValue() * moveSpeed, y: rb.velocity.y, z: inputSystem.GetVerticalValue() * moveSpeed);
            //rb.position = new Vector3(x: inputSystem.GetHorizontalValue() * moveSpeed, y: rb.velocity.y, z: rb.velocity.z);            
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public void SetInputSystem(IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }


    }
}
