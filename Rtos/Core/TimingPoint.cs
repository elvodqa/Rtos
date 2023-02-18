namespace Rtos.Core;

public class TimingPoint
{
    public double Offset;
    public double MsPerBeat;
    public int Meter;
    public int SampleSet;
    public int SampleIndex;
    public int Volume;
    public bool Inherited;
    public bool KiaiMode;
    
    public TimingPoint()
    {
    }
}