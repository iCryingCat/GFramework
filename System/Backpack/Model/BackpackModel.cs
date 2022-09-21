using System.Collections.Generic;

using GFramework;
using GFramework.Backpack;
using GFramework.UI;

public class BackpackModel : BaseViewModel
{
    public BindableProperty<List<Item>> items = new BindableProperty<List<Item>>();

    public void OnSwitchItemType(ItemType type)
    {
        Item[] items = GameApp.inventorySystem.GetItemsByType(type);
    }
}
