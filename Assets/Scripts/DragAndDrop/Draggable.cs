using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public class Draggable : MonoBehaviour
    {
        public UnityEvent OnDragStarted { get; } = new UnityEvent();
        public UnityEvent OnDragEnded { get; } = new UnityEvent();
        public UnityEvent OnDragging { get; } = new UnityEvent();
        protected bool IsDragging;

        protected Quaternion InitialRotation;
        protected Vector3 InitialPosition;
        protected Vector3 InitialScale;


        private void Awake()
        {
            var transform1 = transform;
            InitialRotation = transform1.rotation;
            InitialPosition = transform1.position;
            InitialScale = transform1.localScale;
        }

        protected virtual void Update()
        {
            if (IsDragging)
            {
                OnDragging?.Invoke();
            }
        }

        private void OnMouseDown()
        {
            IsDragging = true;
            OnDragStarted?.Invoke();
        }

        private void OnMouseUp()
        {
            IsDragging = false;
            OnDragEnded?.Invoke();
        }
    }
}