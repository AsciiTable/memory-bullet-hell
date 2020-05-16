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
}
