using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableColorChanger : MonoBehaviour, IColorChangeable
{
    private Selectable selectable;
    private ColorBlock defaultColorBlock;

    void Start()
    {
        selectable = GetComponent<Button>();
        defaultColorBlock = selectable.colors;
    }

    public void ResetColor()
    {
        selectable.colors = defaultColorBlock;
    }

    public void SetNormalColor(Color color)
    {
        var colors = selectable.colors;
        colors.normalColor = color;
        selectable.colors = colors;
    }
}
