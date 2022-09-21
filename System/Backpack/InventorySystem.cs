using System;
using System.Collections.Generic;
using System.Linq;

namespace GFramework.Backpack
{
    /// <summary>
    /// 道具系统
    /// </summary>
    public class InventorySystem : IGameSystem
    {
        // 道具集合
        private Dictionary<ItemType, Dictionary<int, Item>> itemMap = new Dictionary<ItemType, Dictionary<int, Item>>();
        // 已使用空间
        public int Occupation { get; private set; }
        // 总容量
        public int Capacity { get; private set; }

        public void Init()
        {
            // 配置表加载
        }

        public void Expend(int extra)
        {
            this.Capacity += extra;
        }

        public ItemType[] GetItemTypes()
        {
            return this.itemMap.Keys.ToArray();
        }

        public Item[] GetItemsByType(ItemType type)
        {
            if (itemMap == null)
                itemMap = new Dictionary<ItemType, Dictionary<int, Item>>();
            if (!itemMap.ContainsKey(type))
            {
                throw new Exception("不存在该道具类别");
            }
            return itemMap[type].Values.ToArray();
        }

        /// <summary>
        /// 获取道具存储到道具系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Store<T>(T item) where T : Item
        {
            if (!itemMap.ContainsKey(item.Type)) { return; }
            if (!itemMap[item.Type].ContainsKey(item.ID))
            {
                itemMap[item.Type].Add(item.ID, item);
            }
            itemMap[item.Type][item.ID].Store(item.Count);
        }

        /// <summary>
        /// 道具被使用更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Consume<T>(T item) where T : Item
        {
            if (!itemMap.ContainsKey(item.Type)) { return false; }
            if (!itemMap[item.Type].ContainsKey(item.ID)) { return false; }
            if (!itemMap[item.Type][item.ID].Consume(item.Count)) return false;
            return true;
        }
    }
}
