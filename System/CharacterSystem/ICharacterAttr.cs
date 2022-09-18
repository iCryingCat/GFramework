using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ICharacterAttr
{
    protected int hp;
    protected int maxHp;
    protected float moveSpeed;

    protected IAttrStrategy strategy;
}
