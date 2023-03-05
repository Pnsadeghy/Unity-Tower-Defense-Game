using System;
using UnityEngine;

public class ValueHelper {
    public static Quaternion LookAt(Vector3 source, Vector3 target) {
        Vector3 direction = target - source;
        return Quaternion.LookRotation(Vector3.forward, direction);
    }
}