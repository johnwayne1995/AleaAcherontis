using System;
using UnityEngine;

namespace CardGame
{
    public class characterController : MonoBehaviour
    {
        public int speed = 1;
        public GameObject Enemy;
        public GameObject Dialogue;
        public GameObject ReadyToBattle;

        protected void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.position += Vector3.up*speed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position += Vector3.down*speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += Vector3.left*speed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right*speed;
            }
            
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "DoorLeft" || other.name == "DoorRight")
            {
                Dialogue.SetActive(true);
                Enemy.SetActive(true);
            }
            if (other.name == "Enemy")
            {
                ReadyToBattle.SetActive(true);
                Enemy.SetActive(true);
            }
        }
    }
}