using System.ComponentModel.DataAnnotations;

using static TeamService.Infrastructure.Validations.Validations;

namespace TeamService.Models.Requests;

public class CreateTeamRequestModel
{
    [Required]
    [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
    public string Name { get; init; }

    [Required]
    [StringLength(TeamGoalsMaxLenght, MinimumLength = TeamGoalsMinLenght)]
    public string Goals { get; init; }

    [Required]
    public IEnumerable<string> Members { get; init; }
}
