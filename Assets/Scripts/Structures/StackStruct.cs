using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackStruct : Structures
{
    protected override void SetDirection()
    {
        direction = Vector3.up;
    }

    public override void AddItem()
    {
        iterator = items.Count;
        base.AddItem();
    }

    public override void PopItem()
    {
        iterator = items.Count - 1;
        base.PopItem();
        iterator--;
    }

    public override void PeekItem()
    {
        iterator = items.Count - 1;
        base.PeekItem();
    }
}
