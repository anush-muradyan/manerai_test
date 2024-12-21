using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DraggableItemsData", order = 1)]

    public class DraggableItemsData : ScriptableObject
    {
        [SerializeField] private List<DraggableItemData> draggableItems;
        public List<DraggableItemData> DraggableItems => draggableItems;
    }
}