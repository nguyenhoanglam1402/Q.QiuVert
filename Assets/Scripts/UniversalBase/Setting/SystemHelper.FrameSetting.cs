using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QiQiuVert
{
  public static partial class SystemHelper
  {
    static int _fpsLimit = 60;

    public static void SetFrameLimit(int fpsLimit = 60)
    {
      if (_fpsLimit != fpsLimit)
        _fpsLimit = fpsLimit;
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = _fpsLimit;
    }
  }
}