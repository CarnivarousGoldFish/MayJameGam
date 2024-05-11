using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMarker : MonoBehaviour
{
    public MapTile CurrentTile;

    public void SetMarkerPosition(Vector3 target)
    {
        transform.position = target;
    }
}
