using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GFramework
{
    public class MonoPoolController : DntdMonoSingleton<MonoPoolController>
    {
        private Dictionary<PoolID, MonoPool> poolMap = new Dictionary<PoolID, MonoPool>();

        public MonoPool New(PoolID poolID, GameObject pref, int cap)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<PoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                poolMap.Add(poolID, new MonoPool(pref, cap));
            }
            return poolMap[poolID];
        }

        public MonoPool GetPool(PoolID poolID)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<PoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                return null;
            }
            return poolMap[poolID];
        }

        public void Delete(PoolID poolID)
        {
            if (poolMap == null)
            {
                poolMap = new Dictionary<PoolID, MonoPool>();
            }
            if (!poolMap.ContainsKey(poolID))
            {
                return;
            }
            poolMap.Remove(poolID);
        }
    }
}