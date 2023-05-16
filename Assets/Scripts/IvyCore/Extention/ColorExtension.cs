using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    public static Color GetNewColorWithDifferentAlpha(this Color col, float alpha)
    {
        return new Color(col.r, col.g, col.b, alpha);
    }
}