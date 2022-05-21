using System;
using UnityEngine;

namespace QiQiuVert
{
  public class CharacterController : MonoBehaviour, ICharacterController
  {
    public UnityEngine.CharacterController characterController;
    public Transform mainCameraTransform;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity = 0f;

    float _axisX = 0;
    float _axisZ = 0;
    float _rotationY = 0;
    float _angleFollowCamera = 0f;


    void Update()
    {
      Vector3 direction = UpdateAxis();
      _rotationY = GetRotationAngle().Item2;
      _angleFollowCamera = GetRotationAngle().Item1;
      Move(direction);
    }

    public Vector3 UpdateAxis()
    {
      _axisX = Input.GetAxisRaw("Horizontal");
      _axisZ = Input.GetAxisRaw("Vertical");
      return new Vector3(_axisX, 0, _axisZ).normalized;
    }


    public Tuple<float, float> GetRotationAngle()
    {
      float targetAngle = Mathf.Atan2(_axisX, _axisZ) * Mathf.Rad2Deg + mainCameraTransform.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
      return Tuple.Create(targetAngle, angle);

    }


    public void Move(Vector3 direction)
    {

      if (direction.magnitude > 0f)
      {
        Vector3 moveDirection = Quaternion.Euler(0f, _angleFollowCamera, 0f) * Vector3.forward;
        characterController.Move(moveDirection * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
      }
    }
  }

}

