using Data;
using UnityEngine;

namespace Game
{
    public class DraggableItemPresenter : MonoBehaviour
    {
        [SerializeField] private DraggableItemType type;

        public DraggableItemType Type => type;
    }
}