/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   04/10/2022
**/

using System.Threading.Tasks;

namespace DnssWebApi.Interfaces
{
    public interface IParameters
    {
        void StartMessuarementAsync();
        void SaveSettingsAsync();
        bool StopMessuarementAsync();

        Task<bool> SetMountingAngleAsync(float angleZ);
        Task<bool> SetMountingOffsetAsync(float x, float y, float z);
        Task<bool> SetPositionPrimaryAntennaAsync(float x, float y, float z);
        Task<bool> SetPositionSecondayAntennaAsync(float x, float y, float z);
        Task<bool> SetPOIAsync(int index, string name, float x, float y, float z);
    }
}