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