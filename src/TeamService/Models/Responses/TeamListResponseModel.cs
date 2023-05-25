namespace TeamService.Models.Responses;

public class TeamListResponseModel
{
    public string Name { get; init; }

    public string Goals { get; init; }

    public IEnumerable<string> MembersUsernames { get; init; }
}
