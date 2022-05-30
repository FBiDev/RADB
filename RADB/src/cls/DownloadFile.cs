using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RADB
{
    public class DownloadFile : IEquatable<DownloadFile>
    {
        public string URL { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }

        public long BytesReceived { get; set; }
        public long TotalBytesToReceive { get; set; }
        public float ProgressPercentage { get; set; }

        public DownloadFile(string url, string path)
        {
            URL = url;
            Path = path;
            Name = new FileInfo(Path).Name;
        }

        public bool Equals(DownloadFile other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return URL.Equals(other.URL) && Path.Equals(other.Path);
            //return URL.Equals(other.URL);
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            int hashProductPath = Path == null ? 0 : Path.GetHashCode();

            //Get hash code for the Code field.
            int hashProductURL = URL == null ? 0 : URL.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductPath ^ hashProductURL;
            //return hashProductURL;
        }
    }
}
