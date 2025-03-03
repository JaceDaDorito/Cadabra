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
		private Transform[] transformArray = Array.Empty<Transform>();
		public int Count
		{
			get
			{
				return transformArray.Length;
			}
		}

		public Transform FindTransform(int index)
        {
			return transformArray[index].transform;
        }
    }
}

