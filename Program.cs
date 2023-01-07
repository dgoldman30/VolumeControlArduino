using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
using System.IO.Ports;
using java.awt;

namespace VolumeControlArduino
{
    class Program
    {
        static void Main(string[] args)
        {
            TrayIcon = new NotifyIcon();
            trayIcon.Text = "My application";
            trayIcon.Icon = TheIcon

    // Add menu to the tray icon and show it.
    trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
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
                //sp.Close();
                Console.WriteLine("Please plug in your Volume Control Module.");
            }

            while (true)
            {
                if (sp.IsOpen)
                {
                    var val = sp.ReadLine().Substring(0);
                    double value;
                    
                    try 
                    {
                        value = Convert.ToDouble(val);
                        if (value == -100)
                        {
                            
                            if (microphone.IsMuted == false)
                            {
                                microphone.Mute(true);
                                defaultPlaybackDevice.Mute(true);
                            }
         
                            Console.WriteLine("Muted");
                        } else
                        {
                            if (microphone.IsMuted == true)
                            {
                                microphone.Mute(false);
                                defaultPlaybackDevice.Mute(false);
                            }
                            
                            defaultPlaybackDevice.Volume += value;
                            Console.WriteLine("Current Volume:" + defaultPlaybackDevice.Volume);
                        }
                       
                    }
                    catch (Exception e) {

                        Console.WriteLine(e);
                         value = 0;
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