using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPointInstruction
{
    None,
    Jump   
}


public enum EInstructionHandle
{
    PerformOnForward,
    PerformOnReverse,
    PerformNever,
    PerformAlways
}


public class AINode : MonoBehaviour
{

    public EPointInstruction Instruction;
    public EInstructionHandle Handle;

    public float WaitDuration;

    internal Patrol Owner;
    internal Entity Ent;
    internal int NodeNum;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner.gameObject)
        {
            if (NodeNum == Owner.CurrentPoint)
            {
                if (WaitDuration > 0.0f)
                {
                    StartCoroutine(WaitDelay());
                }
                else
                {
                    PerformInstruction();
                }
            }
        }
    }


    private void PerformInstruction()
    {
        switch (Instruction)
        {
            case EPointInstruction.None:
                Owner.GoToNextPoint();
                break;


            case EPointInstruction.Jump:
                if (ShouldPerform())
                {
                    Ent.Jump();
                }
                Owner.GoToNextPoint();
                break;            
        }

    }


    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(WaitDuration);
        PerformInstruction();
    }


    bool ShouldPerform()
    {
        switch (Handle)
        {
            case EInstructionHandle.PerformOnForward:
                return !(Owner.Reverse);


            case EInstructionHandle.PerformOnReverse:
                return (Owner.Reverse);


            case EInstructionHandle.PerformNever:
                return false;


            case EInstructionHandle.PerformAlways:
                return true;
        }
        return false;
    }
}
