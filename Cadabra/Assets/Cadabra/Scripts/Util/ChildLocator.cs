using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cadabra.Util
{
	//this sucks
	public class ChildLocator : MonoBehaviour
	{
		[SerializeField]
		private NameTransformPair[] nameTransformPairs = Array.Empty<NameTransformPair>();
		public struct NameTransformPair
        {
			public string name;
			public Transform transform;
        }
		public int Count
		{
			get
			{
				return nameTransformPairs.Length;
			}
		}

		public Transform FindTransform(int index)
        {
			return nameTransformPairs[index].transform;
        }
    }
}

