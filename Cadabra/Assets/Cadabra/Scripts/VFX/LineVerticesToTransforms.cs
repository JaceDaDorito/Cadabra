using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.Vfx
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineVerticesToTransforms : MonoBehaviour
    {
        [SerializeField]
        private Transform[] nodes = Array.Empty<Transform>();
        private LineRenderer lineRenderer;

        private Vector3[] vertices = Array.Empty<Vector3>();

        private void Awake()
        {
            Array.Resize(ref vertices, nodes.Length);
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            if (nodes.Length == 0) gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Transform transform = nodes[i];
                if (transform) vertices[i] = transform.position;
            }
            lineRenderer.SetPositions(vertices);
        }
    }
}
