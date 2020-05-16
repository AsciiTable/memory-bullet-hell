using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHandler : MonoBehaviour
{
    public delegate void onUpdate();
    public static event onUpdate UpdateOccurred;
    private void Update()
    {
        if (UpdateOccurred != null)
            UpdateOccurred();
    }

    public delegate void onFixedUpdate();
    public static event onFixedUpdate FixedUpdateOccurred;
    private void FixedUpdate()
    {
        if (FixedUpdateOccurred != null)
            FixedUpdateOccurred();
    }
}
