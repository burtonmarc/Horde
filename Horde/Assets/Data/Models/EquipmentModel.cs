using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentModelData : IModelData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;

        public EquipmentModelData()
        {
            InventoryItems = new List<ItemData>();
        }
    }
    
    public class EquipmentModel : SaveableBaseModel, IModel
    {
        private EquipmentModelData equipmentModelData;
        
        // References
        
        // Unsaved Data
        
        // Saved Data
        public ItemData EquippedWeapon
        {
            get => equipmentModelData.EquippedWeapon;
            set
            {
                equipmentModelData.EquippedWeapon = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }
        
        public ItemData EquippedNecklace
        {
            get => equipmentModelData.EquippedNecklace;
            set
            {
                equipmentModelData.EquippedNecklace = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }
        
        public ItemData EquippedGloves
        {
            get => equipmentModelData.EquippedGloves;
            set
            {
                equipmentModelData.EquippedGloves = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }
        
        public ItemData EquippedArmor
        {
            get => equipmentModelData.EquippedArmor;
            set
            {
                equipmentModelData.EquippedArmor = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }
        
        public ItemData EquippedBelt
        {
            get => equipmentModelData.EquippedBelt;
            set
            {
                equipmentModelData.EquippedBelt = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }
        
        public ItemData EquippedShoes
        {
            get => equipmentModelData.EquippedShoes;
            set
            {
                equipmentModelData.EquippedShoes = value;
                DataGateway.UpdateUserData(equipmentModelData);
            }
        }

        public List<ItemData> InventoryItems
        {
            get => equipmentModelData.InventoryItems;
        }

        public override void AddModelData(IModelData modelData)
        {
            equipmentModelData = modelData as EquipmentModelData;
        }

        public void AddInventoryItem(ItemData itemData)
        {
            equipmentModelData.InventoryItems.Add(itemData);
            // TODO: Add sort to the inventory items
            //equipmentModelData.InventoryItems.Sort(item => item.ItemRarity);
        }
    }
}