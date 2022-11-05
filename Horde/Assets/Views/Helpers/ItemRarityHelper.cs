using System.Collections.Generic;
using UnityEngine;

namespace Views.Helpers
{
    public class ItemRarityHelper
    {
        private static readonly Dictionary<string, Color> rarityToColor = new Dictionary<string, Color>
        {
            {commonRarity, Color.gray},
            {uncommonRarity, Color.green},
            {rareRarity, Color.blue},
            {epicRarity, Color.red},
            {legendaryRarity, Color.yellow},
            {mythicRarity, Color.yellow},
        };

        private static Color errorColor = Color.magenta;
        
        private const string commonRarity = "Common";
        
        private const string uncommonRarity = "Uncommon";
        
        private const string rareRarity = "Rare";
        
        private const string epicRarity = "Epic";
        
        private const string legendaryRarity = "Legendary";
        
        private const string mythicRarity = "Mythic";

        public static Color GetColorByRarity(string itemRarity)
        {
            if (rarityToColor.TryGetValue(itemRarity, out var color))
            {
                return color;
            }

            return errorColor;
        }
    }
}