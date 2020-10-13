using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    void Update()
    {
        changeScale();
    }

    private void changeScale()
    {
        transform.Find("Bar").localScale = new Vector3(transform.GetComponentInParent<Enemy>().GetHealthPercent(),1);
    }

}
