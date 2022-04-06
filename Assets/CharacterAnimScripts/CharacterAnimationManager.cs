using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    #region Face State Parameters
    public MeshRenderer _renderer;

    public Vector2 tiling;

    public List<CharacterExpression> faceStates = new List<CharacterExpression>();
    public CharacterExpression.CharExpression defaultFaceState;

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
        }
    }
    private CharacterExpression _currentFaceState;
    #endregion

    #region Awake/Start/Update/Enable/Disable
    private void Awake()
    {
        #region Face State
        if(_renderer != null)
        {
            _faceMaterial = _renderer.materials[materialIndex];
            _faceMaterial.mainTextureScale = tiling;
            SetExpression(defaultFaceState);
        };
        #endregion
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    #endregion

    #region Face State Methods
    public void SetExpression(CharacterExpression.CharExpression faceState)
    {
        currentFaceState = faceState;
        _faceMaterial.mainTextureOffset = _currentFaceState.offset;
    }
    #endregion

    #region Animation

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
