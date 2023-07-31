namespace Issentialz.Application.Features.Visits.Queries.GetCheckedInClients
{
    public class GetCheckedInClientsQueryResponse
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientMobileNumber { get; set; }
        public string ClientEmail { get; set; }
        public DateTimeOffset CheckInStamp { get; set; }
    }
}
