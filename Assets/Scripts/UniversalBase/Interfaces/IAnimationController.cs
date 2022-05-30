using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QiQiuVert
{
  public interface IAnimationController
  {
    /// <summary>
    /// Update movement animation state follow axies
    /// </summary>
    /// <param name="axisX">Horizontal axis</param>
    /// <param name="axisY">Vertical axis</param>
    public void UpdateMovementAnimationAxis(float axisX, float axisY);
  }
}
