namespace CostPlanningServer.Model
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
