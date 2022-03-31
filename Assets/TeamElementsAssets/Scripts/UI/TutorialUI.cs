using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public string title
    {
        get
        {
            return _title.text;
        }
        set
        {
            _title.text = value;
        }
    }
    public string description
    {
        get
        {
            return _description.text;
        }
        set
        {
            _description.text = value;
        }
    }

    public List<Sprite> images = new List<Sprite>();

    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Image imagesHolder;
    public float maxImageDuration = 5f;

    private Coroutine displayImagesCo;

    private void OnEnable()
    {
        title = MiniGame.singleton.minigameName;
        description = MiniGame.singleton.minigameDescription;
        DisplayImages();
    }

    public void DisplayImages()
    {
        displayImagesCo = StartCoroutine(DisplayCo());
    }

    public void StopDisplayingImages()
    {
        StopCoroutine(displayImagesCo);
    }

    private IEnumerator DisplayCo()
    {
        yield return null;
    }
}
