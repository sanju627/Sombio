using System;

[Serializable]
public class TimeSaveData
{
    public string timeStart;
    public string timeEnd;

    public TimeSaveData()
    {
        timeStart = "";
        timeEnd = "";
    }

    public TimeSaveData(DateTime start, DateTime end)
    {
        timeStart = start.ToString();
        timeEnd = end.ToString();
    }
}
