using Game.Core;
using UnityEngine;

namespace Game.Character
{
    public class PlayerController : ItemController
    {
        public Player PlayerItem;

        private float _speed;
        private bool _isGround;
        private Vector3 _direction;

        private float _rotationY = 0;

        private float _minimumY = -60f;
        private float _maximumY = 60f;

        public override void Tick()
        {
            if (PlayerItem == null) return;


            if (Input.GetKey("left shift"))
            {
                PlayerItem.CharacterAnimator.SetFloat("Y", 2);

                _speed = _speedRun;
            }
            else
            {
                _speed = _speedWalk;
            }

            _isGround = Physics.CheckSphere(PlayerItem.GroundCheck.position, Constants.GroundDistance, _groundMask);

            if (PlayerItem.PlayerController.isGrounded)
            {
                PlayerItem.CharacterAnimator.SetFloat("X", Input.GetAxis("Horizontal"));
                PlayerItem.CharacterAnimator.SetFloat("Y", Input.GetAxis("Vertical"));
            }

            if (_isGround && _direction.y < 0)
            {
                _direction.y = -2f;
            }

            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");


            if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            {
                _direction.y = Mathf.Sqrt(_jumpHeight * Constants.Gravity);
                PlayerItem.CharacterAnimator.SetTrigger("Jump");

            }

            Vector3 move = PlayerItem.ViewTrans.right * hor + PlayerItem.ViewTrans.forward * ver;

            PlayerItem.PlayerController.Move(move * _speed * Time.deltaTime);

            _direction.y -= Constants.Gravity * Time.deltaTime;

            PlayerItem.PlayerController.Move(_direction * Time.deltaTime);


            PlayerItem.ViewTrans.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity, 0);

            _rotationY += Input.GetAxis("Mouse Y") * _sensitivity;

            _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

            PlayerItem.CamTrans.localEulerAngles = new Vector3(-_rotationY, PlayerItem.CamTrans.localEulerAngles.y, 0);
        }
    }
}
