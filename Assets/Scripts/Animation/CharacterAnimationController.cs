using QiQiuVert;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour, IAnimationController
{

  [SerializeField] Animator _animatorController;
  float _axisX = 0f;
  float _axisY = 0f;
  bool _isGround = false;

  void Start()
  {
    _animatorController.SetBool("Grounded", true);
  }

  void FixedUpdate()
  {
    _axisX = Input.GetAxisRaw("Horizontal");
    _axisY = Input.GetAxisRaw("Vertical");
    UpdateMovementAnimationAxis(_axisX, _axisY);
  }

  public void UpdateMovementAnimationAxis(float axisX, float axisY)
  {
    var direction = new Vector2(_axisX, _axisY).normalized.magnitude;
    _animatorController.SetFloat("MoveSpeed", direction);
  }
}
