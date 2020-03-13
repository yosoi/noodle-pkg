namespace Noodle
{
  using System;
  using UnityEngine;

  public class PoolObject : MonoBehaviour
  {
    public event Action<PoolObject> OnExpire;

    public void Expire() => OnExpire?.Invoke(this);
  }
}
