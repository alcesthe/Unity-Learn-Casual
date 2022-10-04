using System;
using UnityEngine;

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

        private Animator animator;
        private Rigidbody rigidbody;

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
                    break;
                case PlayerState.Run:
                    animator.SetBool("isRunning", true);
                    ControlPlayerMovement();
                    break;
            }
	    }

        private void ControlPlayerMovement()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed); // Move forward

            // Move X-Axis
            float xAsix = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(xAsix, 0, 0);
            transform.Translate(direction * Time.deltaTime * speed);

            // Clamp the X-Axis
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xAxisRange, xAxisRange),transform.position.y, transform.position.z);
        }
    }
}
