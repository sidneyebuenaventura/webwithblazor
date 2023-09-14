using System.ComponentModel;
using DigitalHubUI.Enums;

namespace DigitalHubUI.Configurations;

public class CategoriesConfiguration
{
    [DisplayName("Type of categories to display")]
    public CategoryTypes CategoryType { get; set; }
}