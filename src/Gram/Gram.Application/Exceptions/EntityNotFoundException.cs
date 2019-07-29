using System;
using System.Collections.Generic;
using System.Text;

namespace Gram.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, int key)
            : base($"Entity \"{ name }\" with primary key { key } was not found.")
        {
        }
    }
}
