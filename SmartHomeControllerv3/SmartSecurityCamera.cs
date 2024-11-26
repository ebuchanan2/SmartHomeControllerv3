using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHomeController
{
    public class SmartSecurityCamera : SmartDevice
    {
        // Private fields
        private string resolution;
        private bool recordingStatus;

        // Public properties
        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        // Constructor
        public SmartSecurityCamera(int cameraDeviceID, string cameraDeviceName, string cameraResolution) : base(cameraDeviceID, cameraDeviceName)
        {
            this.Resolution = cameraResolution;
            recordingStatus = false; // Default recording status
            
        }

        // Methods
        public void StartRecording()
        {
            if (!recordingStatus)
            {
                recordingStatus = true;
                Console.WriteLine($"{DeviceName} has started recording.");
            }
            else
            {
                Console.WriteLine($"{DeviceName} is already recording.");
            }
        }

        public void StopRecording()
        {
            if (recordingStatus)
            {
                recordingStatus = false;
                Console.WriteLine($"{DeviceName} has stopped recording.");
            }
            else
            {
                Console.WriteLine($"{DeviceName} is not currently recording.");
            }
        }

        public override void GetStatus()
        {
            base.GetStatus();
            Console.WriteLine("This is a smart camera");
            Console.WriteLine($"Resolution: {Resolution}");
        }
    }
}
