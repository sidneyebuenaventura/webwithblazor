using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class ModuleMapper
{
    public static ModuleViewModel Map(ModuleDto moduleDto)
    {
        var (id, 
            title, 
            content, 
            order, 
            estimatedDuration) = GetModuleDetails(moduleDto);

        return new ModuleViewModel(id, title, content, order, estimatedDuration);
    }
    
    private static (string id, 
        string title, 
        string content, 
        int order, 
        int? estimatedDuration
        ) GetModuleDetails(ModuleDto moduleDto)
    {
        return (
            id: moduleDto.Id,
            title: moduleDto.Title,
            content: moduleDto.Content,
            order: moduleDto.Order,
            estimatedDuration: moduleDto.EstimatedDuration
        );
    }
}