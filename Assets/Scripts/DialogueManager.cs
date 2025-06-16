using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum CharacterPosition { Left, Right }

[Serializable]
public struct SceneConversa
{
    public Sprite backgroundImage;
    public string characterName;
    public Sprite characterSprite;
    public CharacterPosition position;
    public Vector2 customOffset;
    public Sprite speechBoxSprite;
    public string dialogueText;
}

public class DialogueManager : MonoBehaviour
{
    public Image background;
    public Text characterNameText;
    public Image character;
    public RectTransform characterTransform;
    public Image dialogueBox;
    public Text dialogueText;
    public Button nextButton;

    public List<SceneConversa> sceneMoments;
    private int currentMoment = 0;

    private void Start()
    {
        nextButton.onClick.AddListener(AdvanceScene);
        LoadMoment();
    }

    private void LoadMoment()
    {
        if (currentMoment >= sceneMoments.Count) return;

        SceneConversa moment = sceneMoments[currentMoment];

        // Cambio directo sin transición
        background.sprite = moment.backgroundImage;
        character.sprite = moment.characterSprite;
        characterTransform.anchoredPosition = (moment.position == CharacterPosition.Left) ?
                                             new Vector2(-400, 0) : new Vector2(400, 0);
        character.transform.localScale = (moment.position == CharacterPosition.Right) ?
                                       new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        characterNameText.text = moment.characterName;
        dialogueText.text = moment.dialogueText;
        dialogueBox.sprite = moment.speechBoxSprite;
    }

    public void AdvanceScene()
    {
        currentMoment++;
        if (currentMoment < sceneMoments.Count)
            LoadMoment();
        else
            nextButton.onClick.AddListener(() => BartraSceneUtils.GoToScene("BattleScene"));
    }
}
