using System;

namespace TJSettings
{
  [Serializable]
  public class Folder
  {
    public int IdFolder { get; set; }

    public string NameFolder { get; set; }

    public string FullPath { get; set; }

    public int Level { get; set; }

    public Folder(int idFolder, string nameFolder, string fullPath, int level)
    {
      IdFolder = idFolder; NameFolder = nameFolder; FullPath = fullPath; Level = level;
    }

    public static Folder Create(int idFolder, string nameFolder, string fullPath, int level)
    {
      return new Folder(idFolder, nameFolder, fullPath, level);
    }

    public override string ToString()
    {
      return $"{IdFolder}; {FullPath};";
    }
  }
}

