using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Utils;
public static class SoundManager
{
    private static readonly Dictionary<string, WaveOutEvent> waveOuts = [];
    private static readonly Dictionary<string, AudioFileReader> audioFiles = [];

    public static void PlaySound(string fileName)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "soundtracks", fileName);

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: No se encontró el archivo de sonido en {filePath}");
            return;
        }

        Task.Run(() =>
        {
            if (!waveOuts.ContainsKey(fileName))
            {
                var waveOut = new WaveOutEvent();
                var audioFile = new AudioFileReader(filePath)
                {
                    Volume = 0.4f 
                };
                waveOut.Init(audioFile);

                waveOuts[fileName] = waveOut;
                audioFiles[fileName] = audioFile;
            }

            audioFiles[fileName].Position = 0;

            waveOuts[fileName].Stop();
            waveOuts[fileName].Play();
        });
    }
    public static void PlaySoundLoop(string fileName)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "soundtracks", fileName);

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: No se encontró el archivo de sonido en {filePath}");
            return;
        }

        Task.Run(() =>
        {
            if (!waveOuts.ContainsKey(fileName))
            {
                var waveOut = new WaveOutEvent();
                var audioFile = new AudioFileReader(filePath)
                {
                    Volume = 0.3f 
                };
                waveOut.Init(audioFile);

                waveOuts[fileName] = waveOut;
                audioFiles[fileName] = audioFile;
            }

            audioFiles[fileName].Position = 0;
            waveOuts[fileName].Stop();
            waveOuts[fileName].Play(); 
            waveOuts[fileName].PlaybackStopped += (sender, e) =>
            {
                audioFiles[fileName].Position = 0;
                waveOuts[fileName].Play();
            };
        });
    }

    public static void Dispose()
    {
        foreach (var waveOut in waveOuts.Values)
        {
            waveOut.Stop();
            waveOut.Dispose();
        }
        foreach (var audioFile in audioFiles.Values)
        {
            audioFile.Dispose();
        }

        waveOuts.Clear();
        audioFiles.Clear();
    }
}


