namespace TeamService.Models;

public class Result<T>
{
    private readonly List<string> _errors;

    public Result()
    {
        this._errors = new List<string>();
    }

    public T Data { get; set; }

    public IReadOnlyCollection<string> Errors { get; init; }

    public bool Failure => this._errors.Any();

    public void AddErrors(params string[] errors)
        => this._errors.AddRange(errors);
}
