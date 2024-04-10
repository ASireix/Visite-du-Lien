using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrick : MonoBehaviour
{
    [SerializeField] Animator patrickAnim;

    private void Start()
    {
        StartWave();
    }
    #region Launch Animations
    /// <summary>
    /// Launch Animation using bool
    /// </summary>
    /// <param name="animation_Name"></param>
    /// <param name="state"></param>
    public void LaunchAnimation(string animation_Name, bool state){
        patrickAnim.SetBool(animation_Name,state);
    }


    /// <summary>
    /// Launch animation using Trigger
    /// </summary>
    /// <param name="animation_Name"></param>
    public void LaunchAnimation(string animation_Name){
        patrickAnim.SetTrigger(animation_Name);
    }


    /// <summary>
    /// Launch Animation using float and value
    /// </summary>
    /// <param name="animation_Name"></param>
    /// <param name="state"></param>
    public void LaunchAnimation(string animation_Name, float state){
        patrickAnim.SetFloat(animation_Name,state);
    }
#endregion
    #region Animations Trigger

    public void StartWave()
    {
        patrickAnim.SetBool("isWaving", true);
    }

    public void StopWave()
    {
        patrickAnim.SetBool("isWaving", false);
    }


    public void TriggerPoint()
    {
        patrickAnim.SetTrigger("Point");
    }

    public void TriggerClap()
    {
        patrickAnim.SetTrigger("Clap");
    }

    public void StartJul(){
        patrickAnim.SetBool("isJul", true);
    }

    public void StopJul(){
        patrickAnim.SetBool("isJul", false);
    }
#endregion
}
