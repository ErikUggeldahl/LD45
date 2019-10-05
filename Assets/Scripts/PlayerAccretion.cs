using System.Collections.Generic;
using UnityEngine;

public class PlayerAccretion : MonoBehaviour
{
    HashSet<Vector3Int> brushChildrenPositions = new HashSet<Vector3Int>();

    public void AddBrush(Transform brush)
    {
        var directionToBrush = (brush.position - transform.position).normalized;
        if (directionToBrush == Vector3.zero)
        {
            directionToBrush = (Vector3)Random.insideUnitCircle;
        }

        var localBrushPosition = transform.InverseTransformPoint(brush.position);
        var intPosition = Vector3Int.RoundToInt(localBrushPosition);
        int attempts = 0;
        while (intPosition == Vector3Int.zero || brushChildrenPositions.Contains(intPosition))
        {
            localBrushPosition += directionToBrush;
            intPosition = Vector3Int.RoundToInt(localBrushPosition);
            attempts++;
            if (attempts > 10)
            {
                return;
            }
        }

        brush.parent = transform;
        brush.localPosition = intPosition;
        brushChildrenPositions.Add(intPosition);
    }
}
