using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptsEntry
{
    
    public EPlayerScriptsType key;
    
    [TextArea(3, 10)]
    public string sentences;  


}
