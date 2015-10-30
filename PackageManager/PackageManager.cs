using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PackageManager
{
    public class PackageManager
    {
        string editor_directory;
        string project_path;

        public PackageManager(string start_path,string project_path)
        {
            this.editor_directory = start_path;
            this.project_path = project_path;
        }

        public List<string> GetPackages()
        {
            List<string> ret_list = new List<string> ( );

            foreach(string path in Directory.GetFiles(editor_directory + "\\Packages"))
            {
                if (Path.GetExtension(path) == ".pak" || Path.GetExtension(path) == "pak")
                {
                    ret_list.Add(Path.GetFileNameWithoutExtension(path));
                }
            }

            return ret_list;
        }

        public Package CreatePackage(string package_name , List<string> file_paths)
        {
            if (File.Exists(editor_directory + "\\Packages\\" + package_name + ".pak")) return null;

            FileStream file_stm = File.Create(editor_directory + "\\Packages\\" + package_name + ".pak");
            Package pak = new Package(package_name);

            // Make the structure of pak file
            file_stm.Write(new byte[]{ (byte) 'H' ,  (byte) 'P' },0,2);
            file_stm.Write(BitConverter.GetBytes(file_paths.Count), 0, 4);

            foreach(string file_path in file_paths)
            {
                string file_name = Path.GetFileNameWithoutExtension(file_path);
                string file_ext = Path.GetExtension(file_path).Substring(1, Path.GetExtension(file_path).Length - 1);

                foreach(char ch in file_name)
                {
                    file_stm.WriteByte((byte)ch);
                }

                file_stm.WriteByte((byte)'.');

                foreach (char ch in file_ext)
                {
                    file_stm.WriteByte((byte)ch);
                }

                file_stm.WriteByte(0); // End of file name.

                FileStream stm = new FileStream(file_path, FileMode.Open);
                byte[] bytes = new byte[0];

                while(stm.Position != stm.Length)
                {
                    Array.Resize<byte>(ref bytes,bytes.Length + 1);
                    bytes[bytes.Length - 1] = (byte) stm.ReadByte();
                }

                file_stm.Write(BitConverter.GetBytes(bytes.Length), 0, 4);

                foreach(byte bt in bytes)
                {
                    file_stm.WriteByte(bt);
                }

                pak.AddFile(file_name,file_ext,bytes);
            }

            return pak;
        }

        public Package ReadPackage( string file_path )
        {
            if (Path.GetExtension(file_path) != ".pak" && Path.GetExtension(file_path) != "pak") return null;

            Package pak = new Package( Path.GetFileNameWithoutExtension(file_path ) );
            FileStream file_stm = new FileStream(file_path,FileMode.Open);

            if (file_stm.ReadByte() == (byte)'H' && file_stm.ReadByte() == (byte)'P') // Read first two bytes and check for the format.
            {
                int total_files = BitConverter.ToInt32(ReadBytes(file_stm, 4), 0);

                while (file_stm.Position != file_stm.Length)
                {
                    PackageFile pak_file;
                    pak_file.file_name = ReadString(file_stm, '.'); 
                    
                    pak_file.file_ext = ReadString(file_stm, (char)0);
                    int total_bytes = BitConverter.ToInt32( ReadBytes(file_stm, 4) , 0 );
                    pak_file.bytes_handle = ReadBytes(file_stm, total_bytes);

                    if (pak_file.bytes_handle == null) return null;

                    pak.AddFile(pak_file);
                }

                if (total_files != pak.GetFileCount()) return null;
            }
            else return null;

            return pak;
        }
        
        public void ExtractPackage( Package pak )
        {
            foreach(PackageFile pak_file in pak.GetFiles( ))
            {
                if (pak_file.file_ext == "obj")
                {
                    if (!File.Exists(project_path + "\\Game-Objects\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes(File.Create(project_path + "\\Game-Objects\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else if (pak_file.file_ext == "hvl")
                {
                    if (!File.Exists(project_path + "\\Game-Levels\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes( File.Create(project_path + "\\Game-Levels\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else if (pak_file.file_ext == "anim")
                {
                    if (!File.Exists(project_path + "\\Game-Animation\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes(File.Create(project_path + "\\Game-Animation\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else if (pak_file.file_ext == "nav")
                {
                    if (!File.Exists(project_path + "\\Game-Navigation\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes(File.Create(project_path + "\\Game-Navigation\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else if (pak_file.file_ext == "lib" || pak_file.file_ext == "dll" || pak_file.file_ext == "jar" || pak_file.file_ext == "so" || pak_file.file_ext == "a")
                {
                    if (!File.Exists(project_path + "\\Game-Plugins\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes( File.Create(project_path + "\\Game-Plugins\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else if (pak_file.file_ext == "cs" || pak_file.file_ext == "java" || pak_file.file_ext == "cpp" || pak_file.file_ext == "bs")
                {
                    if (!File.Exists(project_path + "\\Game-Scripts\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes(File.Create(project_path + "\\Game-Scripts\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
                else
                {
                    if (!File.Exists(project_path + "\\Game-Resouces\\" + pak_file.file_name + pak_file.file_ext))
                    {
                        WriteBytes(File.Create(project_path + "\\Game-Resouces\\" + pak_file.file_name + "." + pak_file.file_ext), pak_file.bytes_handle);
                    }
                }
            }
        }

        private string ReadString(FileStream stream_handle , char eof_param)
        {
            string ret_string = "";

            while(stream_handle.Position != stream_handle.Length)
            {
                byte bt = (byte) stream_handle.ReadByte();

                if (bt == (byte)eof_param) return ret_string;

                ret_string += (char) bt;
            }

            return "";
        }

        private byte[] ReadBytes(FileStream stream_handle , int count)
        {
            byte[] bytes = new byte[count];

            for (int cnt = 0; cnt < count; cnt++)
            {
                if (stream_handle.Position == stream_handle.Length) return null;

                bytes[cnt] = (byte)stream_handle.ReadByte();
            }

            return bytes;
        }

        private void WriteBytes(FileStream stream_handle , byte[] bytes)
        {
            foreach(byte bt in bytes)
            {
                stream_handle.WriteByte(bt);
            }

            stream_handle.Close();
        }
    }
}
