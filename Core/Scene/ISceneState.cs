using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{
    private string stateName = "ISceneState";

    public string StateName { get => stateName; set => stateName = value; }

    protected SceneStateController controller;

    public ISceneState(SceneStateController controller)
    {
        this.controller = controller;
    }

    public virtual void StateEnter()
    {

    }

    public virtual void StateBegin()
    {

    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateEnd()
    {

    }

    public virtual void StateExit()
    {

    }
}
