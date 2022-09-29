namespace DnssWebApi
{
    public static class Addresses
    {
        public static readonly string IpAddress = "195.0.0.77";

        public static readonly string StopMesurementPath = @"/api/v1/cmd/cmd_measurement_stop.json";

        public static readonly string SaveSettingsPath = @"/api/v1/cmd/cmd_config_save.json";

        public static readonly string SetMountingAnglePath = @"/api/v1/config/parameter/mounting/config_mounting_angle_adma_z.json";

        public static readonly string SetMountingOffsetPathX = @"/api/v1/config/parameter/mounting/config_mounting_angle_adma_x.json";
        public static readonly string SetMountingOffsetPathY = @"/api/v1/config/parameter/mounting/config_mounting_angle_adma_y.json";
        public static readonly string SetMountingOffsetPathZ = @"/api/v1/config/parameter/mounting/config_mounting_angle_adma_z.json";

        public static readonly string SetPositionPrimaryAntennaPathX = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps_x.json";
        public static readonly string SetPositionPrimaryAntennaPathY = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps_y.json";
        public static readonly string SetPositionPrimaryAntennaPathZ = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps_z.json";

        public static readonly string SetPositionSecondayAntennaPathX = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps2_x.json";
        public static readonly string SetPositionSecondayAntennaPathY = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps2_y.json";
        public static readonly string SetPositionSecondayAntennaPathZ = @"/api/v1/config/parameter/mounting/config_mounting_pos_gps2_z.json";

        public static readonly string SetPOIWithoutIndex = @"/api/v1/config/parameter/mounting/config_mounting_pos_poi";


    }
}
