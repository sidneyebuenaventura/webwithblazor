using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class QuizMapper
{
    
    /* Build QuizViewModel */
    public static QuizViewModel? Map(QuizDto? quizDto, IList<QuizQuestionDto> quizQuestionDtos, IList<QuizQuestionOptionDto> quizQuestionOptionDtos)
    {
        if (quizDto == null) return null;
        var (id,
            title, 
            estimatedDuration, 
            passingRate) = GetQuizDetails(quizDto);

        return new QuizViewModel(id,
            title, 
            estimatedDuration, 
            passingRate,
            quizQuestionDtos
                .Where(q => q.ParentId.Equals(id))
                .Select(q => Map(q, quizQuestionOptionDtos))
                .ToList());
    }
    
    private static (string id,
        string title, 
        int? estimatedDuration, 
        int? passingRate
        ) GetQuizDetails(QuizDto? quizDto)
    {
        return (
            id: quizDto.Id,
            title: quizDto.Title,
            estimatedDuration: quizDto.EstimatedDuration,
            passingRate: quizDto.PassingRate
        );
    }
    
    /* Build QuizQuestionViewModel */
    public static QuizQuestionViewModel Map(QuizQuestionDto quizQuestionDto, IList<QuizQuestionOptionDto> quizQuestionOptionDtos)
    {
        var (id,
            title, 
            content, 
            order) = GetQuizQuestionDetails(quizQuestionDto);

        return new QuizQuestionViewModel(id,
            title, 
            content, 
            order, 
            quizQuestionOptionDtos
                .Where(q => q.ParentId.Equals(quizQuestionDto.Id))
                .Select(Map).ToList());
    }
    
    private static (string id,
        string title, 
        string content, 
        int order
        ) GetQuizQuestionDetails(QuizQuestionDto quizQuestionDto)
    {
        return (
            id: quizQuestionDto.Id,
            title: quizQuestionDto.Title,
            content: quizQuestionDto.Content,
            order: quizQuestionDto.Order
        );
    }
    
    /* Build QuizQuestionOptionViewModel */
    public static QuizQuestionOptionViewModel Map(QuizQuestionOptionDto quizQuestionOptionDto)
    {
        var (id,
            content, 
            isAnswer, 
            order) = GetQuizQuestionOptionDetails(quizQuestionOptionDto);

        return new QuizQuestionOptionViewModel(id,
            content, 
            isAnswer, 
            order);
    }
    
    private static (string id,
        string content, 
        bool isAnswer, 
        int order
        ) GetQuizQuestionOptionDetails(QuizQuestionOptionDto quizQuestionOptionDto)
    {
        return (
            id: quizQuestionOptionDto.Id,
            content: quizQuestionOptionDto.Content,
            isAnswer: quizQuestionOptionDto.IsAnswer,
            order: quizQuestionOptionDto.Order
        );
    }
}