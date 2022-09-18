using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFramework.UI;
using GFramework.Backpack;
using GFramework;

public class BackpackModel : BaseViewModel
{
    public BindableProperty<List<Item>> items = new BindableProperty<List<Item>>();

    public void OnSwitchItemType(ItemType type)
    {
        Item[] items = GameApp.inventorySystem.GetItemsByType(type);
    }
}
