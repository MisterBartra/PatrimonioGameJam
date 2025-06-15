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
    public CanvasGroup backgroundCurrent, backgroundNext;
    public Text characterNameText;
    public CanvasGroup characterCurrent, characterNext;
    public RectTransform characterTransform;
    public CanvasGroup dialogueBoxCurrent, dialogueBoxNext;
    public CanvasGroup dialogueGroupCurrent, dialogueGroupNext;
    public Text dialogueCurrent, dialogueNext;
    public Button nextButton;

    public List<SceneConversa> sceneMoments;
    private int currentMoment = 0;
    private bool isTextAnimating = false;
    private Coroutine textCoroutine;

    private ITransitionEffect transitionEffect;

    private void Start()
    {
        transitionEffect = GetComponent<FadeCanvasGroup>();
        nextButton.onClick.AddListener(AdvanceScene);
        LoadMoment();
    }

    private void LoadMoment()
    {
        if (currentMoment >= sceneMoments.Count) return;

        SceneConversa moment = sceneMoments[currentMoment];

        // Evitar actualización innecesaria
        if (backgroundNext.GetComponent<Image>().sprite != moment.backgroundImage)
            backgroundNext.GetComponent<Image>().sprite = moment.backgroundImage;

        if (characterNext.GetComponent<Image>().sprite != moment.characterSprite)
            characterNext.GetComponent<Image>().sprite = moment.characterSprite;

        characterTransform.anchoredPosition = new Vector2(
            (moment.position == CharacterPosition.Left ? -moment.customOffset.x : moment.customOffset.x),
            moment.customOffset.y
        );

        // Flip automático
        characterTransform.localScale = (moment.position == CharacterPosition.Right) ?
                                       new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        characterNameText.text = moment.characterName;
        dialogueNext.text = moment.dialogueText;
        dialogueBoxNext.GetComponent<Image>().sprite = moment.speechBoxSprite;

        if (textCoroutine != null)
            StopCoroutine(textCoroutine);

        textCoroutine = StartCoroutine(AnimateText(moment.dialogueText));

        transitionEffect.ApplyTransition(backgroundCurrent, backgroundNext);
        transitionEffect.ApplyTransition(characterCurrent, characterNext);
        transitionEffect.ApplyTransition(dialogueBoxCurrent, dialogueBoxNext);
        transitionEffect.ApplyTransition(dialogueGroupCurrent, dialogueGroupNext);
    }

    private IEnumerator AnimateText(string fullText)
    {
        isTextAnimating = true;
        dialogueCurrent.text = "";

        foreach (char letter in fullText)
        {
            dialogueCurrent.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTextAnimating = false;
    }

    public void AdvanceScene()
    {
        if (isTextAnimating)
        {
            StopCoroutine(textCoroutine);
            dialogueCurrent.text = sceneMoments[currentMoment].dialogueText;
            isTextAnimating = false;
        }
        else
        {
            currentMoment++;
            if (currentMoment < sceneMoments.Count)
                LoadMoment();
            else
                nextButton.onClick.AddListener(() => BartraSceneUtils.GoToScene("boss"));
        }
    }
}
