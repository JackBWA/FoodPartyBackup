using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    #region Face State Parameters
    public SkinnedMeshRenderer _renderer;

    public Vector2 tiling = new Vector2(.5f, .25f);

    public List<CharacterExpression> faceStates = new List<CharacterExpression>();
    public CharacterExpression.CharExpression defaultFaceState = CharacterExpression.CharExpression.IDLE;

    public int materialIndex;
    private Material _faceMaterial;

    public CharacterExpression.CharExpression currentFaceState
    {
        get
        {
            return _currentFaceState.charExpression;
        }
        set
        {
            CharacterExpression cE = faceStates.First(fS => fS.charExpression.Equals(value));
            _currentFaceState = cE;
            SetExpression(cE.charExpression);
        }
    }
    private CharacterExpression _currentFaceState;
    #endregion

    public float speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            UpdateFaceBasedOnSpeed(speed);
            ator.SetFloat("Speed", speed);
        }
    }
    private float _speed;

    public Animator ator;

    #region Awake/Start/Update/Enable/Disable
    private void Awake()
    {
        if (ator == null) ator = GetComponent<Animator>();
        #region Face State
        if(_renderer != null)
        {
            _faceMaterial = _renderer.materials[materialIndex];
            _faceMaterial.mainTextureScale = tiling;
            currentFaceState = defaultFaceState;
        };
        #endregion
    }
    #endregion

    #region Face State Methods
    private void UpdateFaceBasedOnSpeed(float speed)
    {
        if (speed >= .66f)
        {
            currentFaceState = CharacterExpression.CharExpression.TEASED;
        } else
        {
            currentFaceState = defaultFaceState;
        }
    }

    private void SetExpression(CharacterExpression.CharExpression faceState)
    {
        //currentFaceState = faceState; // Stack overflow.
        _faceMaterial.mainTextureOffset = _currentFaceState.offset;
    }
    #endregion
}

[Serializable]
public class CharacterExpression
{
    public enum CharExpression
    {
        IDLE,
        ANGRY,
        TEASED,
        HAPPY,
        SAD,
        COOL,
        DEAD
    }

    public CharExpression charExpression;
    public Vector2 offset;
}