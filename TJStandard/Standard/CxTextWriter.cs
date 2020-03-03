using System.IO;
using System.Text;

namespace TJStandard
{
  public class CxTextWriter : TextWriter
  {

    private readonly IOutputMessage OutputMessageDevice;

    public CxTextWriter(IOutputMessage outputMessageDevice)
    {
      OutputMessageDevice = outputMessageDevice;
    }

    public override Encoding Encoding => Encoding.UTF8;

    public void OutputMessage(string message, string header="") => OutputMessageDevice.OutputMessage(message);

    public override void WriteLine(string value) => OutputMessage(value);

    public override void Write(string value) => OutputMessage(value);

  }
}

/*
    public void OutputMessage(string message, string header="")
    {
      OutputMessageDevice.OutputMessage(message);
      //----base.WriteLine(message); // <--- No, no, no need to do that !
    }
*/
