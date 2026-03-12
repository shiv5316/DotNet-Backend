public partial class Hostel
{
    public int HostelId { get; set; }

    public int? RoomNo { get; set; }

    public int? StudentId { get; set; }

    public virtual Student Student { get; set; }
}
