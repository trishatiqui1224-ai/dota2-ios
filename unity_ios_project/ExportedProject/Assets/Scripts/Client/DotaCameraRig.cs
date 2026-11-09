using System;
using UnityEngine;

namespace Dota.Client
{
    public class DotaCameraRig : MonoBehaviour
    {
        public Transform target;
        public bool locked = true;

        private Camera cam;
        private Vector3 focus;

        // Dota 2 standard isometric camera offset:
        // Height +29.7, Distance back -17.15, Tilt 60 degrees down
        private readonly Vector3 cameraOffset = new Vector3(0f, 29.696f, -17.145f);

        private void Awake()
        {
            cam = GetComponent<Camera>();
            if (cam != null)
            {
                cam.enabled = true;
            }
            locked = true;
        }

        public void SnapTo(Vector3 p)
        {
            focus = p;
            transform.position = focus + cameraOffset;
            transform.rotation = Quaternion.Euler(60f, 0f, 0f);
        }

        public void LookAt(Vector3 worldPoint)
        {
            SnapTo(worldPoint);
        }

        public void PanBy(Vector2 screenDelta)
        {
            focus.x += -screenDelta.x * 0.08f;
            focus.z += -screenDelta.y * 0.08f;
            transform.position = focus + cameraOffset;
        }

        private void LateUpdate()
        {
            if (locked && target != null)
            {
                focus = Vector3.Lerp(focus, target.position, Time.deltaTime * 10f);
                transform.position = focus + cameraOffset;
                return;
            }

            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                    PanBy(t.deltaPosition);
                }
            }
        }
    }
}
