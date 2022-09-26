using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    //qua passo l'oggetto perché voglio che siano solo di lettura
    void LoadData(GameData data);

    //gli passa il riferimento perché voglio che glia altri script possano effettivamente modificare i data
    void SaveData(ref GameData data); 
}
