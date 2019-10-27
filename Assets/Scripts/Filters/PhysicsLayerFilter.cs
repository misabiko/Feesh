﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter {
    public LayerMask mask;
    
    public override List<Transform> filter(FlockAgent agent, List<Transform> original) {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in original)
            if (mask == (mask | (1 << item.gameObject.layer))) //if (mask == item's layer)
                filtered.Add(item);

        return filtered;
    }
}
