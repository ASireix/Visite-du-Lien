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
}
