using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    private DesignChanger _designChanger;

    private void Awake()
    {
        _designChanger = GetComponent<DesignChanger>();

        if (_designChanger == null)
        {
            Debug.LogError("DesignChanger is null");
        }
    }

    protected override void OnTrackingFound()
    {
        _designChanger.ChangeDesign();
        base.OnTrackingFound();
    }
}
