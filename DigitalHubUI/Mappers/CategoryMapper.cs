using DigitalHubUI.ViewModels.Categories;
using Progress.Sitefinity.RestSdk.Dto;
using CategoryDto = DigitalHubUI.Dto.CategoryDto;

namespace DigitalHubUI.Mappers;

public static class CategoryMapper
{
    public static ArticleCategoryViewModel? MapArticleCategory(CategoryDto? categoryDto)
    {
        return categoryDto == null
            ? null
            : new ArticleCategoryViewModel(categoryDto.Id, categoryDto.Title, categoryDto.Name);
    }

    public static ArticleCategoryViewModel? MapArticleCategory(TaxonDto? taxonDto)
    {
        if (taxonDto == null) return null;
        taxonDto.TryGetValue<string>("Name", out var name);
        return new ArticleCategoryViewModel(taxonDto.Id, taxonDto.Title, name);
    }

    public static CourseCategoryViewModel? MapCourseCategory(CategoryDto? categoryDto)
    {
        return categoryDto == null
            ? null
            : new CourseCategoryViewModel(categoryDto.Id, categoryDto.Title, categoryDto.Name);
    }

    public static CourseCategoryViewModel? MapCourseCategory(TaxonDto? taxonDto)
    {
        if (taxonDto == null) return null;
        taxonDto.TryGetValue<string>("Name", out var name);
        return new CourseCategoryViewModel(taxonDto.Id, taxonDto.Title, name);
    }
}