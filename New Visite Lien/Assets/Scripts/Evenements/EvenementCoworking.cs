using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenementCoworking : Evenement
{
    [SerializeField] Transform root;
    [SerializeField] float heightOffset;

    [Header("Dialogues")]
    public Dialogue introDialogue;
    public Dialogue outroDialogue;

    [Header("Animations")]
    [SerializeField] AnimationClip introClip;
    [SerializeField] AnimationClip outroClip;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        if (eventData.isCompleted)
        {
            LaunchCustomAnimationClip(introClip);
        }
        else
        {
            LaunchCustomAnimationClip(introClip);
            eventData.isCompleted = true;
            SaveManager.instance.saveSystem.Save();
        }
    }

    public void LaunchCustomAnimationClip(AnimationClip clip)
    {
        clipOverrides["Custom Animation"] = clip;
        aoc.ApplyOverrides(clipOverrides);
        eventAnimator.SetTrigger("CustomTrigger");
    }

    public void usePatrick(string animationName)
    {
        patrick.LaunchAnimation(animationName);
    }

    public void TriggerFirstDialogue()
    {
        introDialogue.TriggerDialogue();
    }

    public void TriggerSecondDialogue()
    {
        outroDialogue.TriggerDialogue();
    }

    public override void GuideeStart()
    {
        throw new System.NotImplementedException();
    }

    public override void JoueeStart()
    {
        throw new System.NotImplementedException();
    }

    public override void CompletedStart()
    {
        throw new System.NotImplementedException();
    }
}
