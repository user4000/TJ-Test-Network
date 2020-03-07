namespace TJStandard
{
  public class Converter
  {
    public ConverterDatetime CvDatetime { get; } = new ConverterDatetime();

    public ConverterBoolean CvBoolean { get; } = new ConverterBoolean();

    public ConverterInt64 CvInt64 { get; } = new ConverterInt64();

    public ConverterFont CvFont { get; } = new ConverterFont();

    public ConverterColor CvColor { get; } = new ConverterColor();
  }
}