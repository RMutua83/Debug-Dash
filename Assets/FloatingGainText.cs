using UnityEngine;
using TMPro;

public class FloatingGainText : MonoBehaviour
{
    public float floatSpeed = 40f;
    public float fadeTime = 1.5f;

    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        if (text == null)
        {
            Debug.LogError("FloatingGainText: TMP_Text not found! Attach this to a TextMeshProUGUI component.");
        }
    }

    void Start()
    {
        Destroy(gameObject, fadeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        if (text != null)
        {
            Color c = text.color;
            c.a -= Time.deltaTime / fadeTime;
            text.color = c;
        }
    }

    public void SetText(string content)
    {
        if (text == null) text = GetComponent<TMP_Text>();
        text.text = content;
    }
}
