namespace CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;

public class DeletepersonCommand
{
    public DeletepersonCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}
