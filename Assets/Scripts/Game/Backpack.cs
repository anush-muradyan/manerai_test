using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private Camera renderCamera;
        
        public UnityEvent OnBackpackClicked { get; } = new UnityEvent();

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            var ray = renderCamera.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Backpack") || hit.collider.CompareTag("Chest"))
                {
                    OnBackpackClicked?.Invoke();
                }
            }
        }
    }
}