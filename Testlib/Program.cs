using ADMA_StartStop;
using DnssWebApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GnssTest
{
    internal class TestLib
    {
        private static async Task Main(string[] args)
        {
            // Test if all Methods Arf Function
            var element = new Parameters();

            Console.WriteLine(await element.SetMountingAngleAsync(50));
            Console.WriteLine(await element.SetPositionPrimaryAntennaAsync(33, 33, 33));
            Console.WriteLine(await element.SetPositionSecondayAntennaAsync(11, 21, 31));
            Console.WriteLine(await element.SetMountingOffsetAsync(10.4f, 20, 30));
            Console.WriteLine(await element.SetPOIAsync(7, "TEST", 1, 2, 3));

            element.StopMessuarementAsync();

            //Use as last after the the changes if not used right code will slow down.
            element.SaveSettingsAsync();
        }
    }
}
