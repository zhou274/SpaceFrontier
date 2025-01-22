using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceFrontier
{
	public class StatusController : MonoBehaviour
	{
		/// <summary>
		/// We need to show a message after each successful body detach. This status controller receives the ID
		/// and displays a message accordingly.
		/// </summary>

		internal int statusID;
		private string statusText;
		public GameObject myLabel;

		void Start()
		{
			switch (statusID)
			{
				case 0:
					statusText = "可以";
					break;
				case 1:
					statusText = "很棒";
					break;
				case 2:
					statusText = "超级棒";
					break;
				case 3:
					statusText = "精彩绝伦";
					break;
				case 4:
					statusText = "完美无瑕";
					break;
			}

			myLabel.GetComponent<TextMeshPro>().text = statusText;
			Destroy(gameObject, 1.5f);
		}
	}
}