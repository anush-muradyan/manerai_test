using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BackpackView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private List<DraggableItemPresenter> presenters;


        private void Start()
        {
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Init(List<DraggableItemData> types)
        {
            presenters.ForEach(presenter => presenter.gameObject.SetActive(false));
            foreach (var itemType in types)
            {
                var matchingPresenters = presenters.Where(p => p.Type.Equals(itemType.Type)).ToList();

                if (matchingPresenters.Count == 0)
                {
                    throw new InvalidOperationException($"No presenters found for item type {itemType}.");
                }

                if (matchingPresenters.Count > 1)
                {
                    throw new InvalidOperationException($"More than one presenter found for item type {itemType}.");
                }

                matchingPresenters[0].gameObject.SetActive(true);
            }

            gameObject.SetActive(true);
        }
        
        
        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }
    }
}