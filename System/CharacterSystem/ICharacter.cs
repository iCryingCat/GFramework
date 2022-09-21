using UnityEngine;
using UnityEngine.AI;

public abstract class ICharacter
{
    protected int id;
    protected string name;
    protected GameObject prefGO;
    protected string iconPath;
    protected AudioSource audioSource;
    protected NavMeshAgent navMeshAgent;

    protected ICharacterAttr attr;

    protected IWeapon weapon;

    public ICharacter(int id, string name, GameObject prefGO, string iconPath)
    {
        this.id = id;
        this.name = name;
        this.prefGO = prefGO;
        this.iconPath = iconPath;

        this.audioSource = this.prefGO.GetComponent<AudioSource>();
        this.navMeshAgent = this.prefGO.GetComponent<NavMeshAgent>();
    }


    public void SetWeapon(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public abstract void Attack(ICharacter targetCharacter);

    public abstract void Attacked(ICharacter attacker);
}
