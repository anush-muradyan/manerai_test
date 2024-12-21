using System.Collections.Generic;
using System.Linq;
using Data;
using DragAndDrop;
using Newtonsoft.Json;
using Request;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private DraggableItemsData draggableItemsData;
        [SerializeField] private List<DraggableItem> draggableItems;
        [SerializeField] private BackpackView backpackView;
        [SerializeField] private Backpack backpack;
        [SerializeField] private List<Transform> backpackPositions;

        private readonly List<DraggableItemData> _inBackpackItems = new();

        private void Start()
        {
            InitDraggableItems();
            backpack.OnBackpackClicked.AddListener(OpenBackpackView);
        }

        private void OpenBackpackView()
        {
            backpackView.Init(_inBackpackItems);
        }

        private void InitDraggableItems()
        {
            for (var index = 0; index < draggableItemsData.DraggableItems.Count; index++)
            {
                if (draggableItems.Count < index || backpackPositions.Count < index)
                {
                    return;
                }

                var itemData = draggableItemsData.DraggableItems[index];
                draggableItems[index].Init(itemData, backpack, backpackPositions[index].position);
                draggableItems[index].OnBackpackAdded.AddListener(OnBackpackAdded);
                draggableItems[index].OnBackpackRemoved.AddListener(OnBackpackRemoved);
            }
        }

        private async void OnBackpackAdded(DraggableItemData itemData)
        {
            if (_inBackpackItems.Contains(itemData))
            {
                return;
            }

            _inBackpackItems.Add(itemData);
            var a = new
            {
                Items = _inBackpackItems.Select(data => data.Type.ToString()).ToList()
            };


            await Request.Request.SendRequestAsync(EndpointsMapper.BaseUrl,
                JsonConvert.SerializeObject(a), "POST");
            
        }   

        private async void OnBackpackRemoved(DraggableItemData itemData)
        {
            if (!_inBackpackItems.Contains(itemData))
            {
                return;
            }
            
            _inBackpackItems.Remove(itemData);
            await Request.Request.SendRequestAsync(EndpointsMapper.BaseUrl,
                JsonConvert.SerializeObject(_inBackpackItems), "POST");
        }
    }
}