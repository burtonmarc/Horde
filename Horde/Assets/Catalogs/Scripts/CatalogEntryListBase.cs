using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Scripts
{
    public class CatalogEntryListBase<T> : ScriptableObject where T : CatalogEntryId
    {
        [SerializeField] private List<T> CatalogEntries;

        public T GetCatalogEntry(string id)
        {
            foreach (var catalogEntry in CatalogEntries)
            {
                if (catalogEntry.Id == id)
                {
                    return catalogEntry;
                }
            }
            
            throw new NotSupportedException($"Couldn't find any entry with id: {id}"); 
        }
    }
}