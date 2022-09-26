using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    //qua passo l'oggetto perch� voglio che siano solo di lettura
    void LoadData(GameData data);

    //gli passa il riferimento perch� voglio che glia altri script possano effettivamente modificare i data
    void SaveData(ref GameData data); 
}
