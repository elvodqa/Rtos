using ManagedBass;

namespace Rtos.Audio;

public class AudioChannel
{
    private int channelHandle;

    public AudioChannel(string filename)
    {
        if (!Bass.Init())
        {
            throw new Exception("Failed to initialize audio engine");
        }

        // Load the audio file
        channelHandle = Bass.CreateStream(filename, 0, 0, BassFlags.Default);

        if (channelHandle == 0)
        {
            throw new Exception("Failed to load audio file");
        }
    }

    public void Play()
    {
        Bass.ChannelPlay(channelHandle, false);
    }

    public void Stop()
    {
        Bass.ChannelStop(channelHandle);
    }

    public void Pause()
    {
        Bass.ChannelPause(channelHandle);
    }

    public void Resume()
    {
        Bass.ChannelPlay(channelHandle, false);
    }

    public double GetLength()
    {
        return Bass.ChannelBytes2Seconds(channelHandle, Bass.ChannelGetLength(channelHandle));
    }

    public void SetPosition(double seconds)
    {
        long position = Bass.ChannelSeconds2Bytes(channelHandle, seconds);
        Bass.ChannelSetPosition(channelHandle, position);
    }

    public double GetPosition()
    {
        long position = Bass.ChannelGetPosition(channelHandle);
        return Bass.ChannelBytes2Seconds(channelHandle, position);
    }

    public void Dispose()
    {
        Bass.StreamFree(channelHandle);
        Bass.Free();
    }
}

public class SfxPlayer
{
    private List<AudioChannel> channels = new();

    public AudioChannel Play(string filename)
    {
        AudioChannel channel = new AudioChannel(filename);
        channels.Add(channel);
        channel.Play();
        return channel;
    }

    public void StopAll()
    {
        foreach (AudioChannel channel in channels)
        {
            channel.Stop();
            channel.Dispose();
        }
        channels.Clear();
    }
}