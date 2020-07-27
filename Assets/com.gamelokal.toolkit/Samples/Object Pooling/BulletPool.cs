using GameLokal.Utility.ObjectPooler;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    protected override void OnBeforeRent(Bullet instance)
    {
        base.OnBeforeRent(instance);
        instance.transform.localPosition = Vector3.zero;
    }
}
