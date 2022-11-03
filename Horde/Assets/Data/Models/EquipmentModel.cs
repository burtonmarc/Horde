using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentModel : IModel
    {
        public List<ItemData> EquippedItems;
        
        public List<ItemData> InventoryItems;

        public EquipmentModel()
        {
            EquippedItems = new List<ItemData>();
            
            InventoryItems = new List<ItemData>();
        }
    }
}