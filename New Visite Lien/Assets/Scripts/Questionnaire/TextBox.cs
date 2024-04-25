using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum BoxToggleType
{
    None,
    Disable,
    TweenBounce,
    SmokeVFX
}
public class TextBox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI TMP_text;
    [Header("Type settings")]
    [SerializeField] BoxToggleType toggleType = BoxToggleType.Disable;
    [SerializeField] float typeSpeed;
    [SerializeField] bool writeAtStart;
    [SerializeField] string baseText;
    [Header("HOVER")]
    [SerializeField] bool hover;
    [Tooltip("How fast it moves")]
    [SerializeField] float hoverSpeed = 0.3f;
    [Tooltip("How far it goes")]
    [SerializeField] float hoverStrength = 0.05f;
    [Header("VFX")]
    public GameObject smokeVFX;
    [SerializeField] Vector3 vfx_Pos = Vector3.zero;
    [SerializeField] float vfx_Scale = 1f;
    GameObject _spawnedVFX;
    [System.NonSerialized]
    public UnityEvent onTextTyped = new UnityEvent();
    public UnityEvent onClickEvent;
    [System.NonSerialized]
    public Vector3 startScale;

    void Awake()
    {
        startScale = transform.localScale;
    }

    void Start()
    {
        if (toggleType == BoxToggleType.SmokeVFX)
        {
            _spawnedVFX = Instantiate(smokeVFX, transform);
            _spawnedVFX.transform.localPosition = vfx_Pos;
            _spawnedVFX.transform.localScale = new Vector3(vfx_Scale,vfx_Scale,vfx_Scale);
            _spawnedVFX.SetActive(false);
        }
        if (writeAtStart) WriteText(baseText);
        if (hover) HoverDown();
    }

    void HoverDown()
    {
        LeanTween.moveY(gameObject, transform.position.y - hoverStrength, 1f / hoverSpeed).setOnComplete(HoverUp);
    }
    void HoverUp()
    {
        LeanTween.moveY(gameObject, transform.position.y + hoverStrength, 1f / hoverSpeed).setOnComplete(HoverDown);
    }

    public void WriteText(string sentence, bool instantanious = false)
    {
        if (instantanious)
        {
            TMP_text.text = sentence;
        }
        else
        {
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        TMP_text.text = sentence;
        TMP_text.maxVisibleCharacters = 0;

        char[] letters = sentence.ToCharArray();
        int index = 0;
        while (index < letters.Length)
        {
            TMP_text.maxVisibleCharacters++;
            index++;
            yield return new WaitForSeconds((1f / typeSpeed) / 10f);
        }
        onTextTyped?.Invoke();
    }

    public void ClearText()
    {
        TMP_text.text = "";
    }

    public void ToggleBox(bool on, BoxToggleType boxToggleType = BoxToggleType.None)
    {
        BoxToggleType toggleTypeTemp;
        if (boxToggleType == BoxToggleType.None)
        {
            toggleTypeTemp = toggleType;
        }
        else
        {
            toggleTypeTemp = boxToggleType;
        }
        if (on)
        {
            gameObject.SetActive(true);
            LeanTween.scale(gameObject, startScale, 0f);

            switch (toggleTypeTemp)
            {
                case BoxToggleType.Disable:
                    break;
                case BoxToggleType.TweenBounce:
                    LeanTween.scale(gameObject, Vector3.zero, 0f);
                    LeanTween.scale(gameObject, startScale, 1f).setEaseOutBounce();
                    break;
                case BoxToggleType.SmokeVFX:
                    if (!_spawnedVFX)
                    {
                        _spawnedVFX = Instantiate(smokeVFX, transform);
                        _spawnedVFX.transform.localPosition = vfx_Pos;
                        _spawnedVFX.transform.localScale = new Vector3(vfx_Scale,vfx_Scale,vfx_Scale);
                    }
                    else
                    {
                        _spawnedVFX.transform.SetParent(transform);
                        _spawnedVFX.transform.localPosition = Vector3.zero;
                        _spawnedVFX.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (toggleTypeTemp)
            {
                case BoxToggleType.Disable:
                    gameObject.SetActive(false);
                    break;
                case BoxToggleType.TweenBounce:
                    LeanTween.scale(gameObject, Vector3.zero, 1f).setEaseInBounce().
                    setOnComplete(() => { gameObject.SetActive(false); });
                    break;
                case BoxToggleType.SmokeVFX:
                    if (!_spawnedVFX)
                    {
                        _spawnedVFX = Instantiate(smokeVFX, transform);
                        _spawnedVFX.transform.localPosition = vfx_Pos;
                        _spawnedVFX.transform.localScale = new Vector3(vfx_Scale,vfx_Scale,vfx_Scale);
                    }
                    else
                    {
                        _spawnedVFX.transform.SetParent(null);
                        _spawnedVFX.SetActive(false);
                        _spawnedVFX.SetActive(true);
                        LeanTween.scale(gameObject, Vector3.zero, 0f);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void Shake()
    {
        LTSeq sequence = LeanTween.sequence();
        sequence.append(
            LeanTween.scale(gameObject, startScale * 0.5f, 0.2f)
        );
        sequence.append(
            LeanTween.scale(gameObject, startScale, 1f).setEaseOutBounce()
        );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Text box is clicked");
        onClickEvent?.Invoke();
    }
}
