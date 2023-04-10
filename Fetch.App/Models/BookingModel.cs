public class BookingModel
{
    public int Id { get; set; }
    public string RiderId { get; set; }
    public string PassengerId { get; set; }
    public LocationModel PickUp { get; set; }
    public LocationModel DropOff { get; set; }
    public List<FareModel> Fare { get; set; } = new List<FareModel>();
    public DateTime? PickUpSchedule { get; set; }
    public DateTime DateCreated { get; set; }
    
    public BookingModel()
    {
        DateCreated = DateTime.Now;
    }
}
