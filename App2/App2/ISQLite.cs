using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
