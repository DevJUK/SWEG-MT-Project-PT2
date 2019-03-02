using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
	public class NewTestScript
	{
		[UnityTest]
		public IEnumerator CallyTest()
		{
			SceneManager.LoadScene("GroundFloorScene");

			yield return new WaitForSeconds(2);


			GameObject Cally = GameObject.FindGameObjectWithTag("Player");

			Assert.IsNotNull(Cally);


			yield return new WaitForSeconds(2);
		}

		[UnityTest]
		public IEnumerator CallyMoved()
		{
			SceneManager.LoadScene("GroundFloorScene");

			yield return new WaitForSeconds(2);

			GameObject Cally = GameObject.FindGameObjectWithTag("Player");

			var StartPos = Cally.transform.position;

			yield return new WaitForSeconds(5);

			Assert.That(StartPos, Is.GreaterThan(Cally.transform.position));

		}

	}
}
