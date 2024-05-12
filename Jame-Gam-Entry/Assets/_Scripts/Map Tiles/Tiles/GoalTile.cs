using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : MapTile
{
    public override void Initialize(MapManager manager)
    {
        base.Initialize(manager);

        _isHazard = false;
        _isGoal = true;
    }
}
