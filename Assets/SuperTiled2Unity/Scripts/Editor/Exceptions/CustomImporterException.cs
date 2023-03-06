using System;

namespace SuperTiled2Unity.Scripts.Editor.Exceptions
{
    public class CustomImporterException : Exception
    {
        public CustomImporterException(string msg) : base(msg)
        {
        }
    }
}
