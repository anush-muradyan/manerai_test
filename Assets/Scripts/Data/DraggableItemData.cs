using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class DraggableItemData
    {
        [SerializeField] private DraggableItemType type;
        [SerializeField] private float weight;
        [SerializeField] private int identifier;
        [SerializeField] private string name;

        public DraggableItemType Type => type;
        public float Weight => weight;
        public int Identifier => identifier;
        public string Name => name;
    }
}