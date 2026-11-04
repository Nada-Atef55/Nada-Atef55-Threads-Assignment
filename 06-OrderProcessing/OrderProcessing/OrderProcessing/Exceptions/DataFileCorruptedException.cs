using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Exceptions;

public class DataFileCorruptedException : Exception
{
    public DataFileCorruptedException(string message) : base(message)
    {
        
    }
}
