using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtra
{
    public static string Bold(this string str)
    {
        return $"<b>{str}</b>";
    }

    public static string Color(this string str, string color)
    {
        return $"<color={color}>{str}</color>";
    }

    public static string Italic(this string str)
    {
        return $"<i>{str}</i>";
    }
}