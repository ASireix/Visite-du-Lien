using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public abstract class Evenement : MonoBehaviour
{
    public EventData eventData;
    protected Animator eventAnimator;
    [SerializeField] protected Patrick patrick;
    [System.NonSerialized]
    public static UnityEvent<EventData> onEventCompleted = new UnityEvent<EventData>();

    protected AnimatorOverrideController aoc;
    protected AnimationClipOverrides clipOverrides;
    bool init;
    // Start is called before the first frame update
    void Start()
    {
        eventAnimator = GetComponent<Animator>();
        aoc = new AnimatorOverrideController(eventAnimator.runtimeAnimatorController);
        eventAnimator.runtimeAnimatorController = aoc;

        clipOverrides = new AnimationClipOverrides(aoc.overridesCount);
        aoc.GetOverrides(clipOverrides);
        OnStart();
        init = true;
        LaunchStart();
    }

    void LaunchStart()
    {
        if (eventData.isCompleted)
        {
            CompletedStart();
            return;
        }
        if (SETTINGS.isGuidee)
        {
            GuideeStart();
        }
        else
        {
            JoueeStart();
        }
    }

    protected virtual void OnStart() { }

    public abstract void GuideeStart();

    public abstract void JoueeStart();

    public abstract void CompletedStart();

    public virtual void CompleteEvent()
    {
        eventData.isCompleted = true;
        SaveManager.instance.saveSystem.Save();
        onEventCompleted?.Invoke(eventData);
    }

    protected virtual void Reset(){
        Debug.Log("Empty reset");
    }

    void OnEnable()
    {
        if (init)
        {
            LaunchStart();
        }
    }
}
