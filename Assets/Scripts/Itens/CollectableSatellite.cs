using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSatellite : CollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddSatellites();
    }
}
