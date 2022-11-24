using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentTitleData : ITitleData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;
    }
    
    [Serializable]
    public class EquipmentUserData : IUserData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;

        public EquipmentUserData(EquipmentTitleData equipmentTitleData)
        {
            EquippedWeapon = equipmentTitleData.EquippedWeapon;
            EquippedNecklace = equipmentTitleData.EquippedNecklace;
            EquippedGloves = equipmentTitleData.EquippedGloves;
            EquippedArmor = equipmentTitleData.EquippedArmor;
            EquippedBelt = equipmentTitleData.EquippedBelt;
            EquippedShoes = equipmentTitleData.EquippedShoes;
            InventoryItems = equipmentTitleData.InventoryItems;
        }
    }
    
    public class EquipmentModel : ModelWithUserData<EquipmentUserData>, IModel
    {
        // References
        
        // Unsaved Data
        
        // Saved Data
        public ItemData EquippedWeapon
        {
            get => UserData.EquippedWeapon;
            set
            {
                UserData.EquippedWeapon = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public ItemData EquippedNecklace
        {
            get => UserData.EquippedNecklace;
            set
            {
                UserData.EquippedNecklace = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public ItemData EquippedGloves
        {
            get => UserData.EquippedGloves;
            set
            {
                UserData.EquippedGloves = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public ItemData EquippedArmor
        {
            get => UserData.EquippedArmor;
            set
            {
                UserData.EquippedArmor = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public ItemData EquippedBelt
        {
            get => UserData.EquippedBelt;
            set
            {
                UserData.EquippedBelt = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public ItemData EquippedShoes
        {
            get => UserData.EquippedShoes;
            set
            {
                UserData.EquippedShoes = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }

        public List<ItemData> InventoryItems
        {
            get => UserData.InventoryItems;
        }
    }
}