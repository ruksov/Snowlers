using UnityEngine;
using System;

[Serializable]
public class Vector3Reference
{
    public bool UseConstant = true;
    public Vector3 ConstantValue;
    public Vector3Variable Variable;

    public Vector3Reference()
    { }

    public Vector3Reference(Vector3 _value)
    {
        UseConstant = true;
        ConstantValue = _value;
    }

    public Vector3 Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator Vector3(Vector3Reference _reference)
    {
        return _reference.Value;
    }
}

