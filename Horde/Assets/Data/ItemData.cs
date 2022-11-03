using System;
using Data.Enums;

namespace Data
{
    [Serializable]
    public class ItemData
    {
        public ItemType ItemType;

        public string ItemId;

        public int ItemRarity;

        public int ItemLevel;
    }
}