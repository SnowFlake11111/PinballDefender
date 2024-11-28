using UnityEngine;
using System;
using Sirenix.OdinInspector;
//  ----------------------------------------------
//  Author:     CuongCT <caothecuong91@gmail.com> 
//  Copyright (c) 2016 OneSoft JSC
// ----------------------------------------------
public abstract class BaseScene : SerializedMonoBehaviour
{
    public abstract void OnEscapeWhenStackBoxEmpty();
}

