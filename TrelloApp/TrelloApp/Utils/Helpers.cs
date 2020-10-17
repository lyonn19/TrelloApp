using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrelloApp.Utils
{

    public static class StreamHelpers
    {
        public static byte[] ReadFully(this Stream input)
        {
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                //stream.Dispose();
                return memoryStream.ToArray();
            }
        }
    }
}
