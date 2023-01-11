using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
using System.IO.Ports;


namespace VolumeControlArduino
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort sp = new SerialPort();
            sp.BaudRate = 9600;
            sp.PortName = "COM7";

            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            CoreAudioDevice microphone = new CoreAudioController().DefaultCaptureDevice;
            Console.WriteLine("Current Volume:" + defaultPlaybackDevice.Volume);

            try
            {
                sp.Open();
            }
            catch
            {
                Console.WriteLine("Please plug in your Volume Control Module.");
            }
            bool muted = false;
            while (true)
            {
                if (sp.IsOpen)
                {


                    try
                    {
                        String val = sp.ReadLine().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        
                        switch (val)
                        {
                            case "V+":
                                {
                                    defaultPlaybackDevice.Volume++;
                                    break;
                                }
                            case "V-":
                                {

                                    defaultPlaybackDevice.Volume--;
                                    break;
                                }
                            case "M+":
                                {

                                    microphone.Volume++;
                                    break;
                                }
                            case "M-":
                                {

                                    microphone.Volume--;
                                    break;
                                }
                            case "MU":
                                {
                                    muted = true;
                                    microphone.Mute(true);
                                    defaultPlaybackDevice.Mute(true);
                                    Console.WriteLine("Muted");
                                    break;
                                }
                            case "UN":
                                {
                                    muted = false;
                                    microphone.Mute(false);
                                    defaultPlaybackDevice.Mute(false);
                                    Console.WriteLine("Unmuted");
                                    break;

                                }
                        }
                        if (!muted)
                        {
                            Console.WriteLine("Current Volume:" + defaultPlaybackDevice.Volume + "-----------------Microphone Volume: " + microphone.Volume);
                        }

                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e);
                    }


                }
                else
                {
                    Console.WriteLine("Please plug in your Volume Control Module.");
                }
            }
        }
    }
}