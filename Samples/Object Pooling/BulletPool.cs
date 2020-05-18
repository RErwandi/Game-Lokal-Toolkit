using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet>
{
    protected override void OnBeforeRent(Bullet instance)
    {
        base.OnBeforeRent(instance);
        instance.transform.localPosition = Vector3.zero;
    }
}
