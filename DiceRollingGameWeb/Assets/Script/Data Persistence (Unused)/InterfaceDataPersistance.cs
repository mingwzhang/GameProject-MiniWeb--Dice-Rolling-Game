using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceDataPersistance
{
    void LoadData(PlayerData data);

    void SaveData(ref PlayerData data);
}
