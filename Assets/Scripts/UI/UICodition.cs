using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICodition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    void Start()
    {
        CharacterManager.Instance.player.condition.uiCondition = this;
    }
}
