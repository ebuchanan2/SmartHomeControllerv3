using SmartHomeController;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

public class Program
{
    private static List<SmartDevice> devices = new List<SmartDevice>();

    static string destinationFilePath;
    public static void Main()
    {
        string folder = "data";
        string fileName = "SmartDevices.csv";
        destinationFilePath = CopyDataToWorkingDir(folder, fileName);
        LoadSmartDevices(destinationFilePath);

    }

    public static string CopyDataToWorkingDir(string folder, string fileName)
    {
        //Define the source and destination paths
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string sourceFilePath = Path.Combine(projectDirectory, folder, fileName);
        string destinationFilePath = Path.Combine(Environment.CurrentDirectory, fileName);
        
        if (File.Exists(sourceFilePath))
        {
            File.Copy(sourceFilePath, destinationFilePath, true);
        }
        else
        {
            Console.WriteLine("Source file not found");
        }

        return destinationFilePath;
    }

    public static void LoadSmartDevices(string destinationFilePath)
    {
        using (var reader = new StreamReader(destinationFilePath))
        {
            //skip the header line
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (values[3].Length == 0 || values[4].Length == 0)
                {
                    values[3] = "0";
                    values[4] = "0";
                }
                if (values[6].Length == 0 || values[7].Length == 0)
                {
                    values[6] = "0";
                    values[7] = "0";
                }
                if (values[8].Length == 0)
                {
                    values[8] = "0";
                }

                //Read each value into relevant variable
                int deviceID = int.Parse(values[0]);
                string deviceType = values[1];
                string deviceName = values[2];
                double brightness = double.Parse(values[3]);
                string colour = values[4];
                string cameraResolution = values[5];
                double currentTemperature = double.Parse(values[6]);
                double targetTemperature = double.Parse(values[7]);
                int volume = int.Parse(values[8]);

                SmartDevice device = null;

                switch (deviceType)
                {
                    case "SmartLight":
                        device = new SmartLight(deviceID, deviceName, brightness, colour);
                        break;
                    case "SmartSecurityCamera":
                        device = new SmartSecurityCamera(deviceID, deviceName, cameraResolution);
                        break;
                    case "SmartThermostat":
                        device = new SmartThermostat(deviceID, deviceName, currentTemperature, targetTemperature);
                        break;
                    case "SmartSpeaker":
                        device = new SmartSpeaker(deviceID, deviceName, volume);
                        break;
                }

                if (device != null)
                {
                    devices.Add(device);
                    device.GetStatus();
                }
            }
        }
    }
}
