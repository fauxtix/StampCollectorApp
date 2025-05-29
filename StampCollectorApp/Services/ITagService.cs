using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetTagsAsync();
        Task SaveTagAsync(Tag tag);
        Task DeleteTagAsync(Tag tag);
        Task<bool> TagNameExistsAsync(string name, int excludeId = 0);
    }
}
