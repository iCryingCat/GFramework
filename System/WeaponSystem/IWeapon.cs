using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器基类
/// </summary>
public abstract class IWeapon
{
    protected int id;
    protected string name;

    protected int atk = 0;
    protected float atkRange = 0.0f;

    /// <summary>
    /// 武器模型
    /// </summary>
    protected GameObject prefGO;
    /// <summary>
    /// 武器持有者
    /// </summary>
    protected ICharacter character;

    protected ParticleSystem particleSystem;
    protected AudioSource audioSource;
    protected LineRenderer lineRenderer;
    protected Light light;

    protected void PlayShootEffect()
    {

    }

    protected void PlaySoundEffect()
    {

    }

    public virtual void Fire()
    {

    }
}
