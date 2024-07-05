using System;
using UnityEngine;

namespace Features.CameraFeature
{
    public class CameraFollowPoint : MonoBehaviour
    {
        public Vector3 Offset;
        public Transform FollowPoint;
        public float MoveSpeed;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, FollowPoint.position + Offset,
                MoveSpeed * Time.deltaTime);
        }
    }
}