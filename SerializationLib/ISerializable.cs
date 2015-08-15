using System;
using System.Collections.Generic;
using System.Text;

namespace SerializationLib
{
    public interface ISerializable
    {
        /// <summary>
        /// Writes the object to the stream.
        /// </summary>
        /// <param name="sr">The stream to write to.</param>
        void ReadFromStream(SerializationReader sr);

        /// <summary>
        /// Reads the object from the stream.
        /// </summary>
        /// <param name="sw">The stream to read from.</param>
        void WriteToStream(SerializationWriter sw);
    }
}
