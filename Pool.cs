namespace Noodle
{
  using UnityEngine;

  public class Pool : MonoBehaviour
  {
    public GameObject Prototype;
    public Transform PoolTransform;

    public PoolObject RetrieveObject(Transform newParent)
    {
      if (PoolTransform.childCount == 0)
      {
        GameObject instance = Instantiate(Prototype, PoolTransform) as GameObject;
      }

      Transform poolObjectTransform = PoolTransform.GetChild(0);

      PoolObject poolObject = poolObjectTransform.GetComponent<PoolObject>()
        ?? poolObjectTransform.gameObject.AddComponent<PoolObject>() as PoolObject;

      poolObject.OnExpire += OnObjectExpired;

      poolObjectTransform.SetParent(newParent);

      poolObjectTransform.localPosition = Vector3.zero;

      poolObjectTransform.localRotation = Quaternion.identity;

      poolObjectTransform.localScale = Vector3.one;

      poolObjectTransform.gameObject.SetActive(true);

      return poolObject;
    }

    public void ReturnObject(PoolObject poolObject)
    {
      poolObject.OnExpire -= OnObjectExpired;

      poolObject.transform.SetParent(PoolTransform);

      poolObject.gameObject.SetActive(false);
    }

    private void OnObjectExpired(PoolObject poolObject)
    {
      ReturnObject(poolObject);
    }
  }
}
