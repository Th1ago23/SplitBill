namespace Domain.DTO.Group
{
    public record GroupResponseDTO(string name, int leaderId, bool isPublic)
    {
    }
}