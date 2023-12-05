using System;
using UnityEngine;

namespace KikiNgao.SimpleBikeControl
{
    public class InputManagerA : MonoBehaviour
    {
        public float horizontal, vertical;
        public FloatingJoystick floatingJoystick;

        public KeyCode speedUpKey = KeyCode.LeftShift;

        [HideInInspector]
        public bool enterExitVehicle;
        [HideInInspector]
        public bool speedUp;
        [SerializeField] private InputButton inputButton;

        private void Update()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }
            else
            {
                horizontal = floatingJoystick.Horizontal;
                vertical = floatingJoystick.Vertical;
            }


            enterExitVehicle = inputButton.IsClicked;


            if (Input.GetKeyDown(speedUpKey)) speedUp = true;
            if (Input.GetKeyUp(speedUpKey)) speedUp = false;
        }


    }
}
