using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPClassLibrary.Patterns;

public class BooleanSingleton
{
    private bool _value;

    private BooleanSingleton(bool value)
    {
        _value = value;
    }

    public static readonly BooleanSingleton True = new BooleanSingleton(true);  
    
    public static readonly BooleanSingleton False = new BooleanSingleton(false);
}
