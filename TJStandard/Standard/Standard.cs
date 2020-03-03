using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;


namespace TJStandard
{
  public static class Standard
  {
    public static char MessageHeaderSeparator { get; } = '|';

    public static string GridColumnPrefix { get; } = "Cc";

    public static string GetGridColumnName(string ColumnName) => $"{GridColumnPrefix}{ColumnName}";

    public static string HeaderAndMessage(string header, string message)
    {
      return header + MessageHeaderSeparator + message;
    }

    public static bool IsEmpty(string value) => string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim());

    public static string GetDateOfPdbFile() => CxConvert.ToString(File.GetLastWriteTime($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.pdb"));

    public static string GetDateOfFile(string FileName) => CxConvert.ToString(File.GetLastWriteTime(FileName));

    public static string GetVersionOfFile(string FileName) => FileVersionInfo.GetVersionInfo(FileName).FileVersion;

    public static void Debug(string value) => Console.WriteLine(value); // TODO: В релизе нужно убрать все вызовы этого метода

    public static string GetExecutingAssemblyPath() => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

    public static Tuple<string, string> HeaderAndMessage(string MessageWithHeader)
    {
      string header, message; header = message = string.Empty; int count = 0;
      string[] words = MessageWithHeader.Split(MessageHeaderSeparator);
      foreach (string word in words) if (++count == 1) { header = word; } else { message += word; }
      return Tuple.Create(header, message);
    }

    public static int CheckRange(int Variable, int MinValue, int MaxValue)
    {
      if (MinValue > MaxValue) MinValue = MaxValue;
      if (Variable > MaxValue) Variable = MaxValue;
      if (Variable < MinValue) Variable = MinValue;
      return Variable;
    }

    public static List<string> GetFiles(string directory, string subDirectory, string searchPattern, char separator = ' ')
    {
      List<string> result = new List<string>();
      if (directory.Trim() == string.Empty) directory = Environment.CurrentDirectory;
      string Folder = subDirectory.Trim() == string.Empty ? directory : Path.Combine(directory, subDirectory);
      string[] array = searchPattern.Split(separator);
      if (Directory.Exists(Folder))
        foreach (string item in array)
          foreach (string file in Directory.EnumerateFiles(Folder, item))
            result.Add(file);
      return result;
    }
  }
}
