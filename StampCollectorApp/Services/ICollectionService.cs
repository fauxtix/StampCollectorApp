using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetTagsAsync();
        Task SaveTagAsync(Tag Tag);
        Task DeleteTagAsync(Tag Tag);
        Task<bool> TagNameExistsAsync(string name, int excludeId = 0);
    }
}
