using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Observable.FromCoroutine<Unit>(observer => Aaa(observer)).Subscribe();
	}

	IEnumerator Aaa(IObserver<Unit> observer) {
		Debug.Log(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("FirstScene");
		yield return null;

		Debug.Log(SceneManager.GetActiveScene().name);

		observer.OnNext(Unit.Default);
		observer.OnCompleted();
	}
}