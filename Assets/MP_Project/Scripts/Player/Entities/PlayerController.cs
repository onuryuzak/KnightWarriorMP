using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Domain.Entities
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private Animator _playerAnimator;

        private Camera _mainCamera;
        private float _minX, _maxX, _minY, _maxY;
        private float _playerWidth, _playerHeight;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _playerWidth = _boxCollider2D.bounds.size.x;
            _playerHeight = _boxCollider2D.bounds.size.y;
            SetBounds();
        }

        private void Update()
        {
            if (!IsOwner) return;
            ClampPosition();
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;
            Move();
        }

        private void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveX, moveY);
            float speed = Mathf.Sqrt(Mathf.Abs(moveX) + Mathf.Abs(moveY));

            _playerAnimator.SetFloat("Speed", speed);
            if (moveX > 0)
            {
                UpdateFlipXServerRpc(false);
            }
            else if (moveX < 0)
            {
                UpdateFlipXServerRpc(true);
            }

            _rb.velocity = movement * _speed;
        }

        [ServerRpc]
        void UpdateFlipXServerRpc(bool flipX)
        {
            UpdateFlipXClientRpc(flipX);
        }

        [ClientRpc]
        void UpdateFlipXClientRpc(bool flipX)
        {
            _spriteRenderer.flipX = flipX;
        }

        private void SetBounds()
        {
            Vector3 lowerLeftCorner = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
            Vector3 upperRightCorner = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

            _minX = lowerLeftCorner.x + _playerWidth / 2;
            _maxX = upperRightCorner.x - _playerWidth / 2;
            _minY = lowerLeftCorner.y + _playerHeight / 2;
            _maxY = upperRightCorner.y - _playerHeight / 2;
        }

        private void ClampPosition()
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minX, _maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _minY, _maxY);
            transform.position = clampedPosition;
        }
    }
}