using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTile : MapTile
{
    public override void Initialize(MapManager manager)
    {
        base.Initialize(manager);

        _isHazard = true;
    }
}
