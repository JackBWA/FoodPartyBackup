using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputsDisplayer : MonoBehaviour
{
    public static InputsDisplayer singleton;

    public InputItemUI prefab;
    public Transform parentContainer;

    private List<InputItemUI> inputsList = new List<InputItemUI>();

    public bool visible
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
            BoardEntity bE = GameBoardManager.singleton.boardPlayers.First(e => e.hasTurn);
            if (value)
            {
                bE.LockTPC();
            }
            else
            {
                bE.UnlockTPC();
            }
        }
    }

    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
            return;
        }

        singleton = this;

        visible = false;
        AddInput(Resources.Load<Sprite>("Icons/PIcon"), "Pausar");
        AddInput(Resources.Load<Sprite>("Icons/SpacebarIcon"), "Tirar dado");
        AddInput(Resources.Load<Sprite>("Icons/IIcon"), "Inventario");
        AddInput(Resources.Load<Sprite>("Icons/MIcon"), "Cámara superior");
        AddInput(Resources.Load<Sprite>("Icons/XIcon"), "Usar objeto");
        AddInput(Resources.Load<Sprite>("Icons/QIcon"), "Cancelar objeto");
        AddInput(Resources.Load<Sprite>("Icons/LeftAltIcon"), "Bloquear cámara");
    }

    public void ToggleVisibility()
    {
        visible = !visible;
    }

    public void AddInput(Sprite image, string text)
    {
        if (prefab == null) prefab = Resources.Load<InputItemUI>("UI/InputItemUI");
        InputItemUI instance = Instantiate(prefab);
        instance.inputImage = image;
        instance.inputText = text;
        instance.gameObject.transform.SetParent(parentContainer);
        inputsList.Add(instance);
    }

    public void ClearInputs()
    {
        for(int i = 0; i < inputsList.Count; i++)
        {
            Destroy(inputsList[i].gameObject);
        }
        inputsList.Clear();
    }
}