using System;

namespace Model
{
  [Serializable]
  public class Event
  {
    public int IdEvent { get; set; }

    public string TimeEvent { get; set; }

    public int IdMessage { get; set; }

    public int IdUser { get; set; }

    public string UserLogin { get; set; }

    public string UserFullName { get; set; }

    public string TextMessage { get; set; }

    public string TextEvent { get; set; }

    public string TextNote { get; set; }
  }
}
