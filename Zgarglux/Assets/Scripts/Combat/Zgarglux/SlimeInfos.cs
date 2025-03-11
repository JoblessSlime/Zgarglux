using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInfos : MonoBehaviour
{
    public int healthPoint;
    public int MaxSplitNumber;
    public float hurtRecoveryTime;
    public bool RecoveringState = false;
    public int damages;
    public float absorbSplitRange;

    public GameObject activeSplit;

    public List<GameObject> splits;
}
