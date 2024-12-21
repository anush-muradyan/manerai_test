using Data;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public class DraggableItem : Draggable
    {
        [SerializeField] private Camera renderCamera;
        [SerializeField] private Transform ground;
        [SerializeField] private Rigidbody rigidbody;
        
        private Backpack _backpack;
        private int _identifier;
        private string _name;
        private DraggableItemType _type;
        
        private Vector3 _offset;
        private float _dragDepth = 0f;
        private bool _isInBackpack;
        private DraggableItemData _data;
        private bool _isInGround;
        private Vector3 _backpackPosition;
        private const float Duration = 0.3f;
        private const float InBackPackScale = 0.5f;
        
        public UnityEvent<DraggableItemData> OnBackpackAdded { get; } = new();
        public UnityEvent<DraggableItemData> OnBackpackRemoved { get; } = new();


        private void Start()
        {
            OnDragStarted.AddListener(DragStarted);
            OnDragEnded.AddListener(DragEnded);
            OnDragging.AddListener(Dragging);
        }

        public void Init(DraggableItemData data, Backpack backpack, Vector3 backpackPosition)
        {
            _data = data;
            rigidbody.mass = data.Weight;
            _backpack = backpack;
            _backpackPosition = backpackPosition;
        }

        protected override void Update()
        {
            base.Update();
            if (_isInBackpack)
            {
                transform.position = _backpackPosition;
            }
        }

        private void DragStarted()
        {
            _isInBackpack = false;
            _isInGround = false;
            transform.DOScale(InitialScale, 0.3f);
            Ray ray = renderCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _dragDepth = Vector3.Distance(renderCamera.transform.position, hit.point);
                _offset = transform.position - hit.point;
            }
        }

        private void Dragging()
        {
            Ray ray = renderCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPosition = ray.GetPoint(_dragDepth) + _offset;
            targetPosition.z = InitialPosition.z;

            var transform1 = transform;
            transform1.position = targetPosition;
            transform1.rotation = InitialRotation;
        }

        private void DragEnded()
        {
        }

        private void OnCollisionEnter(Collision other)
        {
            //check if the hit transform is backpack
            if (_backpack.transform.Equals(other.transform))
            {
                _isInGround = false;
                _isInBackpack = true;
                transform.DOScale(Vector3.one * InBackPackScale, Duration);
                OnBackpackAdded?.Invoke(_data);
                transform.DOMove(_backpackPosition, Duration);
            }
            
            //check if the hit transform is ground
            else if (ground.Equals(other.transform))
            {
                _isInBackpack = false;
                _isInGround = true;
                transform.DOScale(InitialScale, Duration);
                OnBackpackRemoved?.Invoke(_data);
            }
        }

        public void ResetTransform()
        {
            var transform1 = transform;
            transform1.position = InitialPosition;
            transform1.rotation = InitialRotation;
            transform1.localScale = InitialScale;
        }

        private void OnDestroy()
        {
            OnDragStarted.RemoveListener(DragStarted);
            OnDragEnded.RemoveListener(DragEnded);
            OnDragging.RemoveListener(Dragging);
        }
    }
}