using UnityEngine.Assertions;
using System;
using UnityEngine;
using QiQiuVert;

public class CharacterController : MonoBehaviour, ICharacterController
{
  public UnityEngine.CharacterController characterController;
  public Transform mainCameraTransform;
  [Header("Check ground configuration")]
  public LayerMask groundMask;
  public float gravity = -9.8f;
  public Transform groundCheckTransform;
  public float checkGroundDistance = 0.2f;
  Vector3 _velocity;
  [Header("Movement configuration")]
  [SerializeField] float _jumpSpeed = 5f;
  [SerializeField] float _speed = 3f;
  [SerializeField] float _turnSmoothTime = 0.1f;
  float _turnSmoothVelocity = 0f;

  PlayerControl _playerControl;



  float _axisX = 0;
  float _axisZ = 0;
  float _rotationY = 0;
  float _angleFollowCamera = 0f;

  void Awake()
  {
    _playerControl = new PlayerControl();
    _playerControl.Gameplay.Move.performed += context => Debug.Log(context.ReadValueAsObject());
  }


  void Update()
  {
    Vector3 direction = UpdateAxis();
    _rotationY = GetRotationAngle().Item2;
    _angleFollowCamera = GetRotationAngle().Item1;
    if (IsGrounded() && _velocity.y < 0)
      _velocity.y = -2f;
    Move(direction);
    _velocity.y += gravity * Time.deltaTime;
    characterController.Move(_velocity * Time.deltaTime);
    if (Input.GetButtonDown("Jump") && IsGrounded())
    {
      Jump();
    }
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

  public void Jump()
  {
    Debug.Log("Jump!");
    _velocity.y = Mathf.Sqrt(_jumpSpeed * -0.2f * gravity);
  }

  public bool IsGrounded()
  {
    Assert.IsNotNull(groundCheckTransform, "Ground check Transform was null!");
    if (groundCheckTransform != null)
    {
      bool isGrounded = Physics.CheckSphere(groundCheckTransform.position, checkGroundDistance, groundMask);
      Debug.Log(isGrounded);
      return isGrounded;
    }
    return false;
  }

  public void CalculateGravity()
  {
    throw new NotImplementedException();
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(groundCheckTransform.position, checkGroundDistance);

  }
}


