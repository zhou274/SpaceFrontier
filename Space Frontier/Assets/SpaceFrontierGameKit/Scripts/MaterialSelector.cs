﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFrontier
{
	public class MaterialSelector : MonoBehaviour
	{
		public Material[] availableMaterials;

		void Start()
		{
			if (availableMaterials.Length > 0)
			{
				GetComponent<Renderer>().material = availableMaterials[GameController.startingPlanetID];
			}
		}
	}
}