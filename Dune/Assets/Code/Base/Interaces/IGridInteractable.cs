using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridInteractable {
	void StartInteract ();
	void StopInteract ();
	string GetName();
}
