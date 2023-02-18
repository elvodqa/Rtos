using Microsoft.Xna.Framework;
using Rtos.Core.Objects;

namespace Rtos.Core;

public class Beatmap
{
    public int Version;
    public string Title;
    public string Artist;
    public string Creator;
    public Difficulty Difficulty;
    
    public string AudioFile;
    public string BackgroundFile;
    
    public double AudioLeadIn; // Time in seconds before the first hit object that the audio file will start playing
    public double PreviewTime;
    
    public int Countdown;
    public int SampleSet;
    public float StackLeniency;
    public int Mode;
    public int LetterboxInBreaks;
    public int SpecialStyle;
    public int WidescreenStoryboard;
    
    public double SliderMultiplier;
    public double SliderTickRate;
    
    public List<HitObject> HitObjects;
    public List<Break> Breaks;
    public List<TimingPoint> TimingPoints;
    public List<Color> ComboColors;
    
    public Beatmap()
    {
        HitObjects = new List<HitObject>();
        Breaks = new List<Break>();
        TimingPoints = new List<TimingPoint>();
        ComboColors = new List<Color>();
        Difficulty = new Difficulty();
    }

    public void Load(GameBase game, string osuFile)
    {
        string[] lines = File.ReadAllLines(osuFile);
        bool reachedGeneral = false;
        bool reachedMetadata = false;
        bool reachedDifficulty = false;
        bool reachedTimingPoints = false;
        bool reachedColors = false;
        bool reachedHitObjects = false;
        int curComboNumber = 1;
        foreach (string line in lines)
        {
            if (line == "[General]")
            {
                reachedGeneral = true;
                reachedMetadata = false;
                reachedDifficulty = false;
                reachedTimingPoints = false;
                reachedColors = false;
                reachedHitObjects = false;
                continue;
            }
            if (line == "[Metadata]")
            {
                reachedGeneral = false;
                reachedMetadata = true;
                reachedDifficulty = false;
                reachedTimingPoints = false;
                reachedColors = false;
                reachedHitObjects = false;
                continue;
            }
            if (line == "[Difficulty]")
            {
                reachedGeneral = false;
                reachedMetadata = false;
                reachedDifficulty = true;
                reachedTimingPoints = false;
                reachedColors = false;
                reachedHitObjects = false;
                continue;
            }
            if (line == "[TimingPoints]")
            {
                reachedGeneral = false;
                reachedMetadata = false;
                reachedDifficulty = false;
                reachedTimingPoints = true;
                reachedColors = false;
                reachedHitObjects = false;
                continue;
            }
            if (line == "[Colours]")
            {
                reachedGeneral = false;
                reachedMetadata = false;
                reachedDifficulty = false;
                reachedTimingPoints = false;
                reachedColors = true;
                reachedHitObjects = false;
                continue;
            }
            if (line == "[HitObjects]")
            {
                reachedGeneral = false;
                reachedMetadata = false;
                reachedDifficulty = false;
                reachedTimingPoints = false;
                reachedColors = false;
                reachedHitObjects = true;
                continue;
            }
            if (reachedGeneral)
            {
                if (line == "")
                    continue;
                string[] split = line.Split(':');
                switch (split[0])
                {
                    case "AudioFilename":
                        AudioFile = split[1].Trim();
                        break;
                    case "AudioLeadIn":
                        AudioLeadIn = double.Parse(split[1]);
                        break;
                    case "PreviewTime":
                        PreviewTime = double.Parse(split[1]);
                        break;
                    case "Countdown":
                        Countdown = int.Parse(split[1]);
                        break;
                    case "SampleSet":
                        SampleSet = split[1] switch
                        {
                            "Normal" => 0,
                            "Soft" => 1,
                            "Drum" => 2,
                            _ => 0
                        };
                        break;
                    case "StackLeniency":
                        StackLeniency = float.Parse(split[1]);
                        break;
                    case "Mode":
                        Mode = int.Parse(split[1]);
                        break;
                    case "LetterboxInBreaks":
                        LetterboxInBreaks = int.Parse(split[1]);
                        break;
                    case "SpecialStyle":
                        SpecialStyle = int.Parse(split[1]);
                        break;
                    case "WidescreenStoryboard":
                        WidescreenStoryboard = int.Parse(split[1]);
                        break;
                }
            }
            if (reachedMetadata)
            {
                if (line == "")
                    continue;
                string[] split = line.Split(':');
                switch (split[0])
                {
                    case "Title":
                        Title = split[1];
                        break;
                    case "Artist":
                        Artist = split[1];
                        break;
                    case "Creator":
                        Creator = split[1];
                        break;
                }
            }
            if (reachedDifficulty)
            {
                if (line == "")
                    continue;
                string[] split = line.Split(':');
                switch (split[0])
                {
                    case "HPDrainRate":
                        Difficulty.HpDrainRate = double.Parse(split[1]);
                        break;
                    case "CircleSize":
                        Difficulty.CircleSize = double.Parse(split[1]);
                        break;
                    case "OverallDifficulty":
                        Difficulty.OverallDifficulty = double.Parse(split[1]);
                        break;
                    case "ApproachRate":
                        Difficulty.ApproachRate = double.Parse(split[1]);
                        break;
                    case "SliderMultiplier":
                        SliderMultiplier = double.Parse(split[1]);
                        break;
                    case "SliderTickRate":
                        SliderTickRate = double.Parse(split[1]);
                        break;
                }
            }
            if (reachedTimingPoints)
            {
                if (line == "")
                    continue;
                string[] split = line.Split(',');
                TimingPoint timingPoint = new TimingPoint();
                timingPoint.Offset = double.Parse(split[0]);
                timingPoint.MsPerBeat = double.Parse(split[1]);
                timingPoint.Meter = int.Parse(split[2]);
                timingPoint.SampleSet = int.Parse(split[3]);
                timingPoint.SampleIndex = int.Parse(split[4]);
                timingPoint.Volume = int.Parse(split[5]);
                timingPoint.Inherited = int.Parse(split[6]) == 1;
                timingPoint.KiaiMode = int.Parse(split[7]) == 1;
                TimingPoints.Add(timingPoint);
            }
            if (reachedColors)
            {
                string[] split = line.Split(':');
                foreach (string color in split[1].Split('|'))
                { 
                    string[] rgb = color.Split(',');
                    ComboColors.Add(new Color(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2])));
                }
            }
            if (reachedHitObjects)
            {
                /*
                # it seems osu! uses the bezier curve if there are more than 3 points (for perfect curves)
                # https://github.com/ppy/osu/blob/7fbbe74b65e7e399072c198604e9db09fb729626/osu.Game/Rulesets/Objects/SliderCurve.cs#L32 
                */
                
                string[] split = line.Split(',');
                if (int.Parse(split[3]) == 1  || int.Parse(split[3]) == 5) // hitcircle
                {
                    HitCircle hitCircle = new HitCircle();
                    hitCircle.Position = new Vector2(int.Parse(split[0]), int.Parse(split[1]));
                    hitCircle.Time = int.Parse(split[2]);
                    hitCircle.Type = ObjectType.Circle;
                    hitCircle.HitSound = int.Parse(split[4]);
                    if (int.Parse(split[3]) == 5)
                    {
                        curComboNumber = 1;
                        hitCircle.ComboNumber = curComboNumber;
                        curComboNumber++;
                    } 
                    else 
                    {
                        hitCircle.ComboNumber = curComboNumber;
                        curComboNumber++;
                    }
                    HitObjects.Add(hitCircle);
                }
                
                if (int.Parse(split[3]) == 2 || int.Parse(split[3]) == 6) // slider
                {
                    Slider slider = new Slider();
                    slider.Position = new Vector2(int.Parse(split[0]), int.Parse(split[1]));
                    slider.Time = int.Parse(split[2]);
                    slider.Type = ObjectType.Slider;
                    slider.HitSound = int.Parse(split[4]);
                    string[] sliderData = split[5].Split('|');
                    CurveType defaultCurveType = sliderData[0] switch 
                    {
                        "B" => CurveType.Bezier,
                        "C" => CurveType.Catmull,
                        "L" => CurveType.Linear,
                        "P" => CurveType.Perfect,
                        _ => CurveType.Linear,
                    };
                    
                    for (int i = 1; i < sliderData.Length; i++)
                    {
                        string[] point = sliderData[i].Split(':');
                        CurvePoint curvePoint = new CurvePoint();
                        curvePoint.Position = new Vector2(int.Parse(point[0]), int.Parse(point[1]));
                        if (i > 1 && sliderData[i - 1] == sliderData[i])
                        {
                            curvePoint.Type = CurveType.Linear;
                            // remove i-1
                            slider.CurvePoints.RemoveAt(slider.CurvePoints.Count - 1);
                        }
                        else
                        {
                            curvePoint.Type = defaultCurveType;
                        }
                        slider.CurvePoints.Add(curvePoint);
                    }
                    if (int.Parse(split[3]) == 6)
                    {
                        curComboNumber = 1;
                        slider.ComboNumber = curComboNumber;
                        curComboNumber++;
                    } 
                    else 
                    {
                        slider.ComboNumber = curComboNumber;
                        curComboNumber++;
                    }
                    HitObjects.Add(slider);
                }
                
                if (int.Parse(split[3]) == 8 || int.Parse(split[3]) == 12) // spinner
                {
                    Spinner spinner = new Spinner();
                    spinner.Position = new Vector2(int.Parse(split[0]), int.Parse(split[1]));
                    spinner.Time = int.Parse(split[2]);
                    spinner.Type = ObjectType.Spinner;
                    spinner.HitSound = int.Parse(split[4]);
                    if (int.Parse(split[3]) == 12)
                    {
                        curComboNumber = 1;
                        spinner.ComboNumber = curComboNumber;
                        curComboNumber++;
                    } 
                    else 
                    {
                        spinner.ComboNumber = curComboNumber;
                        curComboNumber++;
                    }
                    HitObjects.Add(spinner);
                }
            }
        }
    }
}