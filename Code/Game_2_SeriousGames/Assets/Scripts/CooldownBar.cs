using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : MonoBehaviour
{
    void Update()
    {
        changeScale();
    }

    private void changeScale()
    {
        transform.Find("Bar").localScale = new Vector3(transform.GetComponentInParent<CrossHair>().GetCooldownScalePercent(), 1);
    }
}
