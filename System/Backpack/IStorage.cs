using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 可存放接口
/// 实现该接口的物品可放入背包
/// </summary>
public interface IStorage
{
    void Store(int count);
    bool Consume(int count);
}
