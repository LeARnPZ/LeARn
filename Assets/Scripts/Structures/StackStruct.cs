using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackStruct : Structures
{
    protected override void SetDirection()
    {
        direction = Vector3.up;
    }

    public override void PopItem()
    {
        popIndex = items.Count - 1;
        base.PopItem();
    }
}
