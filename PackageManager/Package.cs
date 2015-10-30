using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager
{
    public struct PackageFile
    {
        public string file_name;
        public string file_ext;
        public byte[] bytes_handle;
    }

    public class Package
    {
        public string package_name;
        private List<PackageFile> files = new List<PackageFile>( );
        
        public Package(string package_name)
        {
            this.package_name = package_name;
        }

        public void AddFile ( string file_name , string file_ext , byte[] bytes)
        {
            PackageFile pak = new PackageFile();

            pak.file_name = file_name;
            pak.file_ext = file_ext;
            pak.bytes_handle = bytes;

            files.Add(pak);
        }

        public void AddFile(PackageFile pak_file)
        {
            files.Add(pak_file);
        }

        public PackageFile FindFile( string file_name )
        {
            foreach(PackageFile file in files)
            {
                if (file.file_name + "." + file.file_ext == file_name)
                {
                    return file;
                }
            }

            return new PackageFile( );
        }

        public List<PackageFile> GetFiles() { return files; }

        public int GetFileCount() { return files.Count;  }
    }
}
