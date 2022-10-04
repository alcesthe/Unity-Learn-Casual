using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityLearnCasual
{
    public enum PlayerState
    {
        Stop,
        Run
    }

    public class Player : MonoBehaviour
	{
        [SerializeField] public float speed = 10f;
        [SerializeField] public PlayerState playerState;
        [SerializeField] float xAxisRange = 8.8f;
        [SerializeField] public float swipeSpeed = 10f;

        private Animator animator;
        private Rigidbody rigidbody;

        private Vector3 lastMousePos;
        private Vector3 mousePos;
        private Vector3 newPosForTrans;
        public Camera mainCam;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            playerState = PlayerState.Stop; // Todo change later
        }

        void Update()
 	    {
            switch (playerState)
            {
                case PlayerState.Stop:
                    animator.SetBool("isRunning", false);
                    //playerControl.Disable();
                    break;
                case PlayerState.Run:
                    animator.SetBool("isRunning", true);
                    //playerControl.Enable();
                    ControlPlayerMovement();
                    break;
            }
	    }

        private void ControlPlayerMovement()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed); // Move forward

            if (Input.GetMouseButton(0))
            {
                mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                
                float xDiff = mousePos.x - lastMousePos.x;

                newPosForTrans.x = transform.position.x + xDiff * Time.deltaTime * swipeSpeed;
                newPosForTrans.y = transform.position.y;
                newPosForTrans.z = transform.position.z;

                transform.position = newPosForTrans + transform.forward * speed * Time.deltaTime;

                lastMousePos = mousePos;
            }

            // Move X-Axis
            /*float xAxis = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(xAxis, 0, 0);
            transform.Translate(direction * Time.deltaTime * speed);*/

            // Clamp the X-Axis
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xAxisRange, xAxisRange),transform.position.y, transform.position.z);
        }
    }
}
