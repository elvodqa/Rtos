namespace Rtos.Audio;

using ManagedBass;

public class MusicPlayer
{
    private int streamHandle = 0;
    private bool paused = false;

    public MusicPlayer(string filename)
    {
        if (!Bass.Init())
        {
            throw new Exception("Failed to initialize audio engine");
        }

        // Load the audio file
        streamHandle = Bass.CreateStream(filename, 0, 0, BassFlags.Default);
        
        if (streamHandle == 0)
        {
            throw new Exception("Failed to load audio file");
        }
    }

    public void Play()
    {
        if (paused)
        {
            Bass.ChannelPlay(streamHandle, false);
        }
        else
        {
            Bass.ChannelPlay(streamHandle, true);
        }
        paused = false;
    }

    public void Stop()
    {
        Bass.ChannelStop(streamHandle);
        paused = false;
    }

    public void Pause()
    {
        Bass.ChannelPause(streamHandle);
        paused = true;
    }

    public void Resume()
    {
        Bass.ChannelPlay(streamHandle, false);
        paused = false;
    }

    public double TrackLength
    {
        get
        {
            //return Bass.ChannelBytes2Seconds(streamHandle, Bass.ChannelGetLength(streamHandle));
            long length = Bass.ChannelGetLength(streamHandle);
            return Bass.ChannelBytes2Seconds(streamHandle, length);
        }
    }
    
    public bool IsPlaying
    {
        get
        {
            return Bass.ChannelIsActive(streamHandle) == PlaybackState.Playing;
        }
    }
    
    public bool IsPaused
    {
        get
        {
            return paused;
        }
    }
    
    public bool IsStopped
    {
        get
        {
            return Bass.ChannelIsActive(streamHandle) == PlaybackState.Stopped;
        }
    }

    public double TrackPos
    {
        get
        {
            long position = Bass.ChannelGetPosition(streamHandle);
            return Bass.ChannelBytes2Seconds(streamHandle, position);
        }
        set
        {
            long position = Bass.ChannelSeconds2Bytes(streamHandle, value);
            Bass.ChannelSetPosition(streamHandle, position);
        }
    }
    
    public float Volume
    {
        get
        {
            Bass.ChannelGetAttribute(streamHandle, ChannelAttribute.Volume, out float volume);
            return volume;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 1)
            {
                value = 1;
            }
            Bass.ChannelSetAttribute(streamHandle, ChannelAttribute.Volume, value);
        }
    }
    
    public void Dispose()
    {
        Bass.StreamFree(streamHandle);
        Bass.Free();
    }
}