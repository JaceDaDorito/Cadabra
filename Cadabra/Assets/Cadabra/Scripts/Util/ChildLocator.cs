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
		private NameTransformPair[] pairArray = Array.Empty<NameTransformPair>();

		[Serializable]
		private struct NameTransformPair
        {
			public string name;
			public Transform transform; 
        }
		public int Count
		{
			get
			{
				return pairArray.Length;
			}
		}

		public Transform FindTransform(int index)
        {
			return pairArray[index].transform;
        }

		public Transform FindTransform(string name)
        {
			foreach(NameTransformPair ntp in pairArray)
            {
				if (ntp.name.Equals(name)) return ntp.transform;
            }
			return null;
        }
    }
}

