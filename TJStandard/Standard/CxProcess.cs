using System.Diagnostics;
using System.Text;

namespace TJStandard.Tools
{
  public class CxProcess
  {
    public static string Execute(string command, string parameter, string encoding = "CP866")
    {
      string parameters = parameter;
      string output = string.Empty;
      string error = string.Empty;

      Encoding enc = XxEncoding.GetEncoding(encoding);

      ProcessStartInfo psi = new ProcessStartInfo(command, parameters);

      psi.RedirectStandardOutput = true;
      psi.RedirectStandardError = true;    
      psi.UseShellExecute = false;
      psi.CreateNoWindow = true;
      psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
      psi.StandardOutputEncoding = enc;

      System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi);

      using (System.IO.StreamReader myOutput = process.StandardOutput)
      {
        output = myOutput.ReadToEnd();
      }

      using (System.IO.StreamReader myError = process.StandardError)
      {
        error = myError.ReadToEnd();
      }

      output = output + " " + error;
      return output;    
    }
  }
}
