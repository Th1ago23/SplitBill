namespace Application.DTO.Group
{
    public record GroupResponseDTO(int id, string name, int leaderId, bool isPublic)
    {
    }
}