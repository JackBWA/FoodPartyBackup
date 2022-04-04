using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpressions : MonoBehaviour
{
    public Etst inputActions;

    public Sprite[] expressions;

    SpriteRenderer sR;
    int index;

    private void Awake()
    {
        TryGetComponent(out sR);
        index = 2;
        Change();
        inputActions = new Etst();
        inputActions.test.a.performed += _ => Change();
    }

    public void Change()
    {
        index++;
        if (index >= expressions.Length) index = 0;
        sR.sprite = expressions[index];
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
