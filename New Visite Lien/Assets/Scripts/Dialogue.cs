using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [Header("Visual representation")]
    [SerializeField] TextMeshProUGUI uiText;
    public DialogueBox dialogueBox;
    [SerializeField] float typeSpeed;

    [Header("Events")]
    [SerializeField] CustomDialogueEvent[] customEvents;
    Dictionary<string, UnityEvent<Dialogue>> _customEventDico = new Dictionary<string, UnityEvent<Dialogue>>();
    
    [SerializeField] TextAsset textFile;
    Queue<string> _dialogue = new Queue<string>();

    bool _isTyping;
    string _textTyped;
    Coroutine _typingCoroutine;

    [System.NonSerialized]
    public UnityEvent onSentenceTyped = new UnityEvent();

    [System.NonSerialized]
    public UnityEvent onDialogueComplete = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        #region Init Dico
        for(int i = 0; i < customEvents.Length; i++)
        {
            CustomDialogueEvent cEvent = customEvents[i];
            _customEventDico.TryAdd(cEvent.eventIdentifier, cEvent.eventToTrigger);
        }
        #endregion
        dialogueBox.InitDialogueBox(this);
    }

    public void TriggerDialogue()
    {
        ReadFile();

        OpenDialogueBox();
        PrintDialogue();
    }

    public void AdvanceDialogue()
    {
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (_isTyping)
        {
            StopCoroutine(_typingCoroutine);
            uiText.text = _textTyped;
            uiText.maxVisibleCharacters = uiText.textInfo.characterCount;
            _isTyping = false;
            onSentenceTyped?.Invoke();
            return;
        }

        if (_dialogue.Peek().Contains("EndQueue"))
        {
            _dialogue.Dequeue();
            EndDialogue();
        }
        else if (_dialogue.Peek().Contains("[EVENT="))
        {
            string eventName = _dialogue.Peek();
            eventName = _dialogue.Dequeue().Substring(eventName.IndexOf("=") + 1, eventName.IndexOf("]") - (eventName.IndexOf("=") + 1));
            if(_customEventDico.TryGetValue(eventName, out UnityEvent<Dialogue> v))
            {
                v?.Invoke(this);
            }
            PrintDialogue();
        }
        else
        {
            _typingCoroutine = StartCoroutine(TypeSentence(_dialogue.Dequeue()));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        _textTyped = sentence;
        _isTyping = true;
        
        uiText.text = sentence;
        uiText.maxVisibleCharacters = 0;

        char[] letters = sentence.ToCharArray();
        int index = 0;
        while (index < letters.Length){
            uiText.maxVisibleCharacters++;
            index++;
            yield return new WaitForSeconds((1f/typeSpeed) / 10f); 
        }

        _isTyping = false;
        onSentenceTyped?.Invoke();
        /*
        foreach(char letter in sentence.ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds((1f/typeSpeed+1f) / 10f);
        }
        
        */
    }

    public void EndDialogue()
    {
        CloseDialogueBox();
        onDialogueComplete?.Invoke();
    }

    void ReadFile()
    {
        string txt = textFile.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray());
        string lastLine = "";

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrEmpty(lastLine) && !string.IsNullOrEmpty(line))
            {
                if (lines[i].StartsWith("["))
                {
                    string special = line.Substring(0, line.IndexOf("]") + 1);
                    string curr = line.Substring(line.IndexOf("]") + 1);

                    _dialogue.Enqueue(special);
                    lastLine += curr; 
                }
                else
                {
                    lastLine += line;
                }
            }
            else if (string.IsNullOrEmpty(line) && !string.IsNullOrEmpty(lastLine))
            {
                _dialogue.Enqueue(lastLine);
                lastLine = "";
            }
            else
            {
                lastLine += line;
            }
        }

        if (!string.IsNullOrEmpty(lastLine)){
            _dialogue.Enqueue(lastLine);
            lastLine = "";
        }
        /*
        foreach (string line in lines)
        {
            
            if (!string.IsNullOrEmpty(line))
            {
                if (line.StartsWith("["))
                {
                    string special = line.Substring(0, line.IndexOf("]") + 1);
                    string curr = line.Substring(line.IndexOf("]") + 1);
                    _dialogue.Enqueue(special);
                    _dialogue.Enqueue(curr);
                }
                else
                {
                    _dialogue.Enqueue(line);
                }
            }
        }
        */
        _dialogue.Enqueue("EndQueue");
    }

    public void OpenDialogueBox()
    {
        dialogueBox.gameObject.SetActive(false);
        LeanTween.scale(dialogueBox.gameObject, Vector3.zero, 0f).setOnComplete(() =>
        {
            dialogueBox.gameObject.SetActive(true);
            LeanTween.scale(dialogueBox.gameObject, Vector3.one, 1f).setEaseInBounce();
        });
    }

    public void CloseDialogueBox()
    {
        LeanTween.scale(dialogueBox.gameObject, Vector3.zero, 1f).setEaseOutBounce().setOnComplete(() => {
            dialogueBox.gameObject.SetActive(false);
            });
        
    }
}
