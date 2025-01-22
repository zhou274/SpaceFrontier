using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFrontier
{
	public class QuickDestroy : MonoBehaviour
	{
		void Start()
		{
			Destroy(gameObject, 1.25f);
		}
	}
}