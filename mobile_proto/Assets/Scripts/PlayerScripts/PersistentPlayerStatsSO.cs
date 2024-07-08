using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Persistent Player Stats SO")]
public class PersistentPlayerStatsSO : ScriptableObject {
    [SerializeField] float knockbackTime, knockbackForce;
    public float KnockbackTime => knockbackTime;
    public float KnockbackForce => knockbackForce;
}
