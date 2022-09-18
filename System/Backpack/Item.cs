using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GFramework;

namespace GFramework.Backpack
{
    public enum ItemType
    {

    }

    public class Item : IStorage
    {
        public ItemType Type { get; protected set; }
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Size { get; protected set; }
        public int OriginalPrice { get; protected set; }
        public int PresentPrice { get; protected set; }
        public string Icon { get; protected set; }
        public string Pref { get; protected set; }
        public int Count { get; protected set; }

        public Item(ItemType type, int id, string name, string description, int size, int originalPrice, int presentPrice, string sprite, string prefPath, int count)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Size = size;
            this.OriginalPrice = originalPrice;
            this.PresentPrice = presentPrice;
            this.Icon = sprite;
            this.Pref = prefPath;
            this.Count = count;
        }

        public void Store(int count)
        {
            this.Count += count;
        }

        public bool Consume(int count)
        {
            if (this.Count - count > 0)
            {
                this.Count -= count;
                return true;
            }
            return false;
        }
    }
}
