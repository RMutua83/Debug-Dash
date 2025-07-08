using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    [TextArea(3, 10)]
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed = 0.05f;

    [Header("Timing")]
    public float SentenceDisplayDuration = 2f; // How long to show after typing

    private void Start()
    {
        DialogueText.text = "";
        StartCoroutine(ShowDialogueSequence());
    }

    IEnumerator ShowDialogueSequence()
    {
        while (Index < Sentences.Length)
        {
            yield return StartCoroutine(WriteSentence(Sentences[Index]));

            yield return new WaitForSeconds(SentenceDisplayDuration);

            DialogueText.text = "";

            Index++;
        }

        // All sentences done
        gameObject.SetActive(false);
    }

    IEnumerator WriteSentence(string sentence)
    {
        DialogueText.text = "";

        string formatted = FormatSentence(sentence);

        foreach (char character in formatted)
        {
            DialogueText.text += character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
    }

    string FormatSentence(string sentence)
    {
        return $"> {sentence}\n";
    }
}